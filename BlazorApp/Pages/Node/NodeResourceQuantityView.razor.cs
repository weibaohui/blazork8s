using System.Collections.Generic;
using System.Linq;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Node
{
    public partial class NodeResourceQuantityView : ComponentBase
    {
        [Parameter]
        public string BlockTitle { get; set; }

        [Parameter]
        public IDictionary<string, ResourceQuantity> Dict { get; set; }

        public string Echo(string key)
        {
            return Dict.FirstOrDefault(w => w.Key == key).Value
                ?.CanonicalizeString(ResourceQuantity.SuffixFormat.BinarySI);
        }
    }
}
