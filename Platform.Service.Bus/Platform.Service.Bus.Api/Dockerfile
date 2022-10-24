#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Platform.Service.Bus.Api/Platform.Service.Bus.Api.csproj", "Platform.Service.Bus.Api/"]
RUN dotnet restore "Platform.Service.Bus.Api/Platform.Service.Bus.Api.csproj"
COPY . .
WORKDIR "/src/Platform.Service.Bus.Api"
RUN dotnet build "Platform.Service.Bus.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Platform.Service.Bus.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Platform.Service.Bus.Api.dll"]