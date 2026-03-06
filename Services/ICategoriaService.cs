using System.Collections.Generic;

// Contrato do serviço de Categorias (inversão de dependência).
// Permite testar o controller com um mock e trocar a implementação sem alterar o controller.
public interface ICategoriaService
{
    IEnumerable<CategoriaDto> GetAll();
    CategoriaDto? GetById(int id);
    CategoriaDto Create(CreateCategoriaDto dto);
    bool Update(int id, CreateCategoriaDto dto);
    bool UpdatePartial(int id, PatchCategoriaDto dto);
    bool Delete(int id);
}
