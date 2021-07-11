namespace server.Service.K8s
{
    public class Watcher
    {
        private readonly PodWatcher  _podWatcher;
        private readonly NodeWatcher _nodeWatcher;

        public Watcher(PodWatcher podWatcher, NodeWatcher nodeWatcher)
        {
            _podWatcher  = podWatcher;
            _nodeWatcher = nodeWatcher;
        }

        public void StartWatch()
        {
            var cli = Kubectl.Instance.Client();
             Kubectl.Instance.getNodes();
            _nodeWatcher.StartWatch(cli);
            _podWatcher.StartWatch(cli);
        }
    }
}
