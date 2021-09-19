using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using k8s.Models;

namespace Extension.k8s
{
    public static class EventExtension
    {
        public static IList<Corev1Event> EventsInTypePod(this Corev1EventList events)
        {
            return events.Items.Where(w => w.InvolvedObject.Kind == "Pod").ToList();
        }

        public static IList<Corev1Event> FilterByNodeName(this Corev1EventList events, string nodeName)
        {
            return events.Items.Where(x => x.InvolvedObject.Kind == "Node" && x.InvolvedObject.Name == nodeName)
                .ToList();
        }

        public static IList<Corev1Event>? FilterByUID(this Corev1EventList eventList, string uid)
        {
            if (!string.IsNullOrEmpty(uid))
            {
                return eventList.Items.Where(w => w.InvolvedObject.Uid == uid)
                    .OrderByDescending(w => w.Type)
                    .OrderByDescending(e => e.LastTimestamp)
                    .ToList();
            }

            return null;
        }
    }
}
