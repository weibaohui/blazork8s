using Generator;
using k8s.Models;

class Program
{
    public void makeTemplate()
    {
        var dictList = new DictList();
        // dictList.AddItem("Node", "V1Node");
        // dictList.AddItem("ReplicaSet", typeof(V1ReplicaSet));
        // dictList.AddItem("Service", "V1Service");
        // dictList.AddItem("Deployment",typeof(V1Deployment));
        dictList.AddItem("DaemonSet", typeof(V1DaemonSet));
        dictList.AddItem("Job", typeof(V1Job));
        dictList.AddItem("ClusterRole", typeof(V1ClusterRole));
        dictList.AddItem("ClusterRoleBinding", typeof(V1ClusterRoleBinding));
        dictList.AddItem("Role", typeof(V1Role));
        dictList.AddItem("RoleBinding", typeof(V1RoleBinding));
        dictList.AddItem("Ingress", typeof(V1Ingress));
        dictList.AddItem("PersistentVolume", typeof(V1PersistentVolume));
        dictList.AddItem("PersistentVolumeClaim", typeof(V1PersistentVolumeClaim));
        dictList.AddItem("StorageClass", typeof(V1StorageClass));
        dictList.AddItem("NetworkPolicy", typeof(V1NetworkPolicy));
        dictList.AddItem("IngressClass", typeof(V1IngressClass));
        dictList.AddItem("EndpointSlice", typeof(V1EndpointSlice));
        dictList.AddItem("Endpoints", typeof(V1Endpoints));
        dictList.AddItem("Secret", typeof(V1Secret));
        dictList.AddItem("PriorityClass", typeof(V1PriorityClass));
        dictList.AddItem("PodDisruptionBudget", typeof(V1PodDisruptionBudget));
        dictList.AddItem("ValidatingWebhookConfiguration", typeof(V1ValidatingWebhookConfiguration));
        dictList.AddItem("MutatingWebhookConfiguration", typeof(V1MutatingWebhookConfiguration));
        dictList.AddItem("LimitRange", typeof(V1LimitRange));
        dictList.AddItem("HorizontalPodAutoscaler", typeof(V1HorizontalPodAutoscaler));
        dictList.AddItem("ResourceQuota", typeof(V1ResourceQuota));
        dictList.AddItem("ConfigMap", typeof(V1ConfigMap));
        dictList.AddItem("CronJob", typeof(V1CronJob));
        dictList.AddItem("StatefulSet", typeof(V1StatefulSet));
        dictList.AddItem("ServiceAccount", typeof(V1ServiceAccount));
        dictList.AddItem("ReplicationController", typeof(V1ReplicationController));


        GeneratorHelper.Generator(dictList.GetDictList()).Run();
    }

    public static void Main()
    {
        // new Program().makeTemplate();
        // EntityPrepare.PrepareK8SEntity();
        // RazorEngineProcessor.Process();
        // Volume();
        kubectExplainGen.Explain();

    }

    public static void Volume()
    {
        var dictList = new DictList();
        dictList.AddItem("CustomResourceDefinition", typeof(V1CustomResourceDefinition));
        GeneratorHelper.Generator(dictList.GetDictList()).Run();

        // var list = EntityPrepare.GetK8SEntity(typeof(V1Volume), "Volume");
        // File.WriteAllText("volume.json",KubernetesJson.Serialize(list));
    }



}
