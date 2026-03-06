# Conteúdo das slides — Tutorial teórico MinhaPrimeiraApi

Use este arquivo para montar o PowerPoint manualmente ou como roteiro da aula. Cada seção é uma slide.

---

## Slide 1 — Capa

**Título:** MinhaPrimeiraApi — Tutorial Teórico  

**Subtítulo:**  
Ferramentas: Docker, Entity Framework Core, MySQL  
Desenvolvimento Web com C#

---

## Slide 2 — Objetivos da aula

**Título:** Objetivos da aula  

**Tópicos:**
- Conhecer as ferramentas utilizadas no projeto: Docker, MySQL e Entity Framework Core.
- Entender o papel de cada uma na arquitetura da API.
- Saber executar a aplicação passo a passo (com e sem Docker).
- Ter uma visão geral dos endpoints e da estrutura do projeto.

*Nota do orador:* Apresente os objetivos antes de entrar nas ferramentas.

---

## Slide 3 — Visão geral do projeto

**Título:** Visão geral do projeto  

**Tópicos:**
- API REST em ASP.NET Core (.NET 9) para fins didáticos.
- Recursos: Produtos e Categorias (CRUD completo).
- Banco de dados MySQL para persistência.
- Containerização com Docker: MySQL + API em um único comando.
- Entity Framework Core como ORM para acesso ao banco.

*Nota do orador:* Projeto usado em sala para ilustrar API + banco + Docker.

---

## Slide 4 — Ferramenta 1: Docker

**Título:** Ferramenta 1: Docker  

**Tópicos:**
- Docker: plataforma de virtualização em nível de sistema operacional (containers).
- Container: ambiente isolado e leve que empacota aplicação + dependências.
- Vantagens: mesmo ambiente em dev e produção; fácil reprodução; um comando para subir tudo.
- No projeto: uma imagem para o MySQL e uma para a API; o Docker Compose orquestra os dois.

*Nota do orador:* Diferencie container de VM: mais leve, compartilha o kernel.

---

## Slide 5 — Conceitos Docker (resumo)

**Título:** Conceitos Docker (resumo)  

**Tópicos:**
- **Imagem:** modelo read-only (ex.: mysql:8.0, nossa API buildada pelo Dockerfile).
- **Container:** instância em execução de uma imagem.
- **Dockerfile:** receita para construir a imagem da nossa API (build + run em estágios).
- **Docker Compose:** arquivo YAML que define e sobe vários serviços (mysql + api) e a rede entre eles.

*Nota do orador:* Mostre o docker-compose.yml do projeto na IDE.

---

## Slide 6 — Ferramenta 2: MySQL

**Título:** Ferramenta 2: MySQL  

**Tópicos:**
- MySQL: banco de dados relacional (SGBD) muito usado em aplicações web.
- No projeto: roda dentro de um container; a API conecta via connection string.
- Banco criado automaticamente (MYSQL_DATABASE=MinhaPrimeiraApi).
- Tabelas: criadas pela aplicação via Entity Framework (EnsureCreated) na primeira execução.
- Dados persistem no volume Docker (mysql_data) mesmo após docker compose down.

*Nota do orador:* Porta 3306 exposta para acesso externo (cliente MySQL, etc.).

---

## Slide 7 — Ferramenta 3: Entity Framework Core

**Título:** Ferramenta 3: Entity Framework Core  

**Tópicos:**
- EF Core: ORM (Object-Relational Mapper) da Microsoft para .NET.
- Permite trabalhar com banco usando classes C# (entidades) em vez de SQL direto.
- **DbContext:** representa a sessão com o banco; expõe DbSet<T> para cada tabela.
- **Provedor MySQL:** Pomelo.EntityFrameworkCore.MySql conecta o EF ao MySQL.
- No projeto: AppDbContext define Produtos e Categorias; EnsureCreated cria o banco e as tabelas.

*Nota do orador:* Mencione que em produção é comum usar Migrations em vez de EnsureCreated.

---

## Slide 8 — Fluxo da aplicação

**Título:** Fluxo da aplicação  

**Tópicos:**
- Cliente HTTP (navegador, Postman, .http) envia requisição para a API.
- Controller (ex.: ProdutosController) recebe a requisição e usa o AppDbContext.
- DbContext executa as operações no MySQL (SELECT, INSERT, UPDATE, DELETE).
- Resposta JSON é devolvida ao cliente.
- Com Docker: API e MySQL estão na mesma rede; a API usa o hostname "mysql" na connection string.

