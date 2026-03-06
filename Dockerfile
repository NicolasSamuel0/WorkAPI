# =============================================================================
# DOCKERFILE - Build e execução da API em um container Linux
# =============================================================================
# Este arquivo define a "imagem" da aplicação. Possui dois estágios (multi-stage):
# 1) Estágio "build": compila a aplicação .NET
# 2) Estágio "run": copia o resultado e executa a API (imagem final menor)
# =============================================================================

# ---------- Estágio 1: Build ----------
# Imagem base com SDK do .NET (permite compilar e publicar)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar o arquivo de projeto e restaurar dependências (NuGet)
# Essa camada é reaproveitada pelo cache do Docker enquanto o .csproj não mudar
COPY ["MinhaPrimeiraApi.csproj", "."]
RUN dotnet restore "MinhaPrimeiraApi.csproj"

# Copiar o resto do código e publicar a aplicação para produção
# --no-restore usa os pacotes já restaurados; -o /app/publish gera os arquivos na pasta indicada
COPY . .
RUN dotnet publish "MinhaPrimeiraApi.csproj" -c Release -o /app/publish --no-restore

# ---------- Estágio 2: Run ----------
# Imagem final só com o runtime (menor; não inclui SDK)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS run
WORKDIR /app

# Copiar apenas o resultado do publish do estágio build
COPY --from=build /app/publish .

# A aplicação escuta na porta 8080 por padrão em ambiente containerizado
ENV ASPNETCORE_URLS=http://+:8080

# Expor a porta para o host poder acessar
EXPOSE 8080

# Comando que inicia a API
ENTRYPOINT ["dotnet", "MinhaPrimeiraApi.dll"]
