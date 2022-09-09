#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AuthPortalApi/AuthPortalApi.csproj", "AuthPortalApi/"]
COPY ["ApiResult/ApiResultLibrary.csproj", "ApiResult/"]
COPY ["AuthLibrary/AuthLibrary.csproj", "AuthLibrary/"]
COPY ["CacheLibrary/CacheLibrary.csproj", "CacheLibrary/"]
COPY ["JwtLibrary/JwtLibrary.csproj", "JwtLibrary/"]

RUN dotnet restore "AuthPortalApi/AuthPortalApi.csproj"

COPY . .
WORKDIR "/src/AuthPortalApi"
RUN dotnet build "AuthPortalApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthPortalApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthPortalApi.dll"]