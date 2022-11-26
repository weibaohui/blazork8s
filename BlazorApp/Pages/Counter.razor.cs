using BlazorApp.Service;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages
{
    public partial class Counter : ComponentBase
    {
        [Inject]
        private IKubeService KubeService { get; set; }

        private int currentCount = 0;

        private void IncrementCount()
        {
            currentCount += 1;
        }
    }
}
