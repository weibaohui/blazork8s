#Debug
#start server using port 4000
docker run -it --rm -v ~/.kube/:/root/.kube/ -p 4001:443 -p 4000:80 ghcr.io/weibaohui/blazork8s:latest
