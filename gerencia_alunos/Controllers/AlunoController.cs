using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using gerencia_alunos.Data;
using gerencia_alunos.Models;
using Microsoft.AspNetCore.Authorization;
 

namespace gerencia_alunos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AlunoController : ControllerBase
    {
        private readonly AppDbContext _context;
        public AlunoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Aluno> Get()
        {
            return _context.Alunos.ToList();
        }

        [HttpPost]
        public async Task<IActionResult> CriarAluno(Aluno aluno)
        {
            await _context.Alunos.AddAsync(aluno);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(BuscarPorId),
                new { id = aluno.Id },
                aluno
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);

            if (aluno == null)
            {
                return NotFound();
            }

            return Ok(aluno);
        }

        
    }
}
