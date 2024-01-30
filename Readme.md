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
docker run -d --name blazork8s  -v ~/.kube/:/root/.kube/ -p 4001:443 -p 4000:8080 ghcr.io/weibaohui/blazork8s:latest
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

## AI 智能诊断 配置

修改BlazorApp目录下的appsettings.json
```
  "AI": {
    "Enable": true, //是否开启
    "Select": "QwenAI" //选择哪一个大模型。可选阿里通义千问、科大讯飞星火大模型
  },
   "QwenAI": {
    "APIKey": "sk-xxxxxxx7dd3494880a7920axxxxxxxxx",
    "Prompt": {
      "error": "简明扼要地用 Kubernetes 专家的身份判断一下这段输出有什么问题，要整齐列出问题对象和可能原因以及操作建议：",
      "security": "简明扼要地用Kubernetes安全专家的身份判断一下这段输出有什么问题，要整齐列出问题对象和可能原因以及操作建议:"
    }
  },
  "XunFeiAI": {
    "APPID": "xxxxxx",
    "APISecret": "XXXjYzgzY2E0ZTkwxxxxxxYxMDJkYTBl",
    "APIKey": "xxxxxxx7dd3494880a7920axxxxxxxxx",
    "Prompt": {
      "error": "简明扼要地用 Kubernetes 专家的身份判断一下这段输出有什么问题，要整齐列出问题对象和可能原因以及操作建议：",
      "security": "简明扼要地用Kubernetes安全专家的身份判断一下这段输出有什么问题，要整齐列出问题对象和可能原因以及操作建议:"
    }
  },
```
## AI 智能翻译
### 点击资源详情页面上，字段前面的问号
* 使用kubectl 获取k8s解释
* 使用配置的AI大模型，进行智能解释，效果如下
  <img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/kubectl-explain.gif">



#预览
<p align="left">
对POD进行智能诊断<br>
 <img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/POD-analyze.gif">
通过提示词获得k8s部署yaml，并执行<br>
     <img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/gpt-deploy.gif">
<br>


<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/cluster-1.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/cluster-2.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/cluster-3.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/cluster-role-2.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/cluster-role-binding.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/cluster-role-bingding2.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/cluster-role.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/deploy-1.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/deploy-2.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/deploy-3.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/deploy.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/detail-1.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/detail-2.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/ds.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/ep.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/epslice.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/epsliece-1.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/events-advice.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/gpt-deploy.gif"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/hpa-2.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/hpa.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/ingress-2.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/ingress-class-1.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/ingress-class-2.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/ingress-class-3.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/ingress.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/job.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/job2.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/kubectl-explain.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/limit-range-2.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/limitrange-1.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/node.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/ns.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/pdb-2.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/pdb.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/pod-1.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/pod-2.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/pod-3.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/pod-4.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/pod.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/portforward-2.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/portforward.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/priorityclass.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/pv-2.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/pv.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/pvc-1.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/pvc-2.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/resourcequota.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/role-2.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/role.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/rolebinding-1.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/rolebinding-2.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/rs-1.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/rs.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/sa-2.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/sa.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/secret-1.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/secret.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/storage-class.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/storageclass-2.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/svc-1.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/svc.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/MutatingWebhookConfiguration.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/POD-analyze.gif"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/ValidatingWebhookConfiguration-1.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/ValidatingWebhookConfiguration.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/cm.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/crd-2.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/crd.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/cronjob-2.png"><br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/cronjob.png"><br>
基于Events进行智能诊断<br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/events-advice.png">

</p>

#功能特性

* 新增 openAI gpt 问题诊断功能
    * Event 分析
    * Pod 分析
    * Deployment 分析
