[![Build](https://github.com/weibaohui/blazork8s/actions/workflows/BlazorApp.yml/badge.svg)](https://github.com/weibaohui/blazork8s/actions/workflows/server.yml)



<p align="center">
  <a href="https://github.com/weibaohui/blazork8s">
    <img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/BlazorApp/wwwroot/pro_icon.svg">
  </a>
</p>

<h1 align="center"> Blazor k8s </h1>

## ☀️ 授权协议

[![BlazorK8s](https://img.shields.io/badge/License-MIT-blue?style=flat-square)](https://github.com/weibaohui/blazork8s/blob/master/LICENSE)


# 1 k8s部署体验
## 一键安装
```docker
kubectl apply -f https://raw.githubusercontent.com/weibaohui/blazork8s/main/deploy/deployment.yaml
```
## 访问
默认使用了nodePort开放，请访问31999端口
[http://NodePortIP:31999](http://127.0.0.1:31999)

# docker 体验
## start server
```docker
docker run -d --name blazork8s  -v ~/.kube/:/root/.kube/ -p 4001:443 -p 4000:80 ghcr.io/weibaohui/blazork8s:latest
```
## web
[web ui](http://localhost:4000)



#预览
<p align="left">
     <img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/node.png">
     <img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/deploy.png">
     <img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/deploy-1.png">
     <img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/rs.png">
     <img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/rs-1.png">
     <img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/pod.png">
     <img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/pod-1.png">
 </p>

#功能特性
* 新增 openAI gpt 问题诊断功能
