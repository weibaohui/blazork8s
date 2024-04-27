using System;
using System.Collections.Generic;
using System.IO;
using Entity;
using k8s.Models;
using SqlSugar;

namespace Generator;

public class kubectExplainGen
{
    public static void Explain()
    {
        var dictList = new DictList();
        // dictList.AddItem("Node", typeof(V1Node));
        // dictList.AddItem("ReplicaSet", typeof(V1ReplicaSet));
        // dictList.AddItem("Deployment", typeof(V1Deployment));
        // dictList.AddItem("DaemonSet", typeof(V1DaemonSet));
        // dictList.AddItem("StatefulSet", typeof(V1StatefulSet));
        // dictList.AddItem("Job", typeof(V1Job));
        // dictList.AddItem("CronJob", typeof(V1CronJob));
        // dictList.AddItem("ReplicationController", typeof(V1ReplicationController));
        //
        // dictList.AddItem("ServiceAccount", typeof(V1ServiceAccount));
        // dictList.AddItem("ClusterRole", typeof(V1ClusterRole));
        // dictList.AddItem("ClusterRoleBinding", typeof(V1ClusterRoleBinding));
        // dictList.AddItem("Role", typeof(V1Role));
        // dictList.AddItem("RoleBinding", typeof(V1RoleBinding));
        //
        // dictList.AddItem("Service", typeof(V1Service));
        // dictList.AddItem("Ingress", typeof(V1Ingress));
        // dictList.AddItem("NetworkPolicy", typeof(V1NetworkPolicy));
        // dictList.AddItem("IngressClass", typeof(V1IngressClass));
        // dictList.AddItem("EndpointSlice", typeof(V1EndpointSlice));
        // dictList.AddItem("Endpoints", typeof(V1Endpoints));
        //
        // dictList.AddItem("PersistentVolume", typeof(V1PersistentVolume));
        // dictList.AddItem("PersistentVolumeClaim", typeof(V1PersistentVolumeClaim));
        // dictList.AddItem("StorageClass", typeof(V1StorageClass));
        //
        // dictList.AddItem("ConfigMap", typeof(V1ConfigMap));
        // dictList.AddItem("Secret", typeof(V1Secret));
        // dictList.AddItem("PriorityClass", typeof(V1PriorityClass));
        // dictList.AddItem("PodDisruptionBudget", typeof(V1PodDisruptionBudget));
        // dictList.AddItem("ValidatingWebhookConfiguration", typeof(V1ValidatingWebhookConfiguration));
        // dictList.AddItem("MutatingWebhookConfiguration", typeof(V1MutatingWebhookConfiguration));
        // dictList.AddItem("LimitRange", typeof(V1LimitRange));
        // dictList.AddItem("HorizontalPodAutoscaler", typeof(V1HorizontalPodAutoscaler));
        // dictList.AddItem("ResourceQuota", typeof(V1ResourceQuota));
        // dictList.AddItem("CustomResourceDefinition", typeof(V1CustomResourceDefinition));
        // dictList.AddItem("Pod", typeof(V1Pod));
        // dictList.AddItem("Lease", typeof(V1Lease));
        // dictList.AddItem("Namespace", typeof(V1Namespace));
        dictList.AddItem("Event", typeof(Corev1Event));

        Console.WriteLine("AddList Over");

        var list = new List<KubeType>();

        foreach (var dictionary in dictList.GetDictList())
        {
            var resourceName = (string)dictionary["Item"];
            var entityList = (IList<KubeType>)dictionary["Properties"];
            list.AddRange(entityList);
            Console.WriteLine(resourceName);
        }


        //使用TranslateService进行翻译

        var db = new kubectExplainGen().DB();
        var count = db.Queryable<KubeExplainRef>().ToList().Count;
        Console.WriteLine(count);

        foreach (var kt in list)
        {
            var rfCount = db.Queryable<KubeExplainRef>().Count(x => x.ExplainFiled == kt.ExplainFiled);
            if (rfCount > 0)
            {
                continue;
            }

            db.Insertable<KubeExplainRef>(new KubeExplainRef()
            {
                ExplainFiled = kt.ExplainFiled,
            }).ExecuteCommand();
        }
    }


    public SqlSugarClient DB()
    {
        var dbPath = Path.GetFullPath(Directory.GetCurrentDirectory() + "../../../../../BlazorApp/docs.db");
        Console.WriteLine(dbPath);
        //创建数据库对象 (用法和EF Dappper一样通过new保证线程安全)
        SqlSugarClient Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = $"DataSource={dbPath}",
                DbType = DbType.Sqlite,
                IsAutoCloseConnection = true
            },
            db =>
            {
                //5.1.3.24统一了语法和SqlSugarScope一样，老版本AOP可以写外面

                db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    Console.WriteLine(sql); //输出sql,查看执行sql 性能无影响


                    //获取原生SQL推荐 5.1.4.63  性能OK
                    Console.WriteLine(UtilMethods.GetNativeSql(sql, pars));

                    //获取无参数化SQL 对性能有影响，特别大的SQL参数多的，调试使用
                    //UtilMethods.GetSqlString(DbType.SqlServer,sql,pars)
                };

                //注意多租户 有几个设置几个
                //db.GetConnection(i).Aop
            });
        return Db;
    }
}
