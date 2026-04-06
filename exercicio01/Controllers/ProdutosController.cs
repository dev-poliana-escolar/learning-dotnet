using exercicio01.Model.Produto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {

        private static readonly List<Produto> listaProdutos = [
            new Produto { Id = 1, NomeProduto = "Abacaxi", Descricao = "Uma fruta tropical" },
            new Produto { Id = 2, NomeProduto  = "Morango", Descricao = "Uma fruta vermelha" },
            new Produto { Id = 3, NomeProduto  = "Chocolate", Descricao = "Um doce"  } 
        ];

        
        [HttpGet]
        public ActionResult<List<Produto>> Get()
        {
            return Ok(listaProdutos);
        }

        
        [HttpPost]
        public ActionResult<Produto> Post(Produto produto)
        {
            produto.Id = listaProdutos.Count + 1; // Simula um ID auto-incremental
            listaProdutos.Add(produto);
            return CreatedAtAction(nameof(Get), new { id = produto.Id }, produto);
        }
        

    }
}
