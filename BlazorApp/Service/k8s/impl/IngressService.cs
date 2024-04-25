using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Utils;
using Entity.Analyze;
using Extension;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class IngressService(
    IKubeService kubeService,
    IServiceService svcService,
    ISecretService secretService,
    IIngressClassService ingressClassService)
    : CommonAction<V1Ingress>, IIngressService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return await kubeService.Client().DeleteNamespacedIngressAsync(name, ns);
    }

    public Task<List<Result>> Analyze()
    {
        var items = List();
        var results = new List<Result>();
        foreach (var item in items.ToList())
        {
            var failures = new List<Failure>();


            var ingressClassName = item.Spec.IngressClassName;
            var anIngValue = (item.Metadata?.Annotations.ToList())
                .FirstOrDefault(x => x.Key == "kubernetes.io/ingress.class").Value;


            //1 spec.ingressClassName 跟 annotation kubernetes.io/ingress.class 不一致
            if (ingressClassName != anIngValue)
            {
                failures.Add(new Failure
                {
                    Text =
                        $"Ingress {item.Namespace()}/{item.Name()} should set spec.ingressClassName and annotation kubernetes.io/ingress.class both to {ingressClassName} {anIngValue}",
                });
            }


            //2 检查ingressClassName是否存在
            if (ingressClassName.IsNullOrWhiteSpace() && !anIngValue.IsNullOrWhiteSpace())
            {
                ingressClassName = anIngValue;
            }

            var ingressClass = ingressClassService.GetByName(ingressClassName);
            if (ingressClass == null)
            {
                failures.Add(new Failure
                {
                    Text =
                        $"Ingress {item.Namespace()}/{item.Name()} spec.ingressClassName {ingressClassName} not exists",
                });
            }

            //3 检查ingress rules
            if (item.Spec.Rules is { Count: > 0 })
            {
                foreach (var rule in item.Spec.Rules)
                {
                    if (rule.Http == null) continue;
                    if (rule.Http.Paths is not { Count: > 0 }) continue;
                    foreach (var path in rule.Http.Paths)
                    {
                        //检查svcName是否存在
                        var svcName = path.Backend?.Service?.Name;
                        if (svcName.IsNullOrWhiteSpace())
                        {
                            continue;
                        }

                        var svc = svcService.GetByName(item.Namespace(), svcName);
                        if (svc == null)
                        {
                            failures.Add(new Failure
                            {
                                Text =
                                    $"Ingress {item.Namespace()}/{item.Name()} uses the svc  {item.Namespace()}/{svcName} not exists",
                            });
                        }
                    }
                }
            }

            //4 检查TLS证书
            if (item.Spec.Tls is { Count: > 0 })
            {
                foreach (var tls in item.Spec.Tls)
                {
                    if (tls.SecretName.IsNullOrWhiteSpace()) continue;
                    var secret = secretService.GetByName(item.Namespace(), tls.SecretName);
                    if (secret == null)
                    {
                        failures.Add(new Failure
                        {
                            Text =
                                $"Ingress {item.Namespace()}/{item.Name()}  uses the secret {item.Namespace()}/{tls.SecretName} as a TLS certificate which does not exist.",
                        });
                    }
                }
            }

            if (failures.Count <= 0) continue;
            results.Add(Result.NewResult(item, failures));
        }

        if (results.Count == 0)
        {
            ClusterInspectionResultContainer.Instance.GetPassResources().Add("Ingress");
        }

        ClusterInspectionResultContainer.Instance.AddResourcesCount("Ingress", items.ToList().Count);

        return Task.FromResult(results);
    }

    public IList<V1Ingress> ListByServiceList(IList<V1Service> services)
    {
        var list = new List<V1Ingress>();
        foreach (var svc in services)
        {
            var ns = svc.Namespace();
            var name = svc.Name();
            var result = List().Where(x => x.Namespace() == ns)
                .Where(x => x.Spec.Rules is { Count: > 0 } && x.Spec.Rules.Any(
                    y => y.Http.Paths is { Count: > 0 } && y.Http.Paths.Any(
                        z => z.Backend?.Service?.Name == name
                    ))).ToList();
            list.AddRange(result);
        }

        return list;
    }

    public string GetRulePathDisplayUrl(V1IngressRule rule, V1HTTPIngressPath path, IList<V1IngressTLS> specTls)
    {
        var pathDisplay = "";
        if (specTls is { Count: > 0 })
            pathDisplay += "https://";
        else
            pathDisplay += "http://";

        if (rule?.Host != null && !string.IsNullOrWhiteSpace(rule?.Host))
            pathDisplay += $"{rule.Host}";
        else
            pathDisplay += "*";

        if (path?.Path != null) pathDisplay += $"{path.Path}";


        return pathDisplay;
    }

    public string GetRulePathBackend(V1HTTPIngressPath path)
    {
        var pathDisplay = "";

        if (path?.Backend?.Service?.Name != null && !string.IsNullOrWhiteSpace(path.Backend?.Service?.Name))
            pathDisplay += $"{path.Backend?.Service?.Name}";

        if (path?.Backend?.Service?.Port?.Number != null) pathDisplay += $":{path.Backend?.Service?.Port?.Number}";

        return pathDisplay;
    }

    public V1ObjectReference GetRulePathBackend(V1HTTPIngressPath path, string ns)
    {
        return KubeHelper.GetObjectRef("Service", ns, path.Backend?.Service?.Name);
    }
}
