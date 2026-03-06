# ===============================
# BUILD
# ===============================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ApiAchadosEPerdidos/ApiAchadosEPerdidos.csproj ApiAchadosEPerdidos/
WORKDIR /src/ApiAchadosEPerdidos

RUN dotnet restore

COPY ApiAchadosEPerdidos/. .

RUN dotnet publish -c Release -o /app/publish

# ===============================
# RUNTIME
# ===============================
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "ApiAchadosEPerdidos.dll"]