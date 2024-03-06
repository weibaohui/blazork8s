[![Build](https://github.com/weibaohui/blazork8s/actions/workflows/BlazorApp.yml/badge.svg)](https://github.com/weibaohui/blazork8s/actions/workflows/server.yml)


<p align="center">
  <a href="https://github.com/weibaohui/blazork8s">
    <img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/BlazorApp/wwwroot/pro_icon.svg">
  </a>
  <h1 align="center"> Blazor k8s </h1>
  <h4 align="center"> 
   <a href="https://github.com/weibaohui/blazork8s/blob/main/Readme.md">English</a>
    <a href="https://gitee.com/weibaohui/blazork8s/blob/main/Readme_cn.md">中文</a>
 </h4>
</p>
A Kubernetes management tool written in C# Blazor, integrating the ChatGPT large models. 
It features a user-friendly interface for easy and efficient Kubernetes administration. 
Particularly suitable for beginners, it offers various convenient functionalities to help novices grasp Kubernetes knowledge.


* Colorful and intuitive display of Kubernetes resources.
* Yaml-defined fields are analyzed and displayed in a tree structure, accompanied by documentation. Additionally, translation using a large model is available, eliminating concerns about forgetting definitions.
* Detailed explanations of Kubernetes resource fields, ensuring no ambiguity about the number of options and their meanings.
* Integration of official examples in a directory tree format, allowing easy browsing, reference, and field copying.
* Generation of Yaml using a large model.
* Problem analysis using a large model.
* Security checks using a large model.
* Dynamic display of resource usage (requires installation of the metric server).
* Integration of page functionalities such as kubectl Describe, kubectl explain, and other high-frequency commands. These can be accessed with a simple click on the user interface.
* Inspection functionality added to the cluster page, conducting common error checks on major resource objects and providing detailed lists.

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
  [http://NodePortIP:31999](http://127.0.0.1:31999)

# Start a Docker image to experience it
## Run
Note: When using Docker Desktop, you need to handle the access domain address of the API server yourself. Ensure that it is accessible within the Docker environment.
```docker
docker run -it --rm    -v ~/.kube/:/root/.kube/ -p 4000:8080 ghcr.io/weibaohui/blazork8s:0.1.2
```

* View：[web ui](http://127.0.0.1:4000)

# Debug

```
 git clone git@github.com:weibaohui/blazork8s.git
 cd blazork8s/BlazorApp
 dotnet watch run
```

# ChatGPT config 

* √ ali Qwen
* √ iFlytek Spark
* √ OpenAI
* TBD (Baidu and other models...)

Modify the appsettings.json in the BlazorApp directory or /app/ directory of the image.

```
  "AI": {
    "Enable": true, //enabled
    "Select": "QwenAI" //choose a model
  },
   "QwenAI": {
    "APIKey": "sk-xxxxxxx7dd3494880a7920axxxxxxxxx",
    "Prompt": {
      "error": "Concisely, in the role of a Kubernetes expert, assess the provided output for any issues. Clearly list the problematic components, potential causes, and recommend appropriate actions:",
      "security": "Concisely, in the role of a Kubernetes expert, assess the provided output for any issues. Clearly list the problematic components, potential causes, and recommend appropriate actions:"
    }
  },
  "XunFeiAI": {
    "APPID": "xxxxxx",
    "APISecret": "XXXjYzgzY2E0ZTkwxxxxxxYxMDJkYTBl",
    "APIKey": "xxxxxxx7dd3494880a7920axxxxxxxxx",
    "Prompt": {
      "error": "Concisely, in the role of a Kubernetes expert, assess the provided output for any issues. Clearly list the problematic components, potential causes, and recommend appropriate actions:",
      "security": "Concisely, in the role of a Kubernetes expert, assess the provided output for any issues. Clearly list the problematic components, potential causes, and recommend appropriate actions:"
    }
  },
```

## Effectiveness

### DocTree expands YAML definitions in a tree-like structure, no longer worry about forgetting the definitions.

<br>
  <img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/doc-tree.gif">
  <br>

### Explanation of Field Meanings

#### Click on the question mark next to the field on the resource details page.

* Use kubectl to obtain Kubernetes explanations
* Using a configured AI large model for intelligent interpretation, the results are as follows:
  <br>
  <img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/kubectl-explain.gif">
  <br>

### Generate deployment YAML

<br>
Obtain k8s deployment YAML through prompts and execute <br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/gpt-deploy.gif">
<br>

### Intelligent Analysis

Added intelligent analysis and security analysis buttons on each resource.
<br>
<img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/POD-analyze.gif">
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
  <img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/docs/img/cluster-inspection.png">
  <br>

## UI preview

[click me](docs/ui.md)
