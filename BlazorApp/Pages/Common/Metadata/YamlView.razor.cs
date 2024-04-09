using System.Threading.Tasks;
using BlazorMonaco.Editor;
using k8s;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;

namespace BlazorApp.Pages.Common.Metadata;

public partial class YamlView<T> : DrawerPageBase<T>
{
    private StandaloneCodeEditor _editor = null!;

    [Parameter]
    public T Item { get; set; }

    private StandaloneEditorConstructionOptions EditorConstructionOptions(StandaloneCodeEditor editor)
    {
        return new StandaloneEditorConstructionOptions
        {
            AutomaticLayout            = true,
            Language                   = "yaml",
            Theme                      = "vs-dark",
            Contextmenu                = true,
            CopyWithSyntaxHighlighting = true,
            CursorSmoothCaretAnimation = "true",
            FoldingImportsByDefault    = true,
            MouseWheelZoom             = true,
            SmoothScrolling            = true,
            WrappingIndent             = "indent",
            AutoClosingBrackets        = "always",
            AutoSurround               = "languageDefined",
            FontSize                   = 16,
            WordWrap                   = "on",
            Minimap = new EditorMinimapOptions
            {
                Enabled = false
            },
            RenderLineHighlight  = "all",
            ScrollBeyondLastLine = true,
            Scrollbar = new EditorScrollbarOptions
            {
                AlwaysConsumeMouseWheel = true
            },
            BracketPairColorization = new BracketPairColorizationOptions
            {
                Enabled                            = true,
                IndependentColorPoolPerBracketType = true
            },
            Value = KubernetesYaml.Serialize(Item),
        };
    }


    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        Item = base.Options;
    }


    protected override void Dispose(bool disposing)
    {
        _editor.Dispose();
        base.Dispose(disposing);
    }

    private async Task OnDidInit()
    {
        await _editor.SetValue(KubernetesYaml.Serialize(Item));
    }
}
