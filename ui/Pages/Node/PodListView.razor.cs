using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using AntDesign.TableModels;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace ui.Pages.Node
{
    public partial class PodListView : ComponentBase
    {
        [Inject]
        private DrawerService DrawerService { get; set; }

        [Parameter]
        public IList<V1Pod> Pods { get; set; }

        async Task OnRowClick(RowData<V1Pod> row)
        {
            var options = new DrawerOptions
            {
                Title = row.Data.Name(),
                Width = 800
            };

            var drawerRef =
                await DrawerService.CreateAsync<PodDetailView, V1Pod, bool>(options,
                    row.Data);

            Console.WriteLine($"row {row.Data.Name()} was clicked.");
        }


    }
}
