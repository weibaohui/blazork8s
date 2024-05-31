[![Build](https://github.com/weibaohui/blazork8s/actions/workflows/BlazorApp.yml/badge.svg)](https://github.com/weibaohui/blazork8s/actions/workflows/server.yml)


<p align="center">
  <img src="/BlazorApp/wwwroot/pro_icon.svg" />
</p>

<h1 align="center">Blazor k8s</h1>

<div align="center">
  <a href="Readme.md">English</a> | <a href="Readme_cn.md">中文</a>
</div>

使用C# Blazor 编写的kubernetes管理工具，集成了ChatGPT类大模型，用简单易用的操作界面，提升k8s管理效率。
尤其适合新手入门使用，提供多种便捷功能方便初学者掌握k8s知识。

* 多彩直观显示k8s资源
* Yaml定义字段按树形展开分析，自带文档，且有可使用大模型进行翻译。再也不用担心记不住定义了。
* 详细的k8s资源字段解释，再也不用担心不知道这个字段有几个选项、都是什么意思了。可链接访问官方文档。
* 官方示例集成，以目录树的形式呈现k8s官方示例，可以随时浏览参考，复制字段了。
* 支持高效编辑资源Yaml，在一个页面内可以一边写yaml字段，一边查字段定义了。
* 支持Pod页面关联显示对应的Service、Ingress，支持Service、Ingress页面展示后端Pod。
* 大模型生成yaml、大模型问题分析、大模型安全检测。
* 支持对话式k8s功能操作，如请检查default命名空间下的pod运行状态等语义化命令。
* 资源用量动态展示（需安装metric server），支持统计数据详情查看。
* 页面功能集成kubectl describe、kubectl explain、kubectl top等高频命令，使用界面点击即可查看。
* 集群页面增加巡检功能，对主要资源对象的常见错误进行巡检，并给出巡检结果明细列表。
* 支持中文、英文以及法语、德语、意大利语、俄语、西班牙语、法语、日语、韩语等12国语言。
* 支持通过拓扑图直观展示workload资源之间的关系以及状态
## ☀️ 授权协议

[![BlazorK8s](https://img.shields.io/badge/License-MIT-blue?style=flat-square)](https://github.com/weibaohui/blazork8s/blob/master/LICENSE)

# k8s 集群安装

使用[KinD](https://kind.sigs.k8s.io/docs/user/quick-start/)、[MiniKube](https://minikube.sigs.k8s.io/docs/start/)
安装一个小型k8s集群

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
kubectl apply -f https://gitee.com/weibaohui/blazork8s/raw/main/deploy/deployment.yaml
```

* 访问：
  默认使用了nodePort开放，请访问31999端口。或自行配置Ingress
  http://NodePortIP:31999

# 使用docker启动镜像进行体验

## 启动服务

使用docker-desktop需要自行处理apiserver的访问域名地址，请确保在docker内可访问
### X86架构环境
```docker
docker run -it --rm    -v ~/.kube/:/root/.kube/ -p 4000:8080 ghcr.io/weibaohui/blazork8s:0.2.2
```
### ARM Run (Mac M1/2/3等ARM架构)
```docker
docker run -it --rm    -v ~/.kube/:/root/.kube/ -p 4000:8080 ghcr.io/weibaohui/blazork8s:0.2.2-arm
```

* 访问：http://IP:4000  (!不要使用 127.0.0.1/localhost!)

# 源码 DEBUG 调试

```
 git clone git@github.com:weibaohui/blazork8s.git
 cd blazork8s/BlazorApp
 dotnet watch run
```
# 界面语言配置
 界面默认显示为中文。如需默认显示其他语言，请修改源码`BlazorApp`目录下的`appsettings.json`或镜像`/app/appsettings.json`
```
 "SimpleI18n": {
    "LocaleFilesPath": "wwwroot/lang",
    "DefaultCultureName": "LANGUAGE"
  }
```
`LANGUAGE`可选值包括
```json
{
  "en-US": "English",
  "zh-CN": "中文(Chinese)",
  "es": "Español (Spanish)",
  "ru": "Русский (Russian)",
  "pt-br": "Português (Portuguese)",
  "pl": "Polski (Polish)",
  "ko": "한국어 (Korean)",
  "ja": "日本語 (Japanese)",
  "fr": "Français (French)",
  "de": "Deutsch (German)",
  "hi": "ह\u093fन\u094dद\u0940 (Hindi)",
  "it": "Italiano (Italian)"
}
```
# 大模型 配置

* √ [月之暗面 MoonShot AI](https://kimi.moonshot.cn/)
* √ [谷歌 Gemini](https://gemini.google.com/)
* √ [阿里云通义千问](https://tongyi.aliyun.com/qianwen/)
* √ [科大讯飞星火大模型](https://xinghuo.xfyun.cn/spark)
* √ [OpenAI](https://openai.com/)
* √ [One-API](https://github.com/songquanpeng/one-api) 使用OpenAI接口
* √ [LM Studio](https://github.com/lmstudio-ai/lms) 使用OpenAI接口
* √ [Ollama](https://ollama.com/) 使用OpenAI接口

修改源码BlazorApp目录下的appsettings.json
或镜像/app/目录下的appsettings.json

```
   "AI": {
    "Enable": true, //enabled
    "Select": "OpenAI" //choose a model from below
  },
  "OpenAI": {
    "Token": "sk-xxx",
    "Model": "gpt-3.5-turbo",
    "BaseUrl": "https://api.openai.com/v1"
  },
  "GeminiAI": {
    "APIKey": "AIxxxxxxx7dd3494880a7920axxxxxxxxx",
    "Model": "gemini-pro"
  }
```

## 大模型应用效果

### DocTree树状展开yaml定义，再也不用担心记不住定义了

<br>
  <img src="/docs/img/doc-tree.gif">
  <br>

### 字段含义解释

#### 点击资源详情页面上，字段前面的问号

* 使用kubectl 获取k8s解释
* 使用配置的AI大模型，进行智能解释，效果如下：
  <br>
  <img src="/docs/img/kubectl-explain.gif">
  <br>

### 生成部署yaml

<br>
通过提示词获得k8s部署yaml，并执行<br>
<img src="/docs/img/gpt-deploy.gif">
<br>

### 智能分析

在每一个资源上面都增加了智能分析、安全分析两个按钮。
<br>
<img src="/docs/img/POD-analyze.gif">
<br>
## 资源拓扑展示
<img src="/docs/img/deploy-diagram.jpg">
<br>
## 巡检支持资源情况

* Node
* Pod
* Deployment
* StatefulSet
* ReplicaSet
* CronJob
* Ingress
* Service/Endpoints
* PersistentVolumeClaim
* NetworkPolicy
* HorizontalPodAutoscaler
  <br>
  <img src="/docs/img/cluster-inspection.png">
  <br>

## 页面预览
[click me](docs/ui.md)
