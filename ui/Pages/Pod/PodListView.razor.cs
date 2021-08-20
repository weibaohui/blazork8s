using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using AntDesign.TableModels;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace ui.Pages.Pod
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
                Title = "POD:" + row.Data.Name(),
                Width = 800
            };

            var drawerRef =
                await DrawerService.CreateAsync<PodDetailView, V1Pod, bool>(options,
                    row.Data);
        }
    }
}