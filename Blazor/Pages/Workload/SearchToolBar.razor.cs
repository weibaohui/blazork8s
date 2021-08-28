using System;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.Workload
{
    public partial class SearchToolBar : ComponentBase
    {
        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public int Count { get; set; }

        [Parameter]
        public EventCallback<String> OnNsSelected { get; set; }

        public void OnNsSelectedHandler(string ns)
        {
            OnNsSelected.InvokeAsync(ns);
            // Console.WriteLine($"SearchToolBar index receive ns:{ns}");
        }
    }
}
