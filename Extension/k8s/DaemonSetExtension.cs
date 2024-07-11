using k8s.Models;

namespace Extension.k8s;

public static class DaemonSetExtension
{
    public static bool IsReady(this V1DaemonSetStatus status)
    {
        if (status.DesiredNumberScheduled == status.NumberAvailable &&
            status.DesiredNumberScheduled == status.NumberReady)
        {
            return true;
        }

        return false;
    }
}
