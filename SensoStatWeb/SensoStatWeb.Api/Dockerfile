#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SensoStatWeb.Api/SensoStatWeb.Api.csproj", "SensoStatWeb.Api/"]
COPY ["SensoStatWeb.Models/SensoStatWeb.Models.csproj", "SensoStatWeb.Models/"]
COPY ["SensoStatWeb.Api.Business/SensoStatWeb.Api.Business.csproj", "SensoStatWeb.Api.Business/"]
COPY ["SensoStatWeb.Repository/SensoStatWeb.Repository.csproj", "SensoStatWeb.Repository/"]
COPY ["SensoStatWeb.Business/SensoStatWeb.Business.csproj", "SensoStatWeb.Business/"]
RUN dotnet restore "SensoStatWeb.Api/SensoStatWeb.Api.csproj"
COPY . .
WORKDIR "/src/SensoStatWeb.Api"
RUN dotnet build "SensoStatWeb.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SensoStatWeb.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SensoStatWeb.Api.dll"]