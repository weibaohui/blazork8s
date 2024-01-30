[![Build](https://github.com/weibaohui/blazork8s/actions/workflows/BlazorApp.yml/badge.svg)](https://github.com/weibaohui/blazork8s/actions/workflows/server.yml)


<p align="center">
  <a href="https://github.com/weibaohui/blazork8s">
    <img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/BlazorApp/wwwroot/pro_icon.svg">
  </a>
  <h1 align="center"> Blazor k8s </h1>
</p>

使用C# Blazor 编写的kubernetes管理工具，集成了ChatGPT类大模型，用简单易用的操作界面，提升k8s管理效率。
尤其适合新手入门使用，提供多种便捷功能方便初学者掌握k8s知识。

* 多彩直观显示k8s资源
* Yaml定义字段按树形展开分析，自带文档，且有可使用大模型进行翻译
* 详细的k8s资源字段解释
* 大模型生成yaml
* 大模型问题分析
* 大模型安全检测
* 大模型k8s资源字段定义解释
* 资源用量动态展示（需安装metric server）

## ☀️ 授权协议

[![BlazorK8s](https://img.shields.io/badge/License-MIT-blue?style=flat-square)](https://github.com/weibaohui/blazork8s/blob/master/LICENSE)

# k8s 体验

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
 cd blazork8s/BlazorApp
 dotnet watch run
```

## 大模型 配置

* √ 阿里云通义千问
* √ 科大讯飞星火大模型
* √ openAI
* 未完待续 (百度等模型...)

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

## 大模型加持

### DocTree展开解释yaml含义
<br>
  <img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/doc-tree.gif">
  <br>

### 字段含义解释

#### 点击资源详情页面上，字段前面的问号

* 使用kubectl 获取k8s解释
* 使用配置的AI大模型，进行智能解释，效果如下：
  <br>
  <img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/kubectl-explain.gif">
  <br>

### 生成部署yaml

<br>
通过提示词获得k8s部署yaml，并执行<br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/gpt-deploy.gif">
<br>

### POD智能分析

<br>
 <img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/POD-analyze.gif">
<br>

## 页面预览

[click me](ui.md)
