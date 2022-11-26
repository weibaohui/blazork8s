using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Deployment;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.impl
{
    public class DeploymentService : IDeploymentService
    {
        private readonly IBaseService  _baseService;
        private readonly DrawerService _drawerService;

        public DeploymentService(IBaseService baseService, DrawerService drawerService)
        {
            _baseService   = baseService;
            _drawerService = drawerService;
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
            await _drawerService.CreateAsync<DeploymentDetailView, V1Deployment, bool>(options, deploy);
        }

        public async Task<V1Deployment> FilterByName(string name)
        {
            var list = await List();
            return list.Items.First(x => x.Name() == name);
        }

        public async Task<V1DeploymentList> List()
        {
            return await _baseService.Client().ListDeploymentForAllNamespacesAsync();
        }

        public async Task<V1DeploymentList> ListByNamespace(string ns)
        {
            if (string.IsNullOrEmpty(ns))
            {
                return await List();
            }

            return await _baseService.Client().ListNamespacedDeploymentAsync(ns);
        }

        public async Task<IList<V1Deployment>> ListItemsByNamespaceAsync(string ns)
        {
            var ls = await ListByNamespace(ns);
            return ls.Items;
        }
    }
}
