using System;
using System.Collections.Generic;
using AntDesign.TableModels;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace ui.Pages.Node
{
    public partial class PodListView : ComponentBase
    {
        [Parameter]
        public IList<V1Pod> Pods { get; set; }

        void OnRowClick(RowData<V1Pod> row)
        {
            Console.WriteLine($"row {row.Data.Name()} was clicked.");
        }
    }
}
