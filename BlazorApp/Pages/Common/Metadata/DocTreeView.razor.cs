using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Utils.Swagger;
using Entity;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common.Metadata;

public partial class DocTreeView<T> : FeedbackComponent<T, bool> where T : IKubernetesObject<V1ObjectMeta>
{
    [Parameter]
    public T Item { get; set; }



    [Inject]
    private IMessageService MessageService { get; set; }

    private readonly List<TreeData> _dataList = new();
    private          Tree<TreeData> _tree;

    private          TreeData _currentItem         = new();
    private          string   _currentRootKey      = "";
    private readonly string[] _defaultSelectedKeys = new string[] { "apiVersion" };

    protected override async Task OnInitializedAsync()
    {
        Item = base.Options;
        var instance  = (T)Activator.CreateInstance(typeof(T));
        var attribute = instance.GetKubernetesTypeMetadata();
        var group     = attribute.Group == "" ? "core" : attribute.Group.Split(".")[0];
        var key       = $"io.k8s.api.{group}.{attribute.ApiVersion}.{attribute.Kind}";
        //对CRD的key进行特殊处理
        if ($"{group}.{attribute.ApiVersion}.{attribute.Kind}" == "apiextensions.v1.CustomResourceDefinition")
        {
            key = "io.k8s.apiextensions-apiserver.pkg.apis.apiextensions.v1.CustomResourceDefinition";
        }
        _currentRootKey = key;
        var definition = SwaggerHelper.Instance.GetEntityByName(key);
        _dataList.AddRange(definition.ToTreeData().ChildList);
        await base.OnInitializedAsync();
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
            var refKey   = SwaggerHelper.Instance.GetRefKey(args.Node.DataItem.RefKey);
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

    private void OnChecked(TreeEventArgs<TreeData> obj)
    {
        throw new NotImplementedException();
    }

    private void OnItemClick(TreeEventArgs<TreeData> arg)
    {
        _currentItem = arg.Node.DataItem;
    }

}
