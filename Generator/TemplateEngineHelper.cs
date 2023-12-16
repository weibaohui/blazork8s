using System;
using JinianNet.JNTemplate;

namespace Generator;

public class TemplateEngineHelper
{
    public void Run()
    {
        var template = Engine.CreateTemplate("hello,$name!");
        template.Set("name", "JNTemplate");
        var render = template.Render();
        Console.WriteLine(render);
    }
    public static ITemplate LoadTemplateByString(string value)
    {
        var template = Engine.CreateTemplate(value);
        return template;
    }
    public static ITemplate LoadTemplateByPath(string fullPath)
    {
        var template = Engine.LoadTemplate(fullPath);
        return template;
    }


}
