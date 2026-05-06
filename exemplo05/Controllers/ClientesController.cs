using exemplo05.Data;
using exemplo05.Models;
using Microsoft.AspNetCore.Mvc;

namespace exemplo05.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public IEnumerable<Cliente> Get()
        {
            return _context.Clientes;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = cliente.Codigo }, cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Cliente cliente)
        {
            if (id != cliente.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
            {
                if (!_context.Clientes.Any(e => e.Codigo == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
