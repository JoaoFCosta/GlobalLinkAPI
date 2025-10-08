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
        .Include(d => d.Company)
        .Select(d => new DonateDTO
        {
            Id = d.Id,
            EmpresaNome = d.Company!.EmpresaNome,
            Tipo = d.Tipo,
            Observacoes = d.Observacoes,
            Status = d.Status
        })
        .ToListAsync();

            return Ok(donates);
        }

        // GET: api/Donates/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Donate>> GetDonate(int id)
        {
            var donate = await _context.Donations.FindAsync(id);

            if (donate == null)
            {
                return NotFound();
            }

            return donate;
        }

        // PUT: api/Donates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
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
            var result = new DonateDTO
            {
                Id = donate.Id,
                EmpresaId = donate.EmpresaId,
                OngId = donate.OngId,
                Tipo = donate.Tipo,
                Observacoes = donate.Observacoes,
                Status = donate.Status,
                EmpresaNome = (await _context.Companies.FindAsync(donate.EmpresaId))?.EmpresaNome ?? "Desconhecido",
                OngNome = (await _context.Ongs.FindAsync(donate.OngId))?.OngNome ?? "Desconhecido"
            };

            return CreatedAtAction(nameof(GetDonate), new { id = donate.Id }, result);
        }

        // DELETE: api/Donates/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteDonate(int id)
        {
            var donate = await _context.Donations.FindAsync(id);
            if (donate == null)
            {
                return NotFound();
            }

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
