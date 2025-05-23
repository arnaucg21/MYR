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
    public class TicketsController : ControllerBase
    {
        private readonly MakeYourRestaurantContext _context;

        public TicketsController(MakeYourRestaurantContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetAllTickets()
        {
            return await _context.Tickets
                .Include(t => t.OrderLists)
                .Include(t => t.Restaurant)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
            var ticket = await _context.Tickets
                .Include(t => t.OrderLists)
                .Include(t => t.Restaurant)
                .FirstOrDefaultAsync(t => t.Id == id);

            return ticket == null ? NotFound() : ticket;
        }

        [HttpGet("restaurant/{restaurantId}/count/billed")]
        public async Task<ActionResult<int>> GetBilledCount(int restaurantId)
        {
            return await _context.Tickets.CountAsync(t => t.RestaurantId == restaurantId && t.Billed == true);
        }

        [HttpGet("restaurant/{restaurantId}/count/received")]
        public async Task<ActionResult<int>> GetReceivedCount(int restaurantId)
        {
            return await _context.Tickets.CountAsync(t => t.RestaurantId == restaurantId && t.Billed == false);
        }

        [HttpPut("{restaurantId}/table/{tableNumber}/markserved")]
        public async Task<IActionResult> MarkTableAsServed(int restaurantId, int tableNumber)
        {
            // Find the open (not billed) ticket for this table and restaurant
            var ticket = await _context.Tickets
                .Where(t => t.RestaurantId == restaurantId && t.TableNumber == tableNumber && t.Billed == false)
                .FirstOrDefaultAsync();

            if (ticket == null)
                return NotFound("No open ticket found for this table.");

            // Mark as billed
            ticket.Billed = true;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                ticket.Id,
                Message = "Table marked as served and ticket billed successfully."
            });
        }


        [HttpGet("restaurant/{restaurantId}/billed")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetBilledTickets(int restaurantId)
        {
            return await _context.Tickets
                .Where(t => t.RestaurantId == restaurantId && t.Billed == true)
                .ToListAsync();
        }

        [HttpGet("restaurant/{restaurantId}/received")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetReceivedTickets(int restaurantId)
        {
            return await _context.Tickets
                .Where(t => t.RestaurantId == restaurantId && t.Billed == false)
                .ToListAsync();
        }

        [HttpGet("restaurant/{restaurantId}/all")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetAllByRestaurant(int restaurantId)
        {
            var tickets = await _context.Tickets
                .Where(t => t.RestaurantId == restaurantId)
                .ToListAsync();

            return tickets.Count == 0 ? NotFound("No tickets found for this restaurant.") : tickets;
        }

        [HttpGet("restaurant/{restaurantId}/table/billed")]
        public async Task<ActionResult<IEnumerable<int?>>> GetBilledTableNumbers(int restaurantId)
        {
            return await _context.Tickets
                .Where(t => t.RestaurantId == restaurantId && t.Billed == true)
                .Select(t => t.TableNumber)
                .Distinct()
                .ToListAsync();
        }

        [HttpGet("restaurant/{restaurantId}/table/received")]
        public async Task<ActionResult<IEnumerable<int?>>> GetReceivedTableNumbers(int restaurantId)
        {
            return await _context.Tickets
                .Where(t => t.RestaurantId == restaurantId && t.Billed == false)
                .Select(t => t.TableNumber)
                .Distinct()
                .ToListAsync();
        }

        [HttpGet("restaurant/{restaurantId}/table/{tableNumber}/billed")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetBilledByTable(int restaurantId, int tableNumber)
        {
            return await _context.Tickets
                .Where(t => t.RestaurantId == restaurantId && t.TableNumber == tableNumber && t.Billed == true)
                .ToListAsync();
        }

        [HttpGet("restaurant/{restaurantId}/table/{tableNumber}/received")]
        public async Task<ActionResult<Ticket>> GetReceivedByTable(int restaurantId, int tableNumber)
        {
            var ticket = await _context.Tickets
                .Where(t => t.RestaurantId == restaurantId && t.TableNumber == tableNumber && t.Billed == false)
                .FirstOrDefaultAsync();

            if (ticket == null)
            {
                return NotFound(); // Return 404 if no ticket found
            }

            return ticket;
        }


        [HttpGet("restaurant/{restaurantId}/ticket/{ticketId}")]
        public async Task<ActionResult<Ticket>> GetTicketByRestaurant(int restaurantId, int ticketId)
        {
            var ticket = await _context.Tickets
                .FirstOrDefaultAsync(t => t.RestaurantId == restaurantId && t.Id == ticketId);

            return ticket == null ? NotFound() : ticket;
        }

        [HttpGet("restaurant/{restaurantId}/received/full")]
        public async Task<ActionResult> GetReceivedFull(int restaurantId)
        {
            var tickets = await _context.Tickets
                .Where(t => t.RestaurantId == restaurantId && t.Billed == false)
                .Include(t => t.OrderLists)
                    .ThenInclude(ol => ol.Meal)
                .ToListAsync();

            var result = tickets.Select(t => new
            {
                ticketId = t.Id,
                tableNumber = t.TableNumber,
                date = t.Date,
                meals = t.OrderLists.Select(ol => new
                {
                    mealId = ol.MealId,
                    name = ol.Meal?.Name,
                    quantity = ol.Quantity,
                    done = ol.Done
                })
            });

            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(int id, Ticket ticket)
        {
            if (id != ticket.Id)
                return BadRequest();

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostTicket(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return ticket.Id;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
                return NotFound();

            var lines = _context.OrderLists.Where(l => l.TicketId == id);
            _context.OrderLists.RemoveRange(lines);

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("createorder")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest request)
        {
            if (request.Meals == null || !request.Meals.Any())
                return BadRequest("No meals selected.");

            var ticket = new Ticket
            {
                TableNumber = request.TableNumber,
                RestaurantId = request.RestaurantId,
                MenuType = request.MenuType,
                Date = DateTime.Now,
                Billed = false
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            double totalPrice = 0;  // total price accumulator
            
            foreach (var meal in request.Meals)
            {
                // Get the Meal Price from DB
                var mealInfo = await _context.Meals.FindAsync(meal.MealId);
                if (mealInfo == null)
                    return BadRequest($"Meal with ID {meal.MealId} does not exist.");

                var orderList = new OrderList
                {
                    TicketId = ticket.Id,
                    MealId = meal.MealId,
                    Quantity = meal.Quantity
                };

                _context.OrderLists.Add(orderList);

                // Calculate price: Meal.Price * Quantity
                totalPrice += (mealInfo.Price ?? 0) * meal.Quantity;
            }

            // Update ticket with calculated total price
            ticket.TotalPrice = totalPrice;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                TicketId = ticket.Id,
                TotalPrice = ticket.TotalPrice,
                Message = "Order created successfully"
            });
        }


        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(t => t.Id == id);
        }
    }
}
