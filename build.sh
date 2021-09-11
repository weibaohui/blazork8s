#login
echo $CR_PAT | docker login ghcr.io -u weibaohui --password-stdin
#blazor ui
docker build -t ghcr.io/weibaohui/blazork8s:ui -f Blazor/Dockerfile .
docker push ghcr.io/weibaohui/blazork8s:ui
#server
docker build -t ghcr.io/weibaohui/blazork8s:latest -f Server/Dockerfile .
docker push ghcr.io/weibaohui/blazork8s:latest
