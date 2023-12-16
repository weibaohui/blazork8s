using System.Collections.Generic;
using System.IO;

namespace Generator;

public class GeneratorHelper
{
    public static GeneratorHelper Instance => Nested.Instance;

    private class Nested
    {
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Nested()
        {
        }

        internal static readonly GeneratorHelper Instance = new GeneratorHelper();
    }

    public static Generator Generator(IList<IDictionary<string, string>> dictList)
    {
        var templatePath            = Directory.GetCurrentDirectory() + "../../../../template/";
        var generatorFolderPath     = Directory.GetCurrentDirectory() + "/../generator/";


        var g = new Generator
        {
            RootFolderTemplatePath = Path.GetFullPath(templatePath),
            GeneratorFolderPath    = Path.GetFullPath(generatorFolderPath)
        };

        g.SetDictList(dictList);
        return g;
    }
}