*Nota do orador:* Desenhe na lousa: Cliente → API (Controller → DbContext) → MySQL.

---

## Slide 9 — Passo a passo: Pré-requisitos

**Título:** Passo a passo: Pré-requisitos  

**Tópicos:**
- Ter o Docker Desktop instalado e em execução (para rodar com Docker).
- Ou: ter .NET 9 SDK instalado (para rodar só a API com dotnet run).
- Ter o código do projeto (clone ou cópia da pasta MinhaPrimeiraApi).
- Abrir a pasta do projeto no terminal (PowerShell ou CMD).

*Nota do orador:* Docker Desktop: docker.com/products/docker-desktop

---

## Slide 10 — Passo a passo: Executar com Docker

**Título:** Passo a passo: Executar com Docker  

**Tópicos:**
1. Abra o terminal na pasta do projeto (onde está o docker-compose.yml).
2. Execute: **docker compose up --build -d**  
   (--build reconstrói a imagem da API; -d roda em segundo plano).
3. Aguarde o MySQL ficar saudável e a API subir (alguns segundos).
4. A API estará em: **http://localhost:8080**  
   Ex.: http://localhost:8080/api/Produtos

*Nota do orador:* Na primeira vez, o download das imagens pode demorar.

---

## Slide 11 — Passo a passo: Testar a API

**Título:** Passo a passo: Testar a API  

**Tópicos:**
- **Navegador:** acesse http://localhost:8080/api/Produtos (lista em JSON).
- **Arquivo .http:** abra MinhaPrimeiraApi.http e altere a URL para localhost:8080; execute cada bloco.
- **Postman/Insomnia:** GET/POST/PUT/DELETE em http://localhost:8080/api/Produtos e /api/Categorias.
- **Parar os containers:** docker compose down (os dados do MySQL ficam no volume).

*Nota do orador:* Mostre na prática um GET e um POST no navegador ou Postman.

---

## Slide 12 — Endpoints disponíveis (resumo)

**Título:** Endpoints disponíveis (resumo)  

**Tópicos:**
- **Produtos:** GET/POST /api/Produtos — GET/PUT/DELETE /api/Produtos/{id}
- **Categorias:** GET/POST /api/Categorias — GET/PUT/PATCH/DELETE /api/Categorias/{id}
- Exemplo: GET /api/Produtos retorna lista; POST com body {"nome":"...", "preco": 100} cria um produto.
- **OpenAPI (Development):** http://localhost:8080/openapi/v1.json

*Nota do orador:* Ressalte a diferença entre PUT (substituição) e PATCH (parcial) em Categorias.

---

## Slide 13 — Estrutura do projeto

**Título:** Estrutura do projeto  

**Tópicos:**
- **Controllers/:** ProdutosController, CategoriasController (rotas da API).
- **Data/AppDbContext.cs:** contexto EF Core (DbSet Produtos e Categorias).
- **entities/:** classes Produto e Categoria (tabelas no banco).
- **DTOs/:** objetos de entrada/saída (ex.: CreateCategoriaDto, CategoriaDto).
- **Services/:** CategoriaService (regras de negócio usando o DbContext).
- **Dockerfile** e **docker-compose.yml:** build e orquestração com MySQL.

*Nota do orador:* Abra a árvore do projeto na IDE para mostrar aos alunos.

---

## Slide 14 — Executar sem Docker (opcional)

**Título:** Executar sem Docker (opcional)  

**Tópicos:**
- Subir só o MySQL: **docker compose up mysql -d**
- Na pasta do projeto: **dotnet run**
- API em http://localhost:5155 (veja Properties/launchSettings.json).
- Connection string em appsettings.json: localhost, porta 3306, banco MinhaPrimeiraApi.

*Nota do orador:* Útil quando o aluno quer debugar a API no Visual Studio sem container.

---

## Slide 15 — Referências e dúvidas

**Título:** Referências e dúvidas  

**Tópicos:**
- Documentação Docker: docs.docker.com
- Entity Framework Core: learn.microsoft.com/ef/core
- MySQL: dev.mysql.com/doc
- Projeto: leia o README.md na raiz do repositório.
- Dúvidas: discutir em sala ou nos fóruns da disciplina.

*Nota do orador:* Deixe tempo para perguntas e indique o README para consulta depois.
