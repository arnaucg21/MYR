using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MakeYourRestaurantApiV1.Models;

namespace MakeYourRestaurantApiV1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PricesController : ControllerBase
    {
        private readonly MakeYourRestaurantContext _context;

        public PricesController(MakeYourRestaurantContext context)
        {
            _context = context;
        }

        // GET: api/Prices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Price>>> GetPrices()
        {
            return await _context.Prices.ToListAsync();
        }

        // GET: api/Prices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Price>> GetPrice(int id)
        {
            var price = await _context.Prices.FindAsync(id);
            return price == null ? NotFound() : price;
        }

        // GET: api/Prices/restaurant/5
        [HttpGet("restaurant/{restaurantId}")]
        public async Task<ActionResult<IEnumerable<Price>>> GetPricesByRestaurantId(int restaurantId)
        {
            var prices = await _context.Prices
                .Where(p => p.RestaurantId == restaurantId)
                .ToListAsync();

            if (prices.Count == 0)
                return NotFound($"No prices found for restaurant ID: {restaurantId}");

            return prices;
        }

        // PUT: api/Prices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrice(int id, Price price)
        {
            if (id != price.Id)
                return BadRequest();

            _context.Entry(price).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PriceExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/Prices
        [HttpPost]
        public async Task<ActionResult<Price>> PostPrice(Price price)
        {
            if (_context.Prices == null)
                return Problem("Entity set 'MakeYourRestaurantContext.Prices' is null.");

            _context.Prices.Add(price);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPrice), new { id = price.Id }, price);
        }

        // DELETE: api/Prices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrice(int id)
        {
            var price = await _context.Prices.FindAsync(id);
            if (price == null)
                return NotFound();

            _context.Prices.Remove(price);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PriceExists(int id)
        {
            return _context.Prices?.Any(e => e.Id == id) ?? false;
        }
    }
}
