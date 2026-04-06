using exercicio01.Model.Cliente;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exercicio01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private static readonly List<Cliente> clientes = [
            new Cliente { Id = 1, Nome = "João Silva", Email = "joao.silva@example.com" },
            new Cliente { Id = 2, Nome = "Maria Oliveira", Email = "maria.oliveira@example.com" },
            new Cliente { Id = 3, Nome = "Carlos Santos", Email = "carlos.santos@example.com" }
        ];

        [HttpGet]
        public ActionResult<List<Cliente>> Get()
        {
            return Ok(clientes);
        }

        [HttpPost]
        public ActionResult<Cliente> Post(Cliente cliente)
        {
            cliente.Id = clientes.Count + 1; // Simula um ID auto-incremental
            clientes.Add(cliente);
            return CreatedAtAction(nameof(Get), new { id = cliente.Id }, cliente);
        }
        
        [HttpPut("{id}")]
        public ActionResult<Cliente> Put(int id, Cliente cliente)
        {
            var existeCliente = clientes.Find(c=> c.Id ==id);
            if(existeCliente==null){
                return NotFound();
            }

            existeCliente.Nome = cliente.Nome;
            existeCliente.Email = cliente.Email;

            return Ok(existeCliente);

        }

        [HttpDelete("{id}")]
        public ActionResult<Cliente> Delete(int id)
        {
            var cliente = clientes.Find(c=>c.Id == id);
            if (cliente ==null)
            {
                return NotFound();
            }

            clientes.Remove(cliente);
            return NoContent();
        }
    }
}
