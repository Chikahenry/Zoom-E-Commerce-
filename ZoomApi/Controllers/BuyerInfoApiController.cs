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
    public class BuyerInfoApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BuyerInfoApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BuyerInfoApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BuyerInfo>>> GetBuyerInfo()
        {
            return await _context.BuyerInfos.ToListAsync();
        }

        // GET: api/BuyerInfoApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BuyerInfo>> GetBuyerInfo(int id)
        {
            var buyerInfo = await _context.BuyerInfos.FindAsync(id);

            if (buyerInfo == null)
            {
                return NotFound();
            }

            return buyerInfo;
        }

        // PUT: api/BuyerInfoApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuyerInfo(int id, BuyerInfo buyerInfo)
        {
            if (id != buyerInfo.BuyerInfoID)
            {
                return BadRequest();
            }

            _context.Entry(buyerInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuyerInfoExists(id))
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

        // POST: api/BuyerInfoApi
        [HttpPost]
        public async Task<ActionResult<BuyerInfo>> PostBuyerInfo(BuyerInfo buyerInfo)
        {
            _context.BuyerInfos.Add(buyerInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBuyerInfo", new { id = buyerInfo.BuyerInfoID }, buyerInfo);
        }

        // DELETE: api/BuyerInfoApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BuyerInfo>> DeleteBuyerInfo(int id)
        {
            var buyerInfo = await _context.BuyerInfos.FindAsync(id);
            if (buyerInfo == null)
            {
                return NotFound();
            }

            _context.BuyerInfos.Remove(buyerInfo);
            await _context.SaveChangesAsync();

            return buyerInfo;
        }

        private bool BuyerInfoExists(int id)
        {
            return _context.BuyerInfos.Any(e => e.BuyerInfoID == id);
        }
    }
}
