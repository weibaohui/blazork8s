using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Service;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Namespace
{
    public partial class NamespaceSelect : ComponentBase
    {
        [Parameter]
        public EventCallback<String> OnNsSelected { get; set; }

        [Inject]
        private INamespaceService NamespaceService { get; set; }

        private IList<V1Namespace> _ns;

        protected override async Task OnInitializedAsync()
        {
            _ns = NamespaceService.List();
        }

        private void HandleChange(string value)
        {
            OnNsSelected.InvokeAsync(value);
        }
    }
}
