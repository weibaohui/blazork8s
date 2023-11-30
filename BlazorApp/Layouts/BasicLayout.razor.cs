using System.Threading.Tasks;
using AntDesign.ProLayout;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Layouts;

public partial class BasicLayout : LayoutComponentBase
{
    public MenuDataItem[] MenuData;

    protected override async Task OnInitializedAsync()
    {
        MenuData = new MenuDataItem[]
        {
            new MenuDataItem
            {
                Path = "/ai/chatDeploy",
                Name = "welcome",
                Key  = "welcome",
                Icon = "smile",
            },
            new MenuDataItem
            {
                Path = "/NodeList",
                Name = "Nodes",
                Key  = "Nodes",
                Icon = "smile",
            },
            new MenuDataItem
            {
                Name     = "Workloads",
                Key      = "Workloads",
                Icon     = "smile",
                Children = WorkloadsMenu(),
            },
        };
        await base.OnInitializedAsync();
    }


    private MenuDataItem[] WorkloadsMenu()
    {
        return new MenuDataItem[]
        {
            new MenuDataItem
            {
                Path = "/Pods",
                Name = "Pods",
                Key  = "Pods",
                Icon = "smile",
            },
            new MenuDataItem
            {
                Path = "/Deployments",
                Name = "Deployments",
                Key  = "Deployments",
                Icon = "smile",
            },
            new MenuDataItem
            {
                Path = "/ReplicaSets",
                Name = "ReplicaSets",
                Key  = "ReplicaSets",
                Icon = "smile",
            },
        };
    }
}
