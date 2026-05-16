using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using gerencia_alunos.Data;
using gerencia_alunos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;


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
        public async Task<ActionResult<IEnumerable<Aluno>>> Get()
        {
            return await _context.Alunos.ToListAsync();
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

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarAluno(int id, Aluno aluno)
        {
            if (id != aluno.Id) // se id diferente do id do objeto
            {
                return BadRequest("O ID da URL é diferente do ID do objeto.");
            }

            var alunoExistente = await _context.Alunos.FindAsync(id); // busca o aluno

            if (alunoExistente == null)
            {
                return NotFound();
            }

            // atualiza as propriedades do aluno existente com os valores do aluno recebido
            alunoExistente.Nome = aluno.Nome;
            alunoExistente.Email = aluno.Email;
            alunoExistente.Curso = aluno.Curso;
            alunoExistente.DataNascimento = aluno.DataNascimento;

            await _context.SaveChangesAsync();

            return Ok(alunoExistente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarAluno(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);

            if (aluno == null)
            {
                return NotFound();
            }

            _context.Alunos.Remove(aluno);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        
    }
}
