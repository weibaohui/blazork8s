using System;
using System.Linq;
using System.Threading.Tasks;
using AntDesign.TableModels;
using BlazorApp.Service;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace  BlazorApp.Pages.Deployment
{
    public partial class DeploymentIndex : ComponentBase
    {
        [Inject]
        private IDeploymentService DeploymentService { get; set; }

        [Inject]
        private IPageDrawerService PageDrawerService { get; set; }

        public TablePagedService<V1Deployment> tps;


        private string _selectedNs = "";


        protected override async Task OnInitializedAsync()
        {
            tps = new TablePagedService<V1Deployment>(DeploymentService);
            await tps.GetData(_selectedNs);
        }


        public async Task OnNsSelectedHandler(string ns)
        {
            _selectedNs = ns;
            await tps.OnNsSelectedHandler(ns);
            await InvokeAsync(StateHasChanged);
        }

        public void RemoveSelection(string uid)
        {
            tps.SelectedRows = tps.SelectedRows.Where(x => x.Metadata.Uid != uid);
        }

        private void Delete(string uid)
        {
        }

        private async Task OnChange(QueryModel<V1Deployment> queryModel)
        {
            tps.OnChange(queryModel);
            await InvokeAsync(StateHasChanged);
        }

        private async Task OnSearchHandler(string key)
        {
            if (String.IsNullOrEmpty(key))
            {
                await tps.GetData(_selectedNs);
            }
            else
            {
                tps.OnSearch(tps.OriginItems.Where(w => w.Name().Contains(key)).ToList());
            }

            await InvokeAsync(StateHasChanged);
        }


        private async Task OnDeployClick(V1Deployment deploy)
        {
            var options = PageDrawerService.DefaultOptions("Deployment:" + deploy.Name());
            await  PageDrawerService.CreateAsync<DeploymentDetailView, V1Deployment, bool>(options, deploy);

        }
    }
}
