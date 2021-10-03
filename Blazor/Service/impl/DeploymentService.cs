using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntDesign;
using Blazor.Pages.Deployment;
using k8s.Models;

namespace Blazor.Service.impl
{
    public class DeploymentService : IDeploymentService
    {
        private readonly IBaseService  BaseService;
        private readonly DrawerService DrawerService;

        public DeploymentService(IBaseService baseService, DrawerService drawerService)
        {
            BaseService   = baseService;
            DrawerService = drawerService;
        }

        public async Task ShowDeploymentDrawer(string  name)
        {
            var deploy = await FilterByName(name);
            await ShowDeploymentDrawer(deploy);
        }
        public async Task ShowDeploymentDrawer(V1Deployment deploy)
        {
            var options = new DrawerOptions
            {
                Title = "Deployment:" + deploy.Name(),
                Width = 800
            };
            await DrawerService.CreateAsync<DeploymentDetailView, V1Deployment, bool>(options, deploy);
        }

        public async Task<V1Deployment> FilterByName(string name)
        {
            var list = await List();
            return list.Items.First(x => x.Name() == name);
        }

        public async Task<V1DeploymentList> List()
        {
            return await BaseService.GetFromJsonAsync<V1DeploymentList>("/KubeApi/apis/apps/v1/deployments");
        }

        public async Task<V1DeploymentList> ListByNamespace(string ns)
        {
            if (string.IsNullOrEmpty(ns))
            {
                return await List();
            }

            return await BaseService.GetFromJsonAsync<V1DeploymentList>(
                @$"/KubeApi/apis/apps/v1/namespaces/{ns}/deployments");
        }

        public async Task<IList<V1Deployment>> ListItemsByNamespaceAsync(string ns)
        {
            var ls = await ListByNamespace(ns);
            return ls.Items;
        }
    }
}
