using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

// Implementação do serviço de Categorias usando o banco de dados (Entity Framework Core).
// Recebe AppDbContext por injeção; cada requisição usa uma instância Scoped do contexto.
public class CategoriaService : ICategoriaService
{
    private readonly AppDbContext _db;

    public CategoriaService(AppDbContext db)
    {
        _db = db;
    }

    public IEnumerable<CategoriaDto> GetAll()
    {
        return _db.Categorias
            .AsNoTracking()
            .Select(c => new CategoriaDto { Id = c.Id, Nome = c.Nome })
            .ToList();
    }

    public CategoriaDto? GetById(int id)
    {
        var c = _db.Categorias.AsNoTracking().FirstOrDefault(x => x.Id == id);
        if (c == null) return null;
        return new CategoriaDto { Id = c.Id, Nome = c.Nome };
    }

    public CategoriaDto Create(CreateCategoriaDto dto)
    {
        var novo = new Categoria { Nome = dto.Nome };
        _db.Categorias.Add(novo);
        _db.SaveChanges();
        return new CategoriaDto { Id = novo.Id, Nome = novo.Nome };
    }

    public bool Update(int id, CreateCategoriaDto dto)
    {
        var c = _db.Categorias.Find(id);
        if (c == null) return false;
        c.Nome = dto.Nome;
        _db.SaveChanges();
        return true;
    }

    public bool UpdatePartial(int id, PatchCategoriaDto dto)
    {
        var c = _db.Categorias.Find(id);
        if (c == null) return false;
        if (dto.Nome is not null) c.Nome = dto.Nome;
        _db.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        var c = _db.Categorias.Find(id);
        if (c == null) return false;
        _db.Categorias.Remove(c);
        _db.SaveChanges();
        return true;
    }
}
