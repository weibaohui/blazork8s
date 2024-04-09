using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;

namespace BlazorApp.Pages.Common.Metadata;

public partial class OptionsView : PageBase
{
    [Parameter]
    public string[] Options { get; set; }

    [Parameter]
    public string SelectOption { get; set; }

    //TODO 关联显示文档解释

    private bool _visible = false;

    private void Open()
    {
        _visible = true;
    }
}
