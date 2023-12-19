using System.IO;
using System.Linq;

namespace Generator;

public static class GeneratorExtension
{
    public static void WriteToFile(this GenFileInfo info)
    {
        if (!Directory.Exists(info.TargetFullPath))
        {
            Directory.CreateDirectory(info.TargetFullPath);
        }

        File.WriteAllText(info.TargetFullFilePath, info.TargetContent);
    }

    public static void WriteToFile(this GenFileInfo info,bool removeEmptyLines)
    {
        if (!Directory.Exists(info.TargetFullPath))
        {
            Directory.CreateDirectory(info.TargetFullPath);
        }

        var contents = info.TargetContent;
        if (removeEmptyLines)
        {
            contents=  string.Join("\n", contents.Split('\n').Where(x => !string.IsNullOrWhiteSpace(x)).ToList());

        }
        File.WriteAllText(info.TargetFullFilePath, contents);
    }
}
