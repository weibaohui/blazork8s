using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using k8s;

namespace BlazorApp.Service;

public interface IKubeService
{
    public Kubernetes         Client();
    public String             Name();
    public String             Version();
    public Task<List<string>> ListNs();
    public Task<List<string>> ListPodByNs(string? ns = "kube-system");
}
