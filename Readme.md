[![Build](https://github.com/weibaohui/blazork8s/actions/workflows/BlazorApp.yml/badge.svg)](https://github.com/weibaohui/blazork8s/actions/workflows/server.yml)

使用C# Blazor 编写的kubernetes管理工具，集成了ChatGPT智能检测能力，用简单易用的操作界面，提升k8s管理效率。

<p align="center">
  <a href="https://github.com/weibaohui/blazork8s">
    <img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/BlazorApp/wwwroot/pro_icon.svg">
  </a>
</p>

<h1 align="center"> Blazor k8s </h1>

## ☀️ 授权协议

[![BlazorK8s](https://img.shields.io/badge/License-MIT-blue?style=flat-square)](https://github.com/weibaohui/blazork8s/blob/master/LICENSE)

# 1 k8s 体验

## 一键安装部署

```docker
kubectl apply -f https://raw.githubusercontent.com/weibaohui/blazork8s/main/deploy/deployment.yaml
```

## 访问

默认使用了nodePort开放，请访问31999端口
[http://NodePortIP:31999](http://127.0.0.1:31999)

# docker 体验

## 启动服务
使用docker-desktop需要自行处理apiserver的访问域名地址，请确保在docker内可访问
```docker
docker run -d --name blazork8s  -v ~/.kube/:/root/.kube/ -p 4001:443 -p 4000:80 ghcr.io/weibaohui/blazork8s:latest
```
## web

[web ui](http://localhost:4000)

# DEBUG 调试
```
 git clone git@github.com:weibaohui/blazork8s.git
 cd blazork8s
 cd BlazorApp
 dotnet watch run
```

## OpenAI 智能诊断 配置

修改BlazorApp目录下的appsettings.json

```
  "OpenAI": {
    "Enable": true, //是否开启
    "Token": "sk-xxx", //openai secret
    "Prompt": {
      "error": "简明扼要地用 Kubernetes 专家的身份判断一下这段输出有什么问题，要整齐列出问题对象和可能原因以及操作建议："
    }
  }
```

#预览
<p align="left">
对POD进行智能诊断
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/POD-analyze.gif">

     <img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/node.png">
     <img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/deploy.png">
     <img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/deploy-1.png">
     <img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/rs.png">
     <img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/rs-1.png">
     <img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/pod.png">
     <img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/pod-1.png">
基于Events进行智能诊断
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/events-advice.png">

</p>

#功能特性

* 新增 openAI gpt 问题诊断功能
    * Event 分析
    * Pod 分析
    * Deployment 分析
