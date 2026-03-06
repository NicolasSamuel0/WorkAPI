using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// Controller REST para o recurso "Produtos".
// A rota base é /api/Produtos (o [controller] é substituído pelo nome da classe sem "Controller").
// Os dados vêm do banco de dados via Entity Framework Core (AppDbContext).
[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _db;

    public ProdutosController(AppDbContext db)
    {
        _db = db;
    }

    // GET /api/Produtos — Lista todos os produtos.
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken = default)
    {
        var produtos = await _db.Produtos.ToListAsync(cancellationToken);
        return Ok(produtos);
    }

    // GET /api/Produtos/{id} — Retorna um produto pelo Id. Retorna 404 se não existir.
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken = default)
    {
        var produto = await _db.Produtos.FindAsync([id], cancellationToken);
        if (produto == null) return NotFound();
        return Ok(produto);
    }

    // POST /api/Produtos — Cria um novo produto. Corpo: { "nome": "string", "preco": number }. Retorna 201 com o recurso criado.
    [HttpPost]
    public async Task<IActionResult> Post(Produto novoProduto, CancellationToken cancellationToken = default)
    {
        // O Id pode ser gerado pelo banco (auto-increment) ou calculado; aqui definimos 0 para o MySQL gerar
        novoProduto.Id = 0;
        _db.Produtos.Add(novoProduto);
        await _db.SaveChangesAsync(cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = novoProduto.Id }, novoProduto);
    }

    // PUT /api/Produtos/{id} — Atualiza um produto existente (substituição completa). Retorna 404 se não existir.
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Produto produtoAtualizado, CancellationToken cancellationToken = default)
    {
        var produto = await _db.Produtos.FindAsync([id], cancellationToken);
        if (produto == null) return NotFound();
        produto.Nome = produtoAtualizado.Nome;
        produto.Preco = produtoAtualizado.Preco;
        await _db.SaveChangesAsync(cancellationToken);
        return Ok(produto);
    }

    // DELETE /api/Produtos/{id} — Remove um produto. Retorna 204 (sem conteúdo) ou 404.
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
    {
        var produto = await _db.Produtos.FindAsync([id], cancellationToken);
        if (produto == null) return NotFound();
        _db.Produtos.Remove(produto);
        await _db.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
}
