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
    public class RestaurantTablesController : ControllerBase
    {
        private readonly MakeYourRestaurantContext _context;

        public RestaurantTablesController(MakeYourRestaurantContext context)
        {
            _context = context;
        }

        // GET: api/RestaurantTables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantTable>>> GetAll()
        {
            return await _context.RestaurantTables
                .Include(t => t.Restaurant)
                .Include(t => t.Qrcode)
                .ToListAsync();
        }

        // GET: api/RestaurantTables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantTable>> GetById(int id)
        {
            var table = await _context.RestaurantTables
                .Include(t => t.Restaurant)
                .Include(t => t.Qrcode)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (table == null)
                return NotFound($"Table with ID {id} not found.");

            return table;
        }

        // GET: api/RestaurantTables/restaurant/3
        [HttpGet("restaurant/{restaurantId}")]
        public async Task<ActionResult<IEnumerable<RestaurantTable>>> GetByRestaurant(int restaurantId)
        {
            var tables = await _context.RestaurantTables
                .Where(t => t.RestaurantId == restaurantId)
                .Include(t => t.Restaurant)
                .Include(t => t.Qrcode)
                .ToListAsync();

            if (!tables.Any())
                return NotFound($"No tables found for restaurant ID {restaurantId}");

            return tables;
        }

        // POST: api/RestaurantTables
        [HttpPost]
        public async Task<ActionResult<RestaurantTable>> Create(RestaurantTable table)
        {
            if (table.RestaurantId == null)
                return BadRequest("RestaurantId is required.");

            var restaurantExists = await _context.Restaurants.AnyAsync(r => r.Id == table.RestaurantId);
            if (!restaurantExists)
                return BadRequest($"Restaurant with ID {table.RestaurantId} does not exist.");

            if (table.QrcodeId != null)
            {
                var qrExists = await _context.Qrcodes.AnyAsync(q => q.Id == table.QrcodeId);
                if (!qrExists)
                    return BadRequest($"QR Code with ID {table.QrcodeId} does not exist.");
            }

            _context.RestaurantTables.Add(table);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = table.Id }, table);
        }

        // PUT: api/RestaurantTables/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RestaurantTable table)
        {
            if (id != table.Id)
                return BadRequest("ID mismatch.");

            if (!_context.RestaurantTables.Any(t => t.Id == id))
                return NotFound($"Table with ID {id} not found.");

            if (table.RestaurantId != null)
            {
                var restaurantExists = await _context.Restaurants.AnyAsync(r => r.Id == table.RestaurantId);
                if (!restaurantExists)
                    return BadRequest($"Restaurant with ID {table.RestaurantId} does not exist.");
            }

            if (table.QrcodeId != null)
            {
                var qrExists = await _context.Qrcodes.AnyAsync(q => q.Id == table.QrcodeId);
                if (!qrExists)
                    return BadRequest($"QR Code with ID {table.QrcodeId} does not exist.");
            }

            _context.Entry(table).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "A concurrency error occurred while updating the table.");
            }
        }

        // DELETE: api/RestaurantTables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var table = await _context.RestaurantTables.FindAsync(id);
            if (table == null)
                return NotFound($"Table with ID {id} not found.");

            _context.RestaurantTables.Remove(table);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
