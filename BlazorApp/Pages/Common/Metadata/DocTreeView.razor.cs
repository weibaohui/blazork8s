using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service;
using BlazorApp.Service.AI;
using BlazorApp.Utils.Swagger;
using Entity;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common.Metadata;

public partial class DocTreeView<T> : DrawerPageBase<T> where T : IKubernetesObject<V1ObjectMeta>
{
    private readonly List<TreeData> _dataList = [];
    private string _cultureName;

    private TreeData _currentItem = new();
    private string _currentRootKey = "";

    private string _translateResult;
    private Tree<TreeData> _tree;

    [Parameter] public T Item { get; set; }

    [Inject] private IAiService AiService { get; set; }


    [Inject] private IMessageService MessageService { get; set; }
    [Inject] private IPromptService PromptService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Item = base.Options;
        var instance = (T)Activator.CreateInstance(typeof(T));
        var attribute = instance.GetKubernetesTypeMetadata();
        var group = attribute.Group == "" ? "core" : attribute.Group.Split(".")[0];
        var key = $"io.k8s.api.{group}.{attribute.ApiVersion}.{attribute.Kind}";
        //对CRD的key进行特殊处理
        if ($"{group}.{attribute.ApiVersion}.{attribute.Kind}" == "apiextensions.v1.CustomResourceDefinition")
        {
            key = "io.k8s.apiextensions-apiserver.pkg.apis.apiextensions.v1.CustomResourceDefinition";
        }

        _currentRootKey = key;
        var definition = SwaggerHelper.Instance.GetEntityByName(key);
        _dataList.AddRange(definition.ToTreeData().ChildList);

        if (AiService.Enabled())
        {
            AiService.SetChatEventHandler(EventHandler);
        }

        var x = (SimpleI18NStringLocalizer)L;
        _cultureName = x.GetCulture().Name;

        await base.OnInitializedAsync();
    }

    private async void EventHandler(object sender, string resp)
    {
        _translateResult += resp;
        await InvokeAsync(StateHasChanged);
    }

    private void ExpandAll()
    {
        _tree.ExpandAll();
    }

    private void CollapseAll()
    {
        _tree.CollapseAll();
    }

    public void OnNodeLoadDelayAsync(TreeEventArgs<TreeData> args)
    {
        if (args.Node.DataItem.RefKey != null)
        {
            var dataItem = args.Node.DataItem;
            dataItem.ChildList.Clear();
            var refKey = SwaggerHelper.Instance.GetRefKey(args.Node.DataItem.RefKey);
            var treeData = SwaggerHelper.Instance.GetEntityByName(refKey).ToTreeData();
            if (treeData.ChildList.Count > 0)
            {
                dataItem.ChildList.AddRange(treeData.ChildList);
            }
            else
            {
                //最后一级的介绍
                treeData.Title = "description";
                dataItem.ChildList.Add(treeData);
            }
        }
    }


    private string OnTitleExpression(TreeNode<TreeData> arg)
    {
        return $"{arg.DataItem.Title}";
    }


    private async Task OnItemClick(TreeEventArgs<TreeData> arg)
    {
        //每次点击都清空一遍
        _translateResult = "";
        _currentItem = arg.Node.DataItem;
        if (_cultureName == "zh-CN")
        {
            //当前只存了中文，所以其他语言都需要调用AI进行翻译
            _translateResult = _currentItem.descriptionCN;
        }

        if (AiService.Enabled() && string.IsNullOrWhiteSpace(_translateResult))
            await ReTranslate();
    }

    private async Task ReTranslate()
    {
        if (!AiService.Enabled()) return;

        _translateResult = "";
        await InvokeAsync(StateHasChanged);

        var prompt = PromptService.GetPrompt("Translate");
        _translateResult = await AiService.AIChat($"{prompt}\r" + _currentItem.description);
        await InvokeAsync(StateHasChanged);
    }
}
