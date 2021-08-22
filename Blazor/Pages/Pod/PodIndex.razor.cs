using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Blazor.Pages.Pod
{
    public partial class PodIndex : ComponentBase
    {
        public void OnNsSelectedHandler(string ns)
        {
            Console.WriteLine($"POD index receive ns:{ns}");
        }
    }
}
