using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Utils;
using Entity.Analyze;
using Extension;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl
{
    public class PodService(IKubeService kubeService, IEventService eventService) : CommonAction<V1Pod>, IPodService
    {
        public async Task<IList<V1Pod>> FilterPodByLabels(string ns, string labels)
        {
            var pods = await kubeService.Client()
                .ListNamespacedPodAsync(namespaceParameter: ns, labelSelector: labels);
            return pods?.Items;
        }

        public async Task<IList<V1Pod>> FilterPodByLabels(string ns, IDictionary<string, string> labels)
        {
            var filter = PodSelectorHelper.ToFilter(labels);
            return await FilterPodByLabels(ns, filter);
        }

        public async Task<IList<V1Pod>> FilterPodByLabelsForAllNamespace(string labels)
        {
            var pods = await kubeService.Client()
                .ListPodForAllNamespacesAsync(labelSelector: labels);
            return pods?.Items;
        }

        public async Task<IList<V1Pod>> FilterPodByLabelsForAllNamespace(IDictionary<string, string> labels)
        {
            var filter = PodSelectorHelper.ToFilter(labels);
            return await FilterPodByLabelsForAllNamespace(filter);
        }

        public IList<V1Pod> ListByNodeName(string nodeName)
        {
            var list = List();
            return list.Where(x => x.Spec.NodeName == nodeName)
                .ToList();
        }

        public IEnumerable<Tuple<string, int>> NodePodsNum()
        {
            var pods = List();
            var tuples = pods.GroupBy(s => s.Spec.NodeName)
                .OrderBy(g => g.Key)
                .Select(g => Tuple.Create(g.Key, g.Count()));


            return tuples;
        }

        public new async Task<object> Delete(string ns, string name)
        {
            return await kubeService.Client().DeleteNamespacedPodAsync(name, ns);
        }

        public async Task<List<Result>> Analyze()
        {
            var items   = List();
            var results = new List<Result>();
            foreach (var pod in items.ToList())
            {
                var failures = new List<Failure>();
                //检查Pending Pods
                if (pod.Status.Phase == "Pending")
                {
                    if (pod.Status.Conditions is { Count: > 0 })
                    {
                        failures.AddRange(
                            from status in pod.Status.Conditions
                            where status.Type == "PodScheduled" && status.Reason == "Unschedulable"
                            where !status.Message.IsNullOrWhiteSpace()
                            select new Failure { Text = status.Message });
                    }
                }

                if (pod.Status.Conditions is { Count: > 0 })
                {
                    foreach (var condition in pod.Status.Conditions.ToList())
                    {
                        if (condition.Type == "ContainersReady" && condition.Status == "False")
                        {
                            //检查是否因为job complated而导致的
                            //任何一个container是这个情况，都会导致进入此分支
                            if (!pod.Status.ContainerStatuses.Any(x =>
                                    x.Ready == false && x.State?.Terminated?.Reason == "Completed"))
                                failures.Add(new Failure()
                                {
                                    Text = condition.Message
                                });
                        }
                    }
                }

                if (pod.Status.ContainerStatuses is { Count: > 0 })
                {
                    //通过遍历pod container status 判断是否正常
                    foreach (var status in pod.Status.ContainerStatuses)
                    {
                        if (status.State.Waiting != null)
                        {
                            if (IsErrorReason(status.State.Waiting.Reason) &&
                                !status.State.Waiting.Message.IsNullOrWhiteSpace())
                            {
                                failures.Add(new Failure()
                                {
                                    Text = status.State.Waiting.Message
                                });
                            }

                            switch (status.State.Waiting.Reason)
                            {
                                case "ContainerCreating" when pod.Status.Phase == "Pending":
                                {
                                    var latestEvent =
                                        await eventService.GetInvolvingObjectLatestEvent(pod.Namespace(), pod.Name());
                                    if (latestEvent == null)
                                    {
                                        continue;
                                    }

                                    if (IsEvtErrorReason(latestEvent.Reason) &&
                                        !latestEvent.Message.IsNullOrWhiteSpace())
                                    {
                                        failures.Add(new Failure()
                                        {
                                            Text = latestEvent.Message
                                        });
                                    }

                                    break;
                                }
                                case "CrashLoopBackOff":
                                    failures.Add(new Failure()
                                    {
                                        Text =
                                            $"the last termination reason is {status.LastState.Terminated.Reason},container={status.Name},pod={pod.Name()}"
                                    });
                                    break;
                            }
                        }
                        else
                        {
                            //ReadinessProbe失败的情况
                            if (pod.Status.Phase == "Running" && !status.Ready)
                            {
                                var evt = await eventService.GetInvolvingObjectLatestEvent(pod.Namespace(), pod.Name());
                                if (evt == null)
                                {
                                    continue;
                                }

                                if (evt.Reason == "Unhealthy" && !evt.Message.IsNullOrWhiteSpace())
                                {
                                    failures.Add(new Failure()
                                    {
                                        Text = evt.Message
                                    });
                                }
                            }
                        }
                    }
                }


                if (failures.Count <= 0) continue;
                results.Add(Result.NewResult(pod, failures));
            }

            if (results.Count == 0)
            {
                ClusterInspectionResultContainer.Instance.GetPassResources().Add("Pod");
            }

            ClusterInspectionResultContainer.Instance.AddResourcesCount("Pod", items.ToList().Count);

            return results;
        }


        private static bool IsErrorReason(string reason)
        {
            List<string> failureReasons =
            [
                "CrashLoopBackOff",
                "ImagePullBackOff",
                "CreateContainerConfigError",
                "PreCreateHookError",
                "CreateContainerError",
                "PreStartHookError",
                "RunContainerError",
                "ImageInspectError",
                "ErrImagePull",
                "ErrImageNeverPull",
                "InvalidImageName"
            ];

            return failureReasons.Contains(reason);
        }

        private static bool IsEvtErrorReason(string reason)
        {
            List<string> failureReasons = ["FailedCreatePodSandBox", "FailedMount"];

            return failureReasons.Contains(reason);
        }
    }
}
