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
* Yaml定义字段按树形展开分析，自带文档，且有可使用大模型进行翻译。再也不用担心记不住定义了。
* 详细的k8s资源字段解释，再也不用担心不知道这个字段有几个选项、都是什么意思了。
* 官方示例集成，以目录树的形式呈现k8s官方示例，可以随时浏览参考，复制字段了。
* 大模型生成yaml
* 大模型问题分析
* 大模型安全检测
* 资源用量动态展示（需安装metric server）
* 页面功能集成kubectl Describe、kubectl explain等高频命令，使用界面点击即可查看。
* 集群页面增加巡检功能，对主要资源对象的常见错误进行巡检，并给出明细列表。

## ☀️ 授权协议

[![BlazorK8s](https://img.shields.io/badge/License-MIT-blue?style=flat-square)](https://github.com/weibaohui/blazork8s/blob/master/LICENSE)

# k8s 集群安装
使用[KinD](https://kind.sigs.k8s.io/docs/user/quick-start/)、[MiniKube](https://minikube.sigs.k8s.io/docs/start/)安装一个小型k8s集群
## KinD方式
* 创建 KinD Kubernetes 集群
```
brew install kind
```
* 创建新的 Kubernetes 集群：
```
kind create cluster --name k8sgpt-demo
```

# 将blazorK8s 部署到集群中体验
## 安装脚本
```docker
kubectl apply -f https://raw.githubusercontent.com/weibaohui/blazork8s/main/deploy/deployment.yaml
```
* 访问：
默认使用了nodePort开放，请访问31999端口。或自行配置Ingress
[http://NodePortIP:31999](http://127.0.0.1:31999)

# 使用docker启动镜像进行体验
## 启动服务
使用docker-desktop需要自行处理apiserver的访问域名地址，请确保在docker内可访问
```docker
docker run -it --rm    -v ~/.kube/:/root/.kube/ -p 4000:8080 ghcr.io/weibaohui/blazork8s:0.0.9
```
* 访问：[web ui](http://127.0.0.1:4000)


# 源码 DEBUG 调试
```
 git clone git@github.com:weibaohui/blazork8s.git
 cd blazork8s/BlazorApp
 dotnet watch run
```

# 大模型 配置
* √ 阿里云通义千问
* √ 科大讯飞星火大模型
* √ openAI
* 未完待续 (百度等模型...)

修改源码BlazorApp目录下的appsettings.json
或镜像/app/目录下的appsettings.json
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

## 大模型应用效果

### DocTree树状展开yaml定义，再也不用担心记不住定义了
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

### 智能分析
在每一个资源上面都增加了智能分析、安全分析两个按钮。
<br>
 <img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/POD-analyze.gif">
<br>

## 页面预览

[click me](ui.md)

## 巡检支持资源情况
* Node
* Pod
* Deployment
* StatefulSet
* ReplicaSet
* CronJob
* Ingress
