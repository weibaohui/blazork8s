using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BlazorApp.Service;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.ai;

public partial class ChatDeployment : ComponentBase
{
    public string txtValue;
    public string Advice;
    public string YamlAdvice;
    bool          _loading = false;

    [Inject]
    private IOpenAiService OpenAi { get; set; }

    private async Task ChatBtnClicked()
    {
        Advice     = "";
        YamlAdvice = "";

        if (!string.IsNullOrEmpty(txtValue))
        {
            _loading   = true;
            Advice     = await OpenAi.Chat(txtValue);
            YamlAdvice = getRegexYaml(Advice);
            _loading   = false;
        }
    }

    private string getRegexYaml(string input)
    {
        #region MyRegion

        var text_input = @"
以下是一个示例的Kubernetes部署YAML文件，用于部署一个简单的Nginx Web服务器，同时使用Ingress对象暴露服务并允许外部访问。

```yaml
# nginx-deployment.yaml

apiVersion: apps/v1
kind: Deployment
metadata:
  name: nginx-deployment
  labels:
    app: nginx
spec:
  replicas: 1 # 定义副本数为1
  selector:
    matchLabels:
      app: nginx
  template:
    metadata:
      labels:
        app: nginx
    spec:
      containers:
        - name: nginx
          image: nginx:latest
          ports:
            - containerPort: 80 # 服务监听端口为80

---

# nginx-service.yaml

apiVersion: v1
kind: Service
metadata:
  name: nginx-service
spec:
  selector:
    app: nginx
  ports:
    - protocol: TCP
      port: 80
      targetPort: 80

---

# nginx-ingress.yaml

apiVersion: extensions/v1beta1
kind: Ingress
metadata:
  name: nginx-ingress
spec:
  rules:
  - host: nginx.example.com # 定义www.example.com映射到nginx-service服务
    http:
      paths:
      - path: /
        backend:
          serviceName: nginx-service
          servicePort: 80
```

通过上述YAML文件可以完成一个部署名为nginx，使用Ingress对象暴露服务并且可以使用域名nginx.example.com进行访问的Nginx Web服务器的部署。

请注意，您需要替换`nginx.example.com`为您要使用的实际域名，并确保您已正确安装和配置了Kubernetes Ingress Controller才能使Ingress对象生效。

";

        #endregion

        string pattern = "```yaml([^`]*)```";
        var    tmp     = RegexYaml(input, pattern);
        if (string.IsNullOrEmpty(tmp))
        {
            tmp = RegexYaml(input, "```([^`]*)```");
        }

        if (string.IsNullOrEmpty(tmp))
        {
            if (Advice.StartsWith("---") || Advice.StartsWith("apiVersion"))
            {
                return Advice;
            }
        }

        return tmp;
    }

    private static string RegexYaml(string input, string pattern)
    {
        Match match = Regex.Match(input, pattern);
        if (match.Success)
        {
            string code = match.Groups[1].Value;
            return code;
        }

        return string.Empty;
    }
}
