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
    private readonly List<TreeData> _dataList = new();

    private TreeData _currentItem = new();
    private string _currentRootKey = "";

    private string _resultCn;
    private bool _showCnResult;
    private Tree<TreeData> _tree;

    [Parameter] public T Item { get; set; }

    [Inject] private IAiService AiService { get; set; }


    [Inject] private IMessageService MessageService { get; set; }

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
        if (x.GetCulture().Name == "zh-CN") _showCnResult = true;

        await base.OnInitializedAsync();
    }

    private async void EventHandler(object sender, string resp)
    {
        _resultCn += resp;
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
        _currentItem = arg.Node.DataItem;
        if (_showCnResult)
        {
            _resultCn = _currentItem.descriptionCN;
            if (AiService.Enabled() && string.IsNullOrWhiteSpace(_currentItem.descriptionCN)) await ReTranslate();
        }
    }

    private async Task ReTranslate()
    {
        if (!AiService.Enabled()) return;

        _resultCn = "";
        await InvokeAsync(StateHasChanged);
        //todo 改为自动翻译为各种语言
        _resultCn = await AiService.AIChat("请逐字翻译以下内容，注意请不要遗漏细节并保持原来的格式：" + _currentItem.description);
    }
}
