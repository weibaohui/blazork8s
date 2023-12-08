using Generator;

class Program
{
    private static readonly string TemplatePath        = Directory.GetCurrentDirectory() + "../../../../template/";
    private static readonly string GeneratorFolderPath = Directory.GetCurrentDirectory() + "/../generator/";


    public void makeTemplate()
    {
        var dictList = new DictList();
        // dictList.AddItem("DaemonSet", "V1DaemonSet");
        // dictList.AddItem("Node", "V1Node");
        // dictList.AddItem("ReplicaSet", "V1ReplicaSet");
        dictList.AddItem("Job", "V1Job");
        dictList.AddItem("Service", "V1Service");
        dictList.AddItem("ServiceAccount", "V1ServiceAccount");
        dictList.AddItem("ClusterRole", "V1ClusterRole");
        dictList.AddItem("ClusterRoleBinding", "V1ClusterRoleBinding");
        dictList.AddItem("Role", "V1Role");
        dictList.AddItem("RoleBinding", "V1RoleBinding");
        dictList.AddItem("Ingress", "V1Ingress");
        dictList.AddItem("PersistentVolume", "V1PersistentVolume");
        dictList.AddItem("PersistentVolumeClaim", "V1PersistentVolumeClaim");
        dictList.AddItem("StorageClass", "V1StorageClass");
        dictList.AddItem("NetworkPolicy", "V1NetworkPolicy");
        dictList.AddItem("IngressClass", "V1IngressClass");
        dictList.AddItem("EndpointSlice", "V1EndpointSlice");
        dictList.AddItem("Endpoints", "V1Endpoints");
        dictList.AddItem("Secret", "V1Secret");
        dictList.AddItem("PriorityClass", "V1PriorityClass");
        dictList.AddItem("PodDisruptionBudget", "V1PodDisruptionBudget");
        dictList.AddItem("ValidatingWebhookConfiguration", "V1ValidatingWebhookConfiguration");
        dictList.AddItem("MutatingWebhookConfiguration", "V1MutatingWebhookConfiguration");
        dictList.AddItem("LimitRange", "V1LimitRange");
        dictList.AddItem("HorizontalPodAutoscaler", "V1HorizontalPodAutoscaler");
        dictList.AddItem("ResourceQuota", "V1ResourceQuota");
        dictList.AddItem("ConfigMap", "V1ConfigMap");
        dictList.AddItem("CronJob", "V1CronJob");
        dictList.AddItem("StatefulSet", "V1StatefulSet");
        dictList.AddItem("ReplicationController", "V1ReplicationController");

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

    public static  void Main()
    {
        new Program().makeTemplate();

    }
}
