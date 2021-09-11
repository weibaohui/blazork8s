#blazor ui
docker build -t weibh/blazork8s:ui -f Blazor/Dockerfile .
#server
docker build -t weibh/blazork8s:server -f Server/Dockerfile .
