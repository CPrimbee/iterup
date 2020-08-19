using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Api.Data;
using Desafio.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Api.Controllers
{
    [ApiController]
    [Route("desafio/respostas")]
    public class RespostasController : ControllerBase
    {
        private readonly DesafioContext _context;

        public RespostasController(DesafioContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public ActionResult<List<Resposta>> GetAll() =>
            _context.Respostas.OrderBy(x => x.NumResposta).ToList();
        
        [HttpGet("{NumReposta}")]
        public async Task<ActionResult<Resposta>> GetByNumResposta(int NumReposta)
        {
            var resposta = await _context.Respostas.FindAsync(NumReposta);

            if (resposta == null)
                return NotFound();

            return resposta;

        }

        [HttpPost]
        public async Task<IActionResult> Save(Resposta resposta)
        {
            _context.Respostas.Add(resposta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByNumResposta), new { NumResposta = resposta.NumResposta }, resposta);
        }

        [HttpPut("{NumResposta}")]
        public async Task<IActionResult> Update(int NumResposta, Resposta resposta)
        {
            if (NumResposta != resposta.NumResposta)
                return BadRequest();

            _context.Entry(resposta).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        [HttpDelete("{NumResposta}")]
        public async Task<IActionResult> Delete(int NumResposta)
        {
            Resposta resposta = await _context.Respostas.FindAsync(NumResposta);
            
            if (resposta == null)
                return NotFound();

            _context.Respostas.Remove(resposta);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}