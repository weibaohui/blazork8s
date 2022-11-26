using System;
using System.Collections.Generic;

namespace BlazorApp.Service;

public interface IKubeService
{
    public String       Name();
    public String       Version();
    public List<string> ListNs();
    public List<string> ListPodByNs(string? ns = "default");
}
