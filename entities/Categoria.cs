// Entidade de domínio que representa uma Categoria.
// Persistida no banco; CategoriaService usa o AppDbContext para acessá-la.
public class Categoria
{
    public int Id { get; set; }
    public required string Nome { get; set; }
}
