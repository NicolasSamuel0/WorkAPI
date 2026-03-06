using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ========== CONFIGURAÇÃO DE SERVIÇOS (DI) ==========
// Add services to the container.
builder.Services.AddControllers(); // Registra os controllers da API (ex.: ProdutosController, CategoriasController)
builder.Services.AddOpenApi();      // Habilita documentação OpenAPI (Swagger) em ambiente de desenvolvimento
builder.Services.AddScoped<ICategoriaService, CategoriaService>(); // Scoped: uma instância por requisição (compatível com DbContext)

// Conexão com o banco: lida da configuração (appsettings.json) ou da variável de ambiente no Docker
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=localhost;Port=3306;Database=MinhaPrimeiraApi;User=root;Password=root;";

// Registro do Entity Framework Core com provedor MySQL (Pomelo)
// AddDbContext usa Scoped por padrão: uma instância por requisição HTTP
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString),
        mySqlOptions => mySqlOptions.EnableRetryOnFailure(3));
});

var app = builder.Build();

// ========== CRIAR BANCO E TABELAS (se não existirem) ==========
// EnsureCreated() cria o banco e as tabelas a partir do modelo EF. Ideal para didática e desenvolvimento.
// Em produção, prefira Migrations: Add-Migration Nome e Update-Database.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        db.Database.EnsureCreated(); // Cria o banco e as tabelas (Produtos, Categorias)
        // Seed: insere dados iniciais apenas se as tabelas estiverem vazias
        if (!db.Produtos.Any())
        {
            db.Produtos.AddRange(
                new Produto { Id = 1, Nome = "Notebook", Preco = 3500 },
                new Produto { Id = 2, Nome = "Mouse", Preco = 80 });
            db.SaveChanges();
        }
        if (!db.Categorias.Any())
        {
            db.Categorias.AddRange(
                new Categoria { Id = 1, Nome = "Eletrônicos" },
                new Categoria { Id = 2, Nome = "Móveis" });
            db.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogWarning(ex, "Erro ao criar/seed do banco. Verifique se o MySQL está acessível (ex.: Docker).");
    }
}

// ========== PIPELINE HTTP ==========
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // Expõe o documento OpenAPI (ex.: em /openapi/v1.json)
}

// app.UseHttpsRedirection(); // Descomente para forçar redirecionamento HTTPS em produção

// Mapeia os endpoints definidos nos controllers (rotas como /api/Produtos e /api/Categorias)
app.MapControllers();

// ========== ENDPOINT MÍNIMO (Minimal API) - Exemplo didático ==========
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
