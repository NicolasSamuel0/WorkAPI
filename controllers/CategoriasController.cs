using Microsoft.AspNetCore.Mvc;

// Controller REST para o recurso "Categorias".
// Rota base: /api/Categorias. Utiliza serviço (ICategoriaService) e DTOs para separação de responsabilidades.
[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaService _service;

    public CategoriasController(ICategoriaService service)
    {
        _service = service;
    }

    // Retorna todas as categorias.
    [HttpGet]
    public IActionResult Get() => Ok(_service.GetAll());

    // Retorna uma categoria pelo identificador.
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var c = _service.GetById(id);
        if (c == null) return NotFound();
        return Ok(c);
    }

    // Cria uma nova categoria.
    [HttpPost]
    public IActionResult Post(CreateCategoriaDto dto)
    {
        var created = _service.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // Atualiza completamente uma categoria existente.
    [HttpPut("{id}")]
    public IActionResult Put(int id, CreateCategoriaDto dto)
    {
        var ok = _service.Update(id, dto);
        if (!ok) return NotFound();
        return NoContent();
    }

    // Aplica atualização parcial em uma categoria. Campos nulos no dto são ignorados.
    [HttpPatch("{id}")]
    public IActionResult Patch(int id, PatchCategoriaDto dto)
    {
        var ok = _service.UpdatePartial(id, dto);
        if (!ok) return NotFound();
        return NoContent();
    }

    // Remove uma categoria pelo identificador.
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var ok = _service.Delete(id);
        if (!ok) return NotFound();
        return NoContent();
    }
}
