FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["allegro-pbi-token-api.csproj", ""]
RUN dotnet restore "./allegro-pbi-token-api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "allegro-pbi-token-api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "allegro-pbi-token-api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "allegro-pbi-token-api.dll"]