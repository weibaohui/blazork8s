# Install Istio For Testing Gateway API

```
bin/istioctl install -f samples/bookinfo/demo-profile-no-gateways.yaml -y
```
# To install the standard Gateway API, use the following command:

```
kubectl apply -f https://github.com/kubernetes-sigs/gateway-api/releases/download/v1.1.0/standard-install.yaml
```
# Gateway API experimental install

```
kubectl apply -f https://github.com/kubernetes-sigs/gateway-api/releases/download/v1.1.0/experimental-install.yaml
```

# Json Schema for Gateway API

```
https://github.com/kubernetes-sigs/gateway-api/cmd/modelschema/main.go
```
```
cmd/modelschema/main.go
```
# Translate to CN
use TranslateService.ProcessDocTree() to translate the json schema to Chinese.
