using System;
using k8s.Models;

namespace ui.Service
{
    public interface IEventService
    {
        Task<V1beta1EventList> List();
    }
}
