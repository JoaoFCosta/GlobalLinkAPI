using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GlobalLinkAPI.Data;
using GlobalLinkAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace GlobalLinkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NeedsController : ControllerBase
    {
        private readonly GlobalLinkDbContext _context;

        public NeedsController(GlobalLinkDbContext context)
        {
            _context = context;
        }

        // 🟢 GET: api/Needs (público)
        // GET: api/Needs
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<NeedDTO>>> GetNeeds()
        {
            var needs = await _context.Needs
                .Include(n => n.Ong)
                .Select(n => new NeedDTO
                {
                    NecessidadeId = n.NecessidadeId,
                    NecessidadeTitulo = n.NecessidadeTitulo,
                    NecessidadeDescricao = n.NecessidadeDescricao,
                    Urgencia = n.Urgencia,
                    Categoria = n.Categoria,
                    Local = n.Local,
                    DataCriacao = n.DataCriacao,
                    OngId = n.OngId,
                    OngNome = n.Ong != null ? n.Ong.OngNome : "Desconhecido"
                })
                .ToListAsync();

            return Ok(needs);
        }

        // GET: api/Needs/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<NeedDTO>> GetNeed(int id)
        {
            var need = await _context.Needs
                .Include(n => n.Ong)
                .Where(n => n.NecessidadeId == id)
                .Select(n => new NeedDTO
                {
                    NecessidadeId = n.NecessidadeId,
                    NecessidadeTitulo = n.NecessidadeTitulo,
                    NecessidadeDescricao = n.NecessidadeDescricao,
                    Urgencia = n.Urgencia,
                    Categoria = n.Categoria,
                    Local = n.Local,
                    DataCriacao = n.DataCriacao,
                    OngId = n.OngId,
                    OngNome = n.Ong != null ? n.Ong.OngNome : "Desconhecido"
                })
                .FirstOrDefaultAsync();

            if (need == null)
                return NotFound();

            return Ok(need);
        }

        // POST: api/Needs
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<NeedDTO>> PostNeed(NeedDTO dto)
        {
            // Valida se a ONG existe
            var ong = await _context.Ongs.FindAsync(dto.OngId);
            if (ong == null)
                return BadRequest("ONG não encontrada.");

            // Cria a necessidade
            var need = new Need
            {
                NecessidadeTitulo = dto.NecessidadeTitulo,
                NecessidadeDescricao = dto.NecessidadeDescricao,
                Urgencia = dto.Urgencia,
                Categoria = dto.Categoria,
                Local = dto.Local,
                DataCriacao = DateTime.UtcNow,
                OngId = dto.OngId
            };

            _context.Needs.Add(need);
            await _context.SaveChangesAsync();

            // Retorna o DTO com nome da ONG
            dto.NecessidadeId = need.NecessidadeId;
            dto.DataCriacao = need.DataCriacao;
            dto.OngNome = ong.OngNome;

            return CreatedAtAction(nameof(GetNeed), new { id = need.NecessidadeId }, dto);
        }

        // PUT: api/Needs/5
        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> PutNeed(int id, NeedDTO dto)
        {
            if (id != dto.NecessidadeId)
                return BadRequest();

            // Valida se a ONG existe
            var ong = await _context.Ongs.FindAsync(dto.OngId);
            if (ong == null)
                return BadRequest("ONG não encontrada.");

            // Atualiza a necessidade
            var need = await _context.Needs.FindAsync(id);
            if (need == null)
                return NotFound();

            need.NecessidadeTitulo = dto.NecessidadeTitulo;
            need.NecessidadeDescricao = dto.NecessidadeDescricao;
            need.Urgencia = dto.Urgencia;
            need.Categoria = dto.Categoria;
            need.Local = dto.Local;
            need.DataCriacao = DateTime.UtcNow;
            need.OngId = dto.OngId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Needs/5
        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteNeed(int id)
        {
            var need = await _context.Needs.FindAsync(id);
            if (need == null)
                return NotFound();

            _context.Needs.Remove(need);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
