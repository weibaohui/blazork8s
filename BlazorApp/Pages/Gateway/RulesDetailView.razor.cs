using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using Entity.Crd.Gateway;
using Mapster;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Gateway;

public partial class RulesDetailView : PageBase
{
    [Parameter] public IList<HTTPRouteRule> Rules { get; set; }

    string _diagramDef = "";

//     const string template = @"
// flowchart LR
//     A[HTTPRouteRule]
//     A-- text -->B
//     A-- text -->C
// ";

    protected override async Task OnInitializedAsync()
    {
        if (Rules is { Count: > 0 })
        {
            _diagramDef = "flowchart LR";


            foreach (var rule in Rules)
            {
                if (rule.Matches is { Count: > 0 })
                {
                    foreach (var match in rule.Matches)
                    {
                        _diagramDef += $"\r\n Gateway --> {match.Path?.Value ?? ""}\r\n";

                        var matchValue = match.Path?.Value ?? "";

                        if (match.Headers is { Count: > 0 })
                        {
                            foreach (var header in match.Headers)
                            {
                                var backends = rule.BackendRefs?.Adapt<IList<BackendRefWithWeight>>();
                                if (backends is { Count: > 0 })
                                {
                                    foreach (var backend in backends)
                                    {
                                        _diagramDef +=
                                            $"\r\n {matchValue} -- {header.Name}={header.Value}  --> {backend.Name}:{backend.Port}\r\n";
                                    }
                                }
                            }
                        }
                        else
                        {
                            var backends = rule.BackendRefs?.Adapt<IList<BackendRefWithWeight>>();
                            if (backends is { Count: > 0 })
                            {
                                foreach (var backend in backends)
                                {
                                    _diagramDef +=
                                        $"\r\n {matchValue} -- {backend.Weight} --> {backend.Name}:{backend.Port}\r\n";
                                }
                            }
                        }


                        if (rule.Filters is { Count: > 0 })
                        {
                            var mirrorList = rule.Filters.Where(x => x.Type == HTTPRouteFilterType.RequestMirror)
                                .ToList();
                            foreach (var item in mirrorList)
                            {
                                var backend = item.RequestMirror?.BackendRef;
                                if (backend is not null)
                                {
                                    _diagramDef +=
                                        $"\r\n  {matchValue} -- Mirror --> {backend.Name}:{backend.Port}\r\n";
                                }
                            }

                            var redirectList = rule.Filters.Where(x => x.Type == HTTPRouteFilterType.RequestRedirect)
                                .ToList();
                            foreach (var item in redirectList)
                            {
                                var backend = item.RequestRedirect;
                                if (backend?.Hostname != null || backend?.Scheme != null)
                                {
                                    _diagramDef +=
                                        $"\r\n  {matchValue} -- Redirect --> {backend.Scheme}://{backend.Hostname}\r\n";
                                }

                                if (backend?.Path != null)
                                {
                                    _diagramDef +=
                                        $"\r\n  {matchValue} -- Redirect --> {backend.Path.ReplaceFullPath}\r\n";
                                }
                            }
                        }
                    }
                }
            }
        }

        await base.OnInitializedAsync();
    }
}
