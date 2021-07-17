#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["AccountManagement.WebApi/AccountManagement.WebApi.csproj", "AccountManagement.WebApi/"]
COPY ["HypermediaDriven.SocialMedia.Core/HypermediaDriven.SocialMedia.Core.csproj", "HypermediaDriven.SocialMedia.Core/"]
RUN dotnet restore "AccountManagement.WebApi/AccountManagement.WebApi.csproj"
COPY . .
WORKDIR "/src/AccountManagement.WebApi"
RUN dotnet build "AccountManagement.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AccountManagement.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AccountManagement.WebApi.dll"]