using Microsoft.EntityFrameworkCore;

// Contexto do Entity Framework Core para a aplicação.
// Representa uma "sessão" com o banco de dados e expõe os DbSets (tabelas).
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // Tabela Produtos no banco. Cada Produto vira uma linha na tabela.
    public DbSet<Produto> Produtos => Set<Produto>();

    // Tabela Categorias no banco.
    public DbSet<Categoria> Categorias => Set<Categoria>();

    // Configuração do modelo: tamanhos máximos e precisão para o MySQL.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Produto: garantir que Nome tenha tamanho máximo (evita problemas com varchar no MySQL)
        modelBuilder.Entity<Produto>(e =>
        {
            e.Property(p => p.Nome).HasMaxLength(200);
            e.Property(p => p.Preco).HasPrecision(18, 2);
        });

        // Categoria: mesmo raciocínio
        modelBuilder.Entity<Categoria>(e =>
        {
            e.Property(c => c.Nome).HasMaxLength(200);
        });
    }
}
