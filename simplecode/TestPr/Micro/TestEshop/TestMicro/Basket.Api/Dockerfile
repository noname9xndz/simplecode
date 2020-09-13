#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-nanoserver-1903 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1903 AS build
WORKDIR /src
COPY ["Basket.Api/Basket.Api.csproj", "Basket.Api/"]
COPY ["TestMicro.Infrastructure/TestMicro.Infrastructure.csproj", "TestMicro.Infrastructure/"]
COPY ["Event.Bus/Event.Bus.csproj", "Event.Bus/"]
COPY ["EventLogEF/EventLogEF.csproj", "EventLogEF/"]
COPY ["Basket.Infrastructure/Basket.Infrastructure.csproj", "Basket.Infrastructure/"]
RUN dotnet restore "Basket.Api/Basket.Api.csproj"
COPY . .
WORKDIR "/src/Basket.Api"
RUN dotnet build "Basket.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Basket.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.Api.dll"]