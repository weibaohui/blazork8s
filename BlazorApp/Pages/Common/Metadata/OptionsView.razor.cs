using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common.Metadata;

public partial class OptionsView : PageBase
{
    //TODO 关联显示文档解释

    private bool _visible = false;

    [Parameter] public string[] Options { get; set; }

    [Parameter] public string SelectOption { get; set; }

    private void Open()
    {
        _visible = true;
    }
}