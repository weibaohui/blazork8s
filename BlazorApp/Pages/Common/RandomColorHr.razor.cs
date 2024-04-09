using System;
using System.Threading.Tasks;
using AntDesign;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;

namespace BlazorApp.Pages.Common;

public partial class RandomColorHr : PageBase
{
    [Parameter]
    public PresetColor? Color { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (Color==null)
        {
            Random        random = new Random();
            PresetColor[] values = (PresetColor[])Enum.GetValues(typeof(PresetColor));
            Color = values[random.Next(values.Length)];
        }

        await base.OnInitializedAsync();
    }
}
