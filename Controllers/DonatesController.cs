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
    [Authorize]
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
        public async Task<ActionResult<IEnumerable<Donate>>> GetDonations()
        {
            return await _context.Donations.ToListAsync();
        }

        // GET: api/Donates/5
        [HttpGet("{id}")]
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
        public async Task<ActionResult<Donate>> PostDonate(Donate donate)
        {
            _context.Donations.Add(donate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDonate", new { id = donate.Id }, donate);
        }

        // DELETE: api/Donates/5
        [HttpDelete("{id}")]
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
