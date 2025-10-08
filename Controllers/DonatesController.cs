using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GlobalLinkAPI.Data;
using GlobalLinkAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace GlobalLinkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonatesController : ControllerBase
    {
        private readonly GlobalLinkDbContext _context;

        public DonatesController(GlobalLinkDbContext context)
        {
            _context = context;
        }

        // GET: api/Donates
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<DonateDTO>>> GetDonations()
        {
            var donates = await _context.Donations
                .Include(d => d.Empresa)
                .Include(d => d.Ong)
                .Select(d => new DonateDTO
                {
                    Id = d.Id,
                    EmpresaId = d.EmpresaId,
                    OngId = d.OngId,
                    Tipo = d.Tipo,
                    Observacoes = d.Observacoes,
                    Status = d.Status,
                    EmpresaNome = d.Empresa != null ? d.Empresa.EmpresaNome : "Desconhecido",
                    OngNome = d.Ong != null ? d.Ong.OngNome : "Desconhecido"
                })
                .ToListAsync();

            return Ok(donates);
        }

        // GET: api/Donates/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<DonateDTO>> GetDonate(int id)
        {
            var donate = await _context.Donations
                .Include(d => d.Empresa)
                .Include(d => d.Ong)
                .Where(d => d.Id == id)
                .Select(d => new DonateDTO
                {
                    Id = d.Id,
                    EmpresaId = d.EmpresaId,
                    OngId = d.OngId,
                    Tipo = d.Tipo,
                    Observacoes = d.Observacoes,
                    Status = d.Status,
                    EmpresaNome = d.Empresa != null ? d.Empresa.EmpresaNome : "Desconhecido",
                    OngNome = d.Ong != null ? d.Ong.OngNome : "Desconhecido"
                })
                .FirstOrDefaultAsync();

            if (donate == null) return NotFound();

            return Ok(donate);
        }

        // PUT: api/Donates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> PutDonate(int id, Donate donate)
        {
            if (id != donate.Id)
            {
                return BadRequest();
            }

            _context.Entry(donate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonateExists(id))
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

        // POST: api/Donates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<DonateDTO>> PostDonate(DonateDTO dto)
        {
            var donate = new Donate
            {
                EmpresaId = dto.EmpresaId,
                OngId = dto.OngId,
                Tipo = dto.Tipo,
                Observacoes = dto.Observacoes,
                Status = dto.Status,
                DataCriacao = DateTime.UtcNow
            };

            _context.Donations.Add(donate);
            await _context.SaveChangesAsync();

            // Retorna DTO com nomes preenchidos
            var result = await _context.Donations
                .Include(d => d.Empresa)
                .Include(d => d.Ong)
                .Where(d => d.Id == donate.Id)
                .Select(d => new DonateDTO
                {
                    Id = d.Id,
                    EmpresaId = d.EmpresaId,
                    OngId = d.OngId,
                    Tipo = d.Tipo,
                    Observacoes = d.Observacoes,
                    Status = d.Status,
                    EmpresaNome = d.Empresa != null ? d.Empresa.EmpresaNome : "Desconhecido",
                    OngNome = d.Ong != null ? d.Ong.OngNome : "Desconhecido"
                })
                .FirstOrDefaultAsync();

            return CreatedAtAction(nameof(GetDonate), new { id = donate.Id }, result);
        }

        // DELETE: api/Donates/5
        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteDonate(int id)
        {
            var donate = await _context.Donations.FindAsync(id);
            if (donate == null) return NotFound();

            _context.Donations.Remove(donate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DonateExists(int id)
        {
            return _context.Donations.Any(e => e.Id == id);
        }
    }
}
