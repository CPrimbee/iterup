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
    [Route("desafio/etapas")]
    public class EtapasController : ControllerBase
    {
        private readonly DesafioContext _context;
        
        public EtapasController(DesafioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Etapa>> GetAll() =>
            _context.Etapas.OrderBy(x => x.NumEtapa).ToList();

        [HttpGet("{NumEtapa}")]
        public async Task<ActionResult<Etapa>> GetByNum(int NumEtapa)
        {
            var etapa = await _context.Etapas.FindAsync(NumEtapa);

            if (etapa == null)
                return NotFound();

            return etapa;
        }

        [HttpGet("{NumEtapa}/respostas")]
        public ActionResult<List<Resposta>> GetByNumEtapa(int NumEtapa)
        {
            var respostas = _context.Respostas.Where(x => x.NumEtapa == NumEtapa).ToList();

            if (respostas == null)
                return NotFound();

            return respostas;
        }

        [HttpPost]
        public async Task<IActionResult> Save(Etapa etapa)
        {
            _context.Etapas.Add(etapa);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByNumEtapa), new { NumEtapa = etapa.NumEtapa }, etapa);
        }
        
        [HttpPut("{NumEtapa}")]
        public async Task<IActionResult> Update(int NumEtapa, Etapa etapa)
        {
            if (NumEtapa != etapa.NumEtapa)
                return BadRequest();

            _context.Entry(etapa).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{NumEtapa}")]
        public async Task<IActionResult> Delete(int NumEtapa)
        {
            Etapa etapa = await _context.Etapas.FindAsync(NumEtapa);
            
            if (etapa == null)
                return NotFound();

            List<Resposta> respostas = _context.Respostas.Where(x => x.NumEtapa == etapa.NumEtapa).ToList();
            if (respostas.Count > 0)
                _context.Respostas.RemoveRange(respostas);

            _context.Etapas.Remove(etapa);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}