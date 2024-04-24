using System.Threading.Tasks;
using BlazorApp.Service.k8s;
using BlazorMonaco.Editor;
using k8s;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common.Metadata;

public partial class YamlView<T> : DrawerPageBase<T>
{
    private StandaloneCodeEditor _editor = null!;
    private string _execResult;
    private bool _loading = false;
    [Inject] private IKubectlService Kubectl { get; set; }
    [Parameter] public T Item { get; set; }

    private async Task BtnApplyClicked()
    {
        _execResult = string.Empty;
        _loading = true;
        var yaml = await _editor.GetValue();
        _execResult = await Kubectl.Apply(yaml);
        _loading = false;
    }


    private StandaloneEditorConstructionOptions EditorConstructionOptions(StandaloneCodeEditor editor)
    {
        return new StandaloneEditorConstructionOptions
        {
            AutomaticLayout = true,
            Language = "yaml",
            Theme = "vs-dark",
            Contextmenu = true,
            CopyWithSyntaxHighlighting = true,
            CursorSmoothCaretAnimation = "true",
            FoldingImportsByDefault = true,
            MouseWheelZoom = true,
            SmoothScrolling = true,
            WrappingIndent = "indent",
            AutoClosingBrackets = "always",
            AutoSurround = "languageDefined",
            FontSize = 16,
            WordWrap = "on",
            Minimap = new EditorMinimapOptions
            {
                Enabled = false
            },
            RenderLineHighlight = "all",
            ScrollBeyondLastLine = true,
            Scrollbar = new EditorScrollbarOptions
            {
                AlwaysConsumeMouseWheel = true
            },
            BracketPairColorization = new BracketPairColorizationOptions
            {
                Enabled = true,
                IndependentColorPoolPerBracketType = true
            },
            Value = KubernetesYaml.Serialize(Item),
        };
    }


    protected override async Task OnInitializedAsync()
    {
        Item = base.Options;
        Kubectl.SetOutputEventHandler(EventHandler);

        await base.OnInitializedAsync();
    }

    private async void EventHandler(object sender, string resp)
    {
        _execResult += resp;
        await InvokeAsync(StateHasChanged);
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
