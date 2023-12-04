using System;
using System.Collections.Generic;
using k8s.Models;

namespace BlazorApp.Service.k8s
{
    public interface IPodService : ICommonAction<V1Pod>
    {
        IEnumerable<Tuple<string, int>> NodePodsNum();

        IList<V1Pod> ListByNodeName(string nodeName);


    }
}
