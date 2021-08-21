﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using k8s.Models;

namespace Extension.k8s
{
    public static class EventExtension
    {
        public static IList<Corev1Event> EventsInTypePod(this Corev1EventList events )
        {
            return events.Items.Where(w => w.InvolvedObject.Kind == "Pod").ToList();
        }
    }
}
