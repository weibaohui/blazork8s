using Generator;

class Program
{
    private static readonly string TemplatePath        = Directory.GetCurrentDirectory() + "../../../../template/";
    private static readonly string GeneratorFolderPath = Directory.GetCurrentDirectory() + "/../generator/";


    public static void Main()
    {

        var dictList = new DictList();
        dictList.AddItem("ReplicaSet", "V1ReplicaSet");
        dictList.AddItem("Deployment", "V1Deployment");

        var fullTemplateFolderPath  = Path.GetFullPath(TemplatePath);        // 替换为要读取的文件夹路径
        var fullFolderPath          = Path.GetFullPath(TemplatePath);        // 替换为要读取的文件夹路径
        var fullGeneratorFolderPath = Path.GetFullPath(GeneratorFolderPath); // 替换为要读取的文件夹路径

        var generator               = new Generator.Generator();
        generator.RecursivelyReadFiles(fullTemplateFolderPath, fullFolderPath);
        generator.PrintGenList();
        generator.RemoveGenFolder(fullGeneratorFolderPath);
        foreach (var dict in dictList.GetDictList())
        {
            generator.SetDict(dict);
            generator.PrepareGenList();
            generator.PrintGenTargetList();
            generator.GenTemplate(fullGeneratorFolderPath);
        }
    }
}
