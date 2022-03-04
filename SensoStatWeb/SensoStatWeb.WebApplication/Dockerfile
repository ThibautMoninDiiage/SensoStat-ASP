#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SensoStatWeb.WebApplication/SensoStatWeb.WebApplication.csproj", "SensoStatWeb.WebApplication/"]
COPY ["SensoStatWeb.Models/SensoStatWeb.Models.csproj", "SensoStatWeb.Models/"]
COPY ["SensoStatWeb.Business/SensoStatWeb.Business.csproj", "SensoStatWeb.Business/"]
RUN dotnet restore "SensoStatWeb.WebApplication/SensoStatWeb.WebApplication.csproj"
COPY . .
WORKDIR "/src/SensoStatWeb.WebApplication"
RUN dotnet build "SensoStatWeb.WebApplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SensoStatWeb.WebApplication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SensoStatWeb.WebApplication.dll"]