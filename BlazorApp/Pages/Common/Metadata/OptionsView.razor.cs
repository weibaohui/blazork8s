using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common.Metadata;

public partial class OptionsView : ComponentBase
{
    [Parameter]
    public string[] Options { get; set; }

    [Parameter]
    public string SelectOption { get; set; }



    private bool _visible = false;

    private void Open()
    {
        _visible = true;
    }
}
