// DTO (Data Transfer Object) para leitura de uma Categoria.
// Usado nas respostas da API (GET); não expõe dados internos da entidade.
public class CategoriaDto
{
    public int Id { get; set; }
    public required string Nome { get; set; }
}
