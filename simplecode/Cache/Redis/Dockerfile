#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Redis/Redis.csproj", "Redis/"]
RUN dotnet restore "Redis/Redis.csproj"
COPY . .
WORKDIR "/src/Redis"
RUN dotnet build "Redis.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Redis.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Redis.dll"]