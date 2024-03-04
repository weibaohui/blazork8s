using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Utils;
using Entity.Analyze;
using k8s;
using k8s.Models;
using NCrontab;

namespace BlazorApp.Service.k8s.impl;

public class CronJobService(IKubeService kubeService) : CommonAction<V1CronJob>, ICronJobService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return await kubeService.Client().DeleteNamespacedCronJobAsync(name, ns);
    }

    public async Task<List<Result>> Analyze()
    {
        var items   = List();
        var results = new List<Result>();
        foreach (var item in items.ToList())
        {
            var failures = new List<Failure>();

            if (item.Spec.Suspend is true)
            {
                failures.Add(new Failure
                {
                    Text =
                        $"CronJob {item.Namespace()}/{item.Name()} is suspended ",
                });
            }
            else
            {
                if (item.Spec.StartingDeadlineSeconds != null && item.Spec.StartingDeadlineSeconds.Value < 0)
                {
                    //启动限时为负数
                    failures.Add(new Failure
                    {
                        Text =
                            $"CronJob {item.Namespace()}/{item.Name()} StartingDeadlineSeconds is negative ",
                    });
                }

                if (!ValidateCronExpression(item.Spec.Schedule))
                {
                    //cron表达式错误
                    failures.Add(new Failure
                    {
                        Text =
                            $"CronJob {item.Namespace()}/{item.Name()} cron {item.Spec.Schedule} is invalid ",
                    });
                }
            }


            if (failures.Count <= 0) continue;
            results.Add(Result.NewResult(item, failures));
        }

        if (results.Count == 0)
        {
            ClusterInspectionResultContainer.Instance.GetPassResources().Add("CronJob");
        }
        ClusterInspectionResultContainer.Instance.AddResourcesCount("CronJob", items.ToList().Count);
        return results;
    }


    /// <summary>
    /// 校验CronExpression
    /// </summary>
    /// <param name="cronExpression"></param>
    /// <returns></returns>
    static bool ValidateCronExpression(string cronExpression)
    {
        try
        {
            CrontabSchedule.Parse(cronExpression);
            return true;
        }
        catch (CrontabException)
        {
            // 如果解析失败，Cron 表达式无效
            return false;
        }
    }
}
