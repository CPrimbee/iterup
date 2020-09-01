using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Desafio.Api.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Desafio.Api.Services;
using Desafio.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Api.Controllers
{
    [ApiController]
    [Route("desafio")]
    public class DesafioController : ControllerBase
    {
        private readonly DesafioContext _context;
        
        public DesafioController(DesafioContext context)
        {
            _context = context;
        }
        
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody]User model)
        {
            var user = UserRepository.Get(model.Username, model.Password);

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos"});

            var token = TokenService.GenerateToken(user);
            user.Password = "";
            return new
            {
                user = user,
                token = token
            };
        }

        [HttpGet("workflow")]
        [AllowAnonymous]
        public ActionResult<dynamic> GetWorkflow()
        {
            var workflow = _context.Etapas
                    .OrderBy(x => x.NumEtapa)
                    .Select(x => new {
                        x.NumEtapa,
                        TipoEtapa = x.TipoEtapa == 0 ? "Pergunta" : "Diálogo",
                        x.TextoEtapa,
                        x.NumProxEtapa,
                        Respostas = _context.Respostas
                            .Where(y => y.NumEtapa == x.NumEtapa)
                            .Select(y => new {
                                y.NumResposta,
                                y.NumEtapa,
                                y.Legenda,
                                y.NumProxEtapa
                            }).ToList()
                    }).ToList();

            return workflow;
        }

        #region Etapas
        [HttpGet("etapas")]
        [Authorize]
        public ActionResult<List<Etapa>> GetAllEtapas() =>
            _context.Etapas.OrderBy(x => x.NumEtapa).ToList();

        [HttpGet("etapas/{NumEtapa}")]
        [Authorize]
        public async Task<ActionResult<Etapa>> GetByNum(int NumEtapa)
        {
            var etapa = await _context.Etapas.FindAsync(NumEtapa);

            if (etapa == null)
                return NotFound();

            return etapa;
        }

        [HttpGet("etapas/{NumEtapa}/respostas")]
        [Authorize]
        public ActionResult<List<Resposta>> GetByNumEtapa(int NumEtapa)
        {
            var respostas = _context.Respostas.Where(x => x.NumEtapa == NumEtapa).ToList();

            if (respostas == null)
                return NotFound();

            return respostas;
        }

        [HttpPost("etapas")]
        [Authorize]
        public async Task<IActionResult> Save(Etapa etapa)
        {
            _context.Etapas.Add(etapa);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByNumEtapa), new { NumEtapa = etapa.NumEtapa }, etapa);
        }
        
        [HttpPut("etapas/{NumEtapa}")]
        [Authorize]
        public async Task<IActionResult> Update(int NumEtapa, Etapa etapa)
        {
            if (NumEtapa != etapa.NumEtapa)
                return BadRequest();

            _context.Entry(etapa).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("etapas/{NumEtapa}")]
        [Authorize]
        public async Task<IActionResult> DeleteEtapas(int NumEtapa)
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
        #endregion
        
        #region Respostas
        [HttpGet("respostas")]
        [Authorize]
        public ActionResult<List<Resposta>> GetAllRespostas() =>
            _context.Respostas.OrderBy(x => x.NumResposta).ToList();
        
        [HttpGet("respostas/{NumReposta}")]
        [Authorize]
        public async Task<ActionResult<Resposta>> GetByNumResposta(int NumReposta)
        {
            var resposta = await _context.Respostas.FindAsync(NumReposta);

            if (resposta == null)
                return NotFound();

            return resposta;

        }

        [HttpPost("respostas")]
        [Authorize]
        public async Task<IActionResult> Save(Resposta resposta)
        {
            _context.Respostas.Add(resposta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByNumResposta), new { NumResposta = resposta.NumResposta }, resposta);
        }

        [HttpPut("respostas/{NumResposta}")]
        [Authorize]
        public async Task<IActionResult> Update(int NumResposta, Resposta resposta)
        {
            if (NumResposta != resposta.NumResposta)
                return BadRequest();

            _context.Entry(resposta).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        [HttpDelete("respostas/{NumResposta}")]
        [Authorize]
        public async Task<IActionResult> DeleteRespostas(int NumResposta)
        {
            Resposta resposta = await _context.Respostas.FindAsync(NumResposta);
            
            if (resposta == null)
                return NotFound();

            _context.Respostas.Remove(resposta);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion
    }
}