using System.IO;

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
}
