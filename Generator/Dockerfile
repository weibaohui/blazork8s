FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Generator/Generator.csproj", "Generator/"]
RUN dotnet restore "Generator/Generator.csproj"
COPY . .
WORKDIR "/src/Generator"
RUN dotnet build "Generator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Generator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Generator.dll"]
