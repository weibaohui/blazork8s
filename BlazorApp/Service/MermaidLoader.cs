using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BlazorApp.Service;

/// <summary>
/// 将Mermaid.js的cdn互联网版本的JS文件修改为适用于Blazor的版本，并将其注入到HTML页面中。
/// 以此提高在局域网中使用Mermaid.js的体验。
/// </summary>
/// <param name="next"></param>
public class ModifyResponseMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/_content/Blazorade.Mermaid/js/blazoradeMermaid.js"))
        {
            var content = @"import mermaid from ""/js/lib/mermaid/mermaid.esm.min.mjs"";

export function run(id, definition, configuration) {
    console.debug(""run (id, definition, configuration)"", id, definition, configuration);
    var elem = document.getElementById(id);
    elem.removeAttribute(""data-processed"");

    elem.innerHTML = definition;
    renderOnly(""#"" + id, configuration);
}

var renderCount = 0;
export async function renderOnly(selector, configuration) {
    console.debug(""renderOnly(selector, configuration)"", selector, configuration);

    if (renderCount == 0) {
        renderCount = 1;
        prerenderDiagram();
    }

    if (configuration) {
        mermaid.initialize(configuration);
    }

    await mermaid.run({ querySelector: selector });
    renderCount++;
}

/**
 * This function is called if no diagrams have been rendered previously. It is used to render a very simple
 * flow chart diagram with no themes. Once the diagram has been rendered, it is removed from the DOM.
 * This is to work around a problem that occurs on initial rendering of diagrams that include inline 
 * themes. That problem does not occur after the initial diagram has been rendered.
 */
function prerenderDiagram() {
    var body = document.getElementsByTagName(""body"");
    if (body.length > 0) {
        var diagElement = document.createElement(""pre"");
        var dt = new Date();
        var id = ""blzrd-"" + dt.getFullYear() + dt.getMonth() + dt.getDate() + dt.getHours() + dt.getMinutes() + dt.getSeconds() + dt.getMilliseconds();
        diagElement.setAttribute(""id"", id);
        body.item(0).appendChild(diagElement);

        run(id, ""flowchart TB\n    a-->b"");

        body.item(0).removeChild(diagElement);
    }
}
";

            context.Response.ContentType = "text/javascript";
            context.Response.StatusCode = 200;

            await context.Response.WriteAsync(content);
        }
        else
        {
            await next(context);
        }
    }
}
