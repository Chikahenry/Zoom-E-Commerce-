using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.Data.Models;
using ZoomApi.Context;

namespace ZoomApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyersApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BuyersApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BuyersApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Buyer>>> GetBuyer()
        {
            return await _context.Buyer.ToListAsync();
        }

        // GET: api/BuyersApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Buyer>> GetBuyer(int id)
        {
            var buyer = await _context.Buyer.FindAsync(id);

            if (buyer == null)
            {
                return NotFound();
            }

            return buyer;
        }

        // PUT: api/BuyersApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuyer(int id, Buyer buyer)
        {
            if (id != buyer.BuyerID)
            {
                return BadRequest();
            }

            _context.Entry(buyer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuyerExists(id))
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

        // POST: api/BuyersApi
        [HttpPost]
        public async Task<ActionResult<Buyer>> PostBuyer(Buyer buyer)
        {
            _context.Buyer.Add(buyer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBuyer", new { id = buyer.BuyerID }, buyer);
        }

        // DELETE: api/BuyersApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Buyer>> DeleteBuyer(int id)
        {
            var buyer = await _context.Buyer.FindAsync(id);
            if (buyer == null)
            {
                return NotFound();
            }

            _context.Buyer.Remove(buyer);
            await _context.SaveChangesAsync();

            return buyer;
        }

        private bool BuyerExists(int id)
        {
            return _context.Buyer.Any(e => e.BuyerID == id);
        }
    }
}
