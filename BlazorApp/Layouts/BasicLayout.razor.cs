using System.Threading.Tasks;
using AntDesign.ProLayout;
using Microsoft.AspNetCore.Components;

namespace BlazorApp
{
    public partial class BasicLayout : LayoutComponentBase
    {
        public MenuDataItem[] MenuData;

        protected override async Task OnInitializedAsync()
        {
            MenuData = new MenuDataItem[]
            {
                new MenuDataItem
                {
                    Path = "/",
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
                new MenuDataItem
                {
                    Path = "/counter",
                    Name = "Counter",
                    Key  = "Counter",
                    Icon = "smile",
                },
                new MenuDataItem
                {
                    Path = "/PodLogs",
                    Name = "PodLogs",
                    Key  = "PodLogs",
                    Icon = "smile",
                }
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
}
