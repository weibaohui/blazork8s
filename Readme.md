[![Docker Image CI](https://github.com/weibaohui/blazork8s/actions/workflows/docker-image.yml/badge.svg)](https://github.com/weibaohui/blazork8s/actions/workflows/docker-image.yml)

#start server
```docker
docker run -d --name blazork8s  -v ~/.kube/:/root/.kube/ -p 4001:443 -p 4000:80 ghcr.io/weibaohui/blazork8s:latest
```
#ui
```docker
docker run -d --name blazork8s-ui -p 6088:80  ghcr.io/weibaohui/blazork8s:ui
```
#web
[web ui](http://localhost:6088)
