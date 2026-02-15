using Microsoft.AspNetCore.Mvc;
using MinhaApi.Models;
using MinhaApi.Services;
using MinhaApi.Data;
using Microsoft.EntityFrameworkCore;

namespace MinhaApi.Controllers
{
    [ApiController]
    [Route("api/lotes")]
    public class LotesExtrasController : ControllerBase
    {
        private readonly AppDbContext _ctx;
        private readonly LoteService _service;

        public LotesExtrasController(AppDbContext ctx, LoteService service)
        {
            _ctx = ctx;
            _service = service;
        }

        [HttpPost("{id}/mover")]
        public IActionResult Mover(int id, [FromBody] MovimentoDto dto)
        {
            var lote = _ctx.LotesMinerio.Include(l => l.Historico).FirstOrDefault(l => l.Id == id);
            if (lote == null) return NotFound();

            try 
            {
                lote.RegistrarMovimentacao(dto.Status, dto.Local);
                _ctx.SaveChanges();
                return Ok(lote);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/avancar-status")]
        public IActionResult AvancarStatus(int id)
        {
            var lote = _ctx.LotesMinerio.Include(l => l.Historico).FirstOrDefault(l => l.Id == id);
            if (lote == null) return NotFound();

            try 
            {
                _service.AvancarStatus(lote);
                _ctx.SaveChanges();
                return Ok(lote);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    public class MovimentoDto
    {
        public string Local { get; set; } = string.Empty;
        public StatusLote Status { get; set; }
    }
} 
}