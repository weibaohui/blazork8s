FROM  mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM  mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "BlazorApp/BlazorApp.csproj"
WORKDIR "/src/BlazorApp"
RUN rm -rf appsettings.Development.json
RUN dotnet build "BlazorApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazorApp.csproj" -c Release -o /app/publish   /p:PublishSingleFile=true


FROM bitnami/kubectl:latest as kubectl
FROM busybox:latest as nc


FROM base AS final
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT Production
COPY --from=build /src/BlazorApp/docs.db .
COPY --from=publish /app/publish .
COPY --from=kubectl /opt/bitnami/kubectl/bin/kubectl /usr/bin/kubectl
COPY --from=nc /bin/nc /bin/nc
RUN rm -rf *.pdb

ENTRYPOINT ["./BlazorApp"]
