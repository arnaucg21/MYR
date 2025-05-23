using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MakeYourRestaurantApiV1.Models;   // your EF entities
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeYourRestaurantApiV1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QrcodesController : ControllerBase
    {
        private readonly MakeYourRestaurantContext _context;

        public QrcodesController(MakeYourRestaurantContext context)
        {
            _context = context;
        }

        // POST: api/Qrcodes
        // Creates a new QR record and returns the created DTO (with its new Id).
        [HttpPost]
        public async Task<ActionResult<QrCodeDto>> Create([FromBody] QrCodeDto dto)
        {
            var entity = new Qrcode
            {
                RestaurantId = dto.RestaurantId,
                TableNumber = dto.TableNumber
            };

            _context.Qrcodes.Add(entity);
            await _context.SaveChangesAsync();

            dto.Id = entity.Id;
            // dto.PhotoFile = entity.PhotoFile; // if you generated it here

            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        // GET: api/Qrcodes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<QrCodeDto>> GetById(int id)
        {
            var e = await _context.Qrcodes.FindAsync(id);
            if (e == null) return NotFound();
            return new QrCodeDto
            {
                Id = e.Id,
                RestaurantId = (int)e.RestaurantId,
                TableNumber = (int)e.TableNumber
            };
        }

        // GET: api/Qrcodes/restaurant/{restaurantId}
        [HttpGet("restaurant/{restaurantId}")]
        public async Task<ActionResult<IEnumerable<QrCodeDto>>> GetByRestaurant(int restaurantId)
        {
            var list = await _context.Qrcodes
                .Where(q => q.RestaurantId == restaurantId)
                .Select(q => new QrCodeDto
                {
                    Id = q.Id,
                    RestaurantId = (int)q.RestaurantId,
                    TableNumber = (int)q.TableNumber
                })
                .ToListAsync();

            return Ok(list);  // even if empty
        }

        // GET: api/Qrcodes/resolveqr/{qrId}?menuType=DailyMenu
        // Returns exactly { TableNumber, RestaurantId, MenuType, Meals } 
        // as your existing code already does.
        [HttpGet("resolveqr/{qrId}")]
        public async Task<IActionResult> ResolveQr(int qrId, [FromQuery] string menuType = "DailyMenu")
        {
            var qr = await _context.Qrcodes.FindAsync(qrId);
            if (qr == null)
                return NotFound("QR code not found.");

            var meals = await _context.Meals
                .Where(m => m.RestaurantId == qr.RestaurantId
                         && ("," + m.MenuTypes + ",")
                              .Contains("," + menuType + ","))
                .ToListAsync();

            return Ok(new
            {
                TableNumber = qr.TableNumber,
                RestaurantId = qr.RestaurantId,
                MenuType = menuType,
                Meals = meals
            });
        }
    }
}
