using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor.Service;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Blazor.Pages.Namespace
{
    public partial class NamespaceSelect:ComponentBase
    {
        [Parameter]
        public EventCallback<String> OnNsSelected { get; set; }

        [Inject]
        private INamespaceService NamespaceService { get; set; }

        private IList<V1Namespace> ns;
        protected override async Task OnInitializedAsync()
        {
             ns = await NamespaceService.GetNamespaces();
           
        }

        void handleChange(string value)
        {
            OnNsSelected.InvokeAsync(value);
            Console.WriteLine(value);
        }

        void handleItemsChange(IEnumerable<string> value)
        {
            Console.WriteLine(value);
        }
    }
}
