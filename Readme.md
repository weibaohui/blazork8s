[![Docker Image CI](https://github.com/weibaohui/blazork8s/actions/workflows/docker-image.yml/badge.svg)](https://github.com/weibaohui/blazork8s/actions/workflows/docker-image.yml)
#start server
```c#
docker run -d --name blazork8s-server -v ~/.kube/:/root/.kube/  weibh/blazork8s-server:test 
```
