FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Blazor/Blazor.csproj", "Blazor/"]
RUN dotnet restore "Blazor/Blazor.csproj"
COPY . .
WORKDIR "/src/Blazor"
RUN dotnet build "Blazor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Blazor.csproj" -c Release -o /app/publish


FROM  nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY Blazor/nginx/default.conf /etc/nginx/conf.d/default.conf
RUN ls /usr/share/nginx/html
EXPOSE 80
EXPOSE 443
