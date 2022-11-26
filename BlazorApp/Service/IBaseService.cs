using System.Threading.Tasks;
using k8s;

namespace BlazorApp.Service
{
    public interface IBaseService
    {
        public Kubernetes Client();
    }
}
