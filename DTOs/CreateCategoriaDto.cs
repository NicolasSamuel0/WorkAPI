// DTO para criação de uma Categoria (POST).
// Contém apenas os campos que o cliente envia; o Id é gerado pelo servidor.
public class CreateCategoriaDto
{
    public required string Nome { get; set; }
}
