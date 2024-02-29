using System.Threading.Tasks;
using AntDesign;
using BlazorMonaco.Editor;
using k8s;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common.Metadata;

public partial class YamlView<T> : FeedbackComponent<T, bool>
{
    [Parameter]
    public T Item { get; set; }

    private StandaloneCodeEditor _editor = null!;

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
