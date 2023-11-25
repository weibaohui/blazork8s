using System.Collections.Generic;
using System.Linq;
using k8s.Models;

namespace Extension.k8s
{
    public static class EventExtension
    {
        public static IList<Corev1Event> EventsInTypePod(this IList<Corev1Event> events)
        {
            return events.Where(w => w.InvolvedObject.Kind == "Pod").ToList();
        }

        public static IList<Corev1Event> FilterBySourceHost(this IList<Corev1Event> events, string nodeName)
        {
            return events.Where(x => x.Source.Host == nodeName)
                .ToList();
        }

        public static IList<Corev1Event>? FilterByUID(this IList<Corev1Event> eventList, string uid)
        {
            if (!string.IsNullOrEmpty(uid))
            {
                return eventList.Where(w => w.InvolvedObject.Uid == uid)
                    .OrderByDescending(w => w.Type)
                    .OrderByDescending(e => e.LastTimestamp)
                    .ToList();
            }

            return new List<Corev1Event>();
        }
    }
}
