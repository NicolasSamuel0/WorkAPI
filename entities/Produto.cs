// Entidade de domínio que representa um Produto.
// Persistida no banco via Entity Framework Core (tabela Produtos).
public class Produto
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public decimal Preco { get; set; }
}
