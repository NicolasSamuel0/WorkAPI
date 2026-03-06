# MinhaPrimeiraApi — Material didático

API REST em ASP.NET Core para fins de estudo. Contém exemplos de **Minimal API**, **Controllers** com CRUD, **DTOs**, **serviços** e **injeção de dependência**.

---

## Como executar

### Opção 1: Sem Docker (MySQL rodando localmente ou em outro container)

1. Suba um MySQL (ex.: `docker run -e MYSQL_ROOT_PASSWORD=root -e MYSQL_DATABASE=MinhaPrimeiraApi -p 3306:3306 mysql:8.0`) ou use o `docker compose up mysql -d` só para o banco.
2. Na pasta do projeto: `dotnet run`
3. A API sobe em **http://localhost:5155** (veja `Properties/launchSettings.json`).

### Opção 2: Com Docker (MySQL + API)

1. Na pasta do projeto execute:
   ```bash
   docker compose up --build -d
   ```
2. O Compose sobe o **MySQL** (porta 3306) e a **API** (porta **8080**).
3. A API fica em **http://localhost:8080** (ex.: http://localhost:8080/api/Produtos).
4. Na primeira subida, a aplicação cria o banco, as tabelas e insere os dados iniciais (seed).

Para parar: `docker compose down`. Os dados do MySQL ficam no volume `mysql_data` (persistem entre subidas).

---

## Endpoints disponíveis

| Método | URL | Descrição |
|--------|-----|-----------|
| **Minimal API** | | |
| GET | `/weatherforecast` | Exemplo do template; retorna previsão do tempo fictícia. |
| **Produtos** (banco MySQL) | | |
| GET | `/api/Produtos` | Lista todos os produtos. |
| GET | `/api/Produtos/{id}` | Retorna um produto pelo `id`. |
| POST | `/api/Produtos` | Cria um produto. Body: `{"nome":"string","preco":number}` |
| PUT | `/api/Produtos/{id}` | Atualiza um produto (substituição completa). |
| DELETE | `/api/Produtos/{id}` | Remove um produto. |
| **Categorias** (serviço + DTOs) | | |
| GET | `/api/Categorias` | Lista todas as categorias. |
| GET | `/api/Categorias/{id}` | Retorna uma categoria pelo `id`. |
| POST | `/api/Categorias` | Cria uma categoria. Body: `{"nome":"string"}` |
| PUT | `/api/Categorias/{id}` | Atualiza uma categoria (completo). |
| PATCH | `/api/Categorias/{id}` | Atualização parcial; body com campos opcionais. |
| DELETE | `/api/Categorias/{id}` | Remove uma categoria. |

Em ambiente **Development**, o documento OpenAPI fica em:  
**http://localhost:5155/openapi/v1.json**

---

## Como testar os endpoints

- **Arquivo `.http`**: Use o arquivo `MinhaPrimeiraApi.http` no Visual Studio ou no Cursor (extensão REST Client). Cada bloco `###` é uma requisição que pode ser executada.
- **Navegador**: apenas para GET (ex.: http://localhost:5155/api/Produtos).
- **Postman / Insomnia / curl**: use a base `http://localhost:5155` e os caminhos da tabela acima. Para POST/PUT/PATCH, envie `Content-Type: application/json` e o corpo em JSON.

Exemplo com **curl** (criar produto):

```bash
curl -X POST http://localhost:5155/api/Produtos -H "Content-Type: application/json" -d "{\"nome\":\"Teclado\",\"preco\":150}"
```

---

## Estrutura do projeto (didática)

- **Controllers**: expõem as rotas HTTP; Produtos usam o `AppDbContext` (EF Core); Categorias usam o `ICategoriaService` (que também usa o banco).
- **Data/AppDbContext**: contexto do Entity Framework Core; define os `DbSet`s (Produtos, Categorias) e a configuração do modelo.
- **Entities**: modelos de domínio (Produto, Categoria) mapeados para tabelas no MySQL.
- **DTOs**: objetos para entrada/saída da API (ex.: CreateCategoriaDto, PatchCategoriaDto, CategoriaDto).
- **Services**: CategoriaService implementa ICategoriaService usando o `AppDbContext`; registrado como Scoped no `Program.cs`.
- **Program.cs**: configura serviços (`AddControllers`, `AddDbContext`, `AddScoped<ICategoriaService, CategoriaService>`), cria o banco/tabelas e seed na subida (`EnsureCreated`), e mapeia rotas (`MapControllers()`).
- **Docker**: `Dockerfile` (build em dois estágios) e `docker-compose.yml` (MySQL + API) para rodar tudo com um comando.

Os comentários no código explicam cada parte para fins de estudo em sala de aula.
