[![Server Build](https://github.com/weibaohui/blazork8s/actions/workflows/server.yml/badge.svg)](https://github.com/weibaohui/blazork8s/actions/workflows/server.yml)
[![UI Build](https://github.com/weibaohui/blazork8s/actions/workflows/ui.yaml/badge.svg)](https://github.com/weibaohui/blazork8s/actions/workflows/ui.yaml)

<p align="center">
  <a href="https://github.com/weibaohui/blazork8s">
    <img src="https://raw.githubusercontent.com/weibaohui/blazork8s/main/Blazor/wwwroot/pro_icon.svg">
  </a>
</p>

<h1 align="center"> Blazor k8s </h1>

## ☀️ 授权协议

[![BlazorK8s](https://img.shields.io/badge/License-MIT-blue?style=flat-square)](https://github.com/weibaohui/blazork8s/blob/master/LICENSE)

# 体验
## start server
```docker
docker run -d --name blazork8s  -v ~/.kube/:/root/.kube/ -p 4001:443 -p 4000:80 ghcr.io/weibaohui/blazork8s:latest
```
## ui
```docker
docker run -d --name blazork8s-ui -p 6088:80  ghcr.io/weibaohui/blazork8s-ui:latest
```
## web
[web ui](http://localhost:6088)

## 🙏 鸣谢

感谢 [JetBrains 公司](https://www.jetbrains.com/?from=mesh) 为本开源项目提供的免费正版 JetBrains Rider  的 License 支持。
