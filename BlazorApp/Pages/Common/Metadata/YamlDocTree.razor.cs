using System.Threading.Tasks;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common.Metadata;

public partial class YamlDocTree<T> : DrawerPageBase<T> where T : IKubernetesObject<V1ObjectMeta>
{
    [Parameter] public T Item { get; set; }


    protected override async Task OnInitializedAsync()
    {
        Item ??= base.Options;

        await base.OnInitializedAsync();
    }
}
