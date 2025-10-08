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
    public class OngsController : ControllerBase
    {
        private readonly GlobalLinkDbContext _context;

        public OngsController(GlobalLinkDbContext context)
        {
            _context = context;
        }

        // GET: api/Ongs
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Ong>>> GetOngs()
        {
            return await _context.Ongs.ToListAsync();
        }

        // GET: api/Ongs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ong>> GetOng(int id)
        {
            var ong = await _context.Ongs.FindAsync(id);

            if (ong == null)
            {
                return NotFound();
            }

            return ong;
        }

        // PUT: api/Ongs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutOng(int id, Ong ong)
        {
            if (id != ong.OngId)
            {
                return BadRequest();
            }

            _context.Entry(ong).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OngExists(id))
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

        // POST: api/Ongs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Ong>> PostOng(Ong ong)
        {
            _context.Ongs.Add(ong);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOng", new { id = ong.OngId }, ong);
        }

        // DELETE: api/Ongs/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteOng(int id)
        {
            var ong = await _context.Ongs.FindAsync(id);
            if (ong == null)
            {
                return NotFound();
            }

            _context.Ongs.Remove(ong);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OngExists(int id)
        {
            return _context.Ongs.Any(e => e.OngId == id);
        }
    }
}
