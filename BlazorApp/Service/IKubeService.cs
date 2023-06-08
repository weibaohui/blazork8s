using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using k8s;

namespace BlazorApp.Service;

public interface IKubeService
{
    public Kubernetes Client();
    public String     Name();

    public String Version();
}
