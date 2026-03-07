using Microsoft.AspNetCore.Mvc;

namespace ApiAchadosEPerdidos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItensController : ControllerBase
    {
        private static List<string> itens = new List<string>();

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(itens);
        }

        [HttpPost]
        public IActionResult Add(string nome)
        {
            itens.Add(nome);
            return Ok(itens);
        }
    }
}