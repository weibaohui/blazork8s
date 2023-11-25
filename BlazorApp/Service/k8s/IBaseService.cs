using k8s;

namespace BlazorApp.Service.k8s
{
    public interface IBaseService
    {
        public Kubernetes Client();
    }
}