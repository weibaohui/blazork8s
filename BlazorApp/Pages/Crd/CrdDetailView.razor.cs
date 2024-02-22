using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Pages.Common.Metadata;
using BlazorApp.Service.k8s;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Crd
{
    public partial class CrdDetailView : DrawerPageBase<V1CustomResourceDefinition>
    {
        [Inject]
        public IKubeService KubeService { get; set; }

        private V1CustomResourceDefinition CustomResourceDefinition { get; set; }

        private readonly List<CustomResource> _crInstanceList = new List<CustomResource>();

        protected override async Task OnInitializedAsync()
        {
            CustomResourceDefinition = base.Options;

            var group    = CustomResourceDefinition.Spec.Group;
            var versions = CustomResourceDefinition.Spec.Versions;
            var plural   = CustomResourceDefinition.Spec.Names.Plural;
            foreach (var version in versions)
            {
                var generic = new GenericClient(KubeService.Client(), group, version.Name, plural);
                var list    = await generic.ListAsync<CustomResourceList<CustomResource>>();
                _crInstanceList.AddRange(list.Items);
            }

            await base.OnInitializedAsync();
        }

        private async Task<string> GetCrYaml(string group, string version, string plural, string name)
        {
            var customObject = await KubeService.Client()
                .GetClusterCustomObjectAsync(group, version, plural, name);
            var json    = KubernetesJson.Serialize(customObject);
            var objects = KubernetesYaml.Deserialize<object>(json);
            var yaml    = KubernetesYaml.Serialize(objects);
            return yaml;
        }

        private async Task OnYamlClick(CustomResource cr)
        {
            var group    = CustomResourceDefinition.Spec.Group;
            var versions = CustomResourceDefinition.Spec.Versions;
            var plural   = CustomResourceDefinition.Spec.Names.Plural;

            var customObject = await KubeService.Client()
                .GetClusterCustomObjectAsync(group, cr.ApiGroupAndVersion().Item2, plural, cr.Name());
            var json    = KubernetesJson.Serialize(customObject);
            var item = KubernetesYaml.Deserialize<object>(json);


            var options = PageDrawerService.DefaultOptions($"Yaml:{cr.Name()}", width: 1000);
            await PageDrawerService
                .ShowDrawerAsync<YamlView<object>, object, bool>(options, item);
        }

        public class CustomResource : KubernetesObject, IMetadata<V1ObjectMeta>
        {
            [JsonPropertyName("metadata")]
            public V1ObjectMeta Metadata { get; set; }
        }

        public abstract class CustomResource<TSpec, TStatus> : CustomResource
        {
            [JsonPropertyName("spec")]
            public TSpec Spec { get; set; }

            [JsonPropertyName("status")]
            public TStatus Status { get; set; }
        }

        public class CustomResourceList<T> : KubernetesObject
            where T : CustomResource
        {
            public V1ListMeta Metadata { get; set; }
            public List<T>    Items    { get; set; }
        }
    }
}
