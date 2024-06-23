[![Build](https://github.com/weibaohui/blazork8s/actions/workflows/BlazorApp.yml/badge.svg)](https://github.com/weibaohui/blazork8s/actions/workflows/server.yml)

<p align="center">
  <img src="/BlazorApp/wwwroot/pro_icon.svg" />
</p>

<h1 align="center">Blazor k8s</h1>

<div align="center">
  <a href="Readme.md">English</a> | <a href="Readme_cn.md">中文</a>
</div>

A Kubernetes management tool written in C# Blazor, integrating the ChatGPT large models. 
It features a user-friendly interface for easy and efficient Kubernetes administration. 
Particularly suitable for beginners, it offers various convenient functionalities to help novices grasp Kubernetes knowledge.


* Colorful and intuitive display of Kubernetes resources.
* Yaml-defined fields are analyzed and displayed in a tree structure, accompanied by documentation. Additionally, translation using a large model is available, eliminating concerns about forgetting definitions.
* Detailed explanations of Kubernetes resource fields, ensuring no ambiguity about the number of options and their meanings.Official documentation with clickable links.
* Integration of official examples in a directory tree format, allowing easy browsing, reference, and field copying.
* Support efficient editing of YAML resources, enabling writing YAML fields on one side of the page while referring to field definitions at the same time.
* Support displaying corresponding Service and Ingress on the Pod page, and support displaying backend Pods on the Service and Ingress pages.
* Generation of Yaml 、Problem analysis、Security checks using a large model.
* Support conversational k8s functionality operations, such as semantic commands to check the running status of pods in the default namespace.
* Display resource usage dynamically (requires installation of metric server), and support viewing detailed statistical data* Integration of page functionalities such as kubectl describe, kubectl explain, kubectl top and other high-frequency commands. These can be accessed with a simple click on the user interface.
* Inspection functionality added to the cluster page, conducting common error checks on major resource objects and providing detailed lists.
* Supports Chinese, English, as well as French, German, Italian, Russian, Spanish, French, Japanese, Korean, and 12  languages.
* Visualize the relationships and states between workload resources intuitively through a topology diagram.

## ☀️ License

[![BlazorK8s](https://img.shields.io/badge/License-MIT-blue?style=flat-square)](https://github.com/weibaohui/blazork8s/blob/master/LICENSE)

# k8s install

Create a small Kubernetes cluster using [KinD](https://kind.sigs.k8s.io/docs/user/quick-start/)、[MiniKube](https://minikube.sigs.k8s.io/docs/start/)

## KinD way

* install KinD on mac with `brew`.
```
brew install kind
```

* Create a new Kubernetes cluster. 

```
kind create cluster --name k8sgpt-demo
```

#  Deploy BlazorK8s to a cluster and experience it:

## kubectl apply yaml

```docker
kubectl apply -f https://raw.githubusercontent.com/weibaohui/blazork8s/main/deploy/deployment.yaml
```

* View the ui：
  By default, it uses NodePort for access. Please visit port 31999, or configure Ingress on your own.
  http://NodePortIP:31999

## Start a Docker image to experience it
### x86 Run
Note: When using Docker Desktop, you need to handle the access domain address of the API server yourself. Ensure that it is accessible within the Docker environment.
```docker
docker run -it --rm    -v ~/.kube/:/root/.kube/ -p 4000:8080 ghcr.io/weibaohui/blazork8s:0.2.5
```
### ARM Run (Mac M1/2/3)
```docker
docker run -it --rm    -v ~/.kube/:/root/.kube/ -p 4000:8080 ghcr.io/weibaohui/blazork8s:0.2.5-arm
```

* View：http://IP:4000 (!Do not use 127.0.0.1/localhost!)

# Debug

```
 git clone git@github.com:weibaohui/blazork8s.git
 cd blazork8s/BlazorApp
 dotnet watch run
```
# Interface Language Configuration
The interface defaults to displaying in Chinese. To set a default display in another language, modify the `appsettings.json` in the source code's `BlazorApp` directory or the image's `/app/appsettings.json`.
```
"SimpleI18n": {
"LocaleFilesPath": "wwwroot/lang",
"DefaultCultureName": "LANGUAGE"
}
```
The available values for `LANGUAGE` include
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
  "hi": "हिंदी (Hindi)",
  "it": "Italiano (Italian)"
}
```
# ChatGPT config 

* √ [MoonShot AI](https://kimi.moonshot.cn/)
* √ [Google Gemini](https://gemini.google.com/)
* √ [Ali Qwen](https://tongyi.aliyun.com/qianwen/)
* √ [iFlytek Spark](https://xinghuo.xfyun.cn/spark)
* √ [OpenAI](https://openai.com/)
* √ [One-API](https://github.com/songquanpeng/one-api) same as OpenAI
* √ [LM Studio](https://github.com/lmstudio-ai/lms)  same as OpenAI
* √ [Ollama](https://ollama.com/) same as OpenAI

Modify the `appsettings.json` in the BlazorApp directory or `/app/appsettings.json`  in the image.

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

## Effectiveness

### DocTree expands YAML definitions in a tree-like structure, no longer worry about forgetting the definitions.

<br>
  <img src="/docs/img/doc-tree.gif">
  <br>

### Explanation of Field Meanings

#### Click on the question mark next to the field on the resource details page.

* Use kubectl to obtain Kubernetes explanations
* Using a configured AI large model for intelligent interpretation, the results are as follows:
  <br>
  <img src="/docs/img/kubectl-explain.gif">
  <br>

### Generate deployment YAML

<br>
Obtain k8s deployment YAML through prompts and execute <br>
<img src="/docs/img/gpt-deploy.gif">
<br>

### Intelligent Analysis

Added intelligent analysis and security analysis buttons on each resource.
<br>
<img src="/docs/img/POD-analyze.gif">
<br>

### Workload Diagram
Visualize the relationships and states between workload resources intuitively through a topology diagram
<img src="/docs/img/deploy-diagram.jpg">
<br>

## List of inspectable resources

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

## UI preview

[click me](docs/ui.md)
