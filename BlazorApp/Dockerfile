
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BlazorApp/BlazorApp.csproj", "BlazorApp/"]
RUN dotnet restore "BlazorApp/BlazorApp.csproj"
COPY . .
WORKDIR "/src/BlazorApp"
RUN dotnet build "BlazorApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazorApp.csproj" -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS  kubectl
RUN apt-get update && apt-get install -y apt-transport-https curl gnupg \
&& curl https://mirrors.aliyun.com/kubernetes/apt/doc/apt-key.gpg | apt-key add - \
&& echo "deb https://mirrors.aliyun.com/kubernetes/apt/ kubernetes-xenial main"  >/etc/apt/sources.list.d/kubernetes.list \
&& apt-get update \
&& apt-get install -y  kubectl


FROM base AS final
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT Production
COPY --from=publish /app/publish .
COPY --from=kubectl /usr/bin/kubectl  /usr/bin/kubectl
ENTRYPOINT ["dotnet", "BlazorApp.dll"]
