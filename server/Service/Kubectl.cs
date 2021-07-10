using System;
using k8s;

namespace server.Service
{
    public class Kubectl
    {
        private static readonly Lazy<Kubectl> Lazy = new Lazy<Kubectl>(() => new Kubectl());

        public static Kubectl Instance => Lazy.Value;

        private IKubernetes _client = null;

        public IKubernetes Client()
        {
            if (_client == null)
            {
                var config = KubernetesClientConfiguration.BuildConfigFromConfigFile();
                _client = new Kubernetes(config);
            }
            return _client;
        }
    }

}
