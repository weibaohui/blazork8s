[![Server Build](https://github.com/weibaohui/blazork8s/actions/workflows/server.yml/badge.svg)](https://github.com/weibaohui/blazork8s/actions/workflows/server.yml)
[![UI Build](https://github.com/weibaohui/blazork8s/actions/workflows/ui.yaml/badge.svg)](https://github.com/weibaohui/blazork8s/actions/workflows/ui.yaml)

#start server
```docker
docker run -d --name blazork8s  -v ~/.kube/:/root/.kube/ -p 4001:443 -p 4000:80 ghcr.io/weibaohui/blazork8s:latest
```
#ui
```docker
docker run -d --name blazork8s-ui -p 6088:80  ghcr.io/weibaohui/blazork8s-ui:latest
```
#web
[web ui](http://localhost:6088)
