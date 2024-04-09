using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using BlazorMonaco.Editor;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;
using Microsoft.AspNetCore.Hosting;

namespace BlazorApp.Pages.Example;

public partial class Example : PageBase
{
    private DirectoryNode _currentItem = new();
    private string        _currentYaml = "";

    private List<DirectoryNode> _dataList = new();

    private StandaloneCodeEditor _editor = null!;
    private string               _execResult;
    private Tree<DirectoryNode>  _tree;

    [Inject]
    private IWebHostEnvironment HostingEnvironment { get; set; }

    [Inject]
    private IKubectlService Kubectl { get; set; }


    protected override Task OnInitializedAsync()
    {
        var wwwRootPath   = HostingEnvironment.WebRootPath;
        var rootDirectory = wwwRootPath + "/k8s_example";
        _dataList = GetDirectoryTree(rootDirectory);
        Kubectl.SetOutputEventHandler(EventHandler);

        return base.OnInitializedAsync();
    }

    private async void EventHandler(object sender, string resp)
    {
        _execResult += resp;
        await InvokeAsync(StateHasChanged);
    }


    private static List<DirectoryNode> GetDirectoryTree(string rootDirectory)
    {
        var directoryTree = new List<DirectoryNode>();
        var rootInfo      = new DirectoryInfo(rootDirectory);
        var rootNode = new DirectoryNode
            { Name = rootInfo.Name, FullName = rootInfo.FullName, Icon = "folder-open", IsLeaf = false };
        directoryTree.Add(rootNode);
        return directoryTree;
    }

    private List<string> GetYamlFiles(string directoryPath)
    {
        var yamlFiles = Directory.GetFiles(directoryPath, "*.yaml").ToList();
        return yamlFiles;
    }

    public void OnNodeLoadDelayAsync(TreeEventArgs<DirectoryNode> args)
    {
        var dataItem = args.Node.DataItem;

        if (dataItem.FullName == null) return;
        var directories = Directory.GetDirectories(dataItem.FullName).ToList();

        if (directories is { Count: > 0 })
        {
            dataItem.ChildList.AddRange(
                directories.Select(x => new DirectoryNode
                        { FullName = x, Name = Path.GetFileName(x), Icon = "folder-open", IsLeaf = false })
                    .ToList()
            );
        }

        var files = GetYamlFiles(dataItem.FullName);
        if (files is { Count: > 0 })
        {
            dataItem.ChildList.AddRange(
                files.Select(x => new DirectoryNode
                        { FullName = x, Name = Path.GetFileName(x), Icon = "file", IsLeaf = true })
                    .ToList()
            );
        }
    }

    private void ExpandAll()
    {
        _tree.ExpandAll();
    }

    private void CollapseAll()
    {
        _tree.CollapseAll();
    }


    private async Task OnItemClick(TreeEventArgs<DirectoryNode> arg)
    {
        _currentItem = arg.Node.DataItem;
        _currentYaml = string.Empty;

        if (_currentItem.IsLeaf)
        {
            _currentYaml = await File.ReadAllTextAsync(_currentItem.FullName);
            await _editor.SetValue(_currentYaml);
        }
    }


    private async Task BtnApplyClicked()
    {
        _execResult  = string.Empty;
        _currentYaml = await _editor.GetValue();
        _execResult  = await Kubectl.Apply(_currentYaml);
    }

    private async Task BtnDeleteClicked()
    {
        _execResult  = string.Empty;
        _currentYaml = await _editor.GetValue();
        _execResult  = await Kubectl.Delete(_currentYaml);
    }

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
            Value = _currentYaml,
        };
    }
}

public class DirectoryNode
{
    public string Icon     { get; set; }
    public string Name     { get; set; }
    public string FullName { get; set; }
    public bool   IsLeaf   { get; set; }

    public List<DirectoryNode> ChildList { get; set; } = new List<DirectoryNode>();
}
