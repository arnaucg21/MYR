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
    public class OrderListController : ControllerBase
    {
        private readonly MakeYourRestaurantContext _context;

        public OrderListController(MakeYourRestaurantContext context)
        {
            _context = context;
        }

        // GET: api/OrderList
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderList>>> GetOrderLists()
        {
            if (_context.OrderLists == null)
                return NotFound();

            return await _context.OrderLists.ToListAsync();
        }

        // GET: api/OrderList/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderList>> GetOrderList(int id)
        {
            if (_context.OrderLists == null)
                return NotFound();

            var orderList = await _context.OrderLists.FindAsync(id);

            if (orderList == null)
                return NotFound();

            return orderList;
        }

        // GET: api/OrderList/order/7
        [HttpGet("order/{ticketId}")]
        public async Task<ActionResult<IEnumerable<OrderList>>> GetOrderListsByTicketId(int ticketId)
        {
            if (_context.OrderLists == null)
                return NotFound();

            var items = await _context.OrderLists
                .Where(o => o.TicketId == ticketId)
                .ToListAsync();

            if (items.Count == 0)
                return NotFound($"No order lines found for ticket ID {ticketId}");

            return items;
        }

        // POST: api/OrderList
        [HttpPost]
        public async Task<ActionResult<OrderList>> PostOrderList(OrderList orderList)
        {
            if (_context.OrderLists == null)
                return Problem("Entity set 'MakeYourRestaurantContext.OrderLists' is null.");

            _context.OrderLists.Add(orderList);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrderList), new { id = orderList.Id }, orderList);
        }

        [HttpPost("updateorder")]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderRequest request)
        {
            if (request.Meals == null || !request.Meals.Any())
                return BadRequest("No meals selected.");

            // 1) Verify the ticket exists
            var ticket = await _context.Tickets.FindAsync(request.TicketId);
            if (ticket == null)
                return NotFound("Ticket not found.");

            // 2) Upsert each meal into OrderList
            foreach (var meal in request.Meals)
            {
                var line = await _context.OrderLists
                    .FirstOrDefaultAsync(o => o.TicketId == request.TicketId && o.MealId == meal.MealId);

                if (line != null)
                {
                    line.Quantity += meal.Quantity;
                    _context.OrderLists.Update(line);
                }
                else
                {
                    _context.OrderLists.Add(new OrderList
                    {
                        TicketId = request.TicketId,
                        MealId = meal.MealId,
                        Quantity = meal.Quantity
                    });
                }
            }

            // 3) Persist the upserts
            await _context.SaveChangesAsync();

            // 4) Recalculate total price from *all* order lines on the ticket
            var fullTotal = await _context.OrderLists
                .Where(o => o.TicketId == request.TicketId)
                .Include(o => o.Meal)              // so you can read .Meal.Price
                .SumAsync(o => (o.Meal.Price ?? 0) * o.Quantity);

            ticket.TotalPrice = fullTotal;

            // 5) Save the new total
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Order updated successfully",
                TotalPrice = ticket.TotalPrice
            });
        }



        [HttpPut("updateorderdone")]
        public async Task<IActionResult> UpdateOrderDone([FromBody] UpdateOrderDoneRequest request)
        {
            // Find the OrderList by TicketId and MealId
            var orderList = await _context.OrderLists
                .Where(ol => ol.TicketId == request.TicketId && ol.MealId == request.MealId)
                .FirstOrDefaultAsync();

            if (orderList == null)
                return NotFound("OrderList not found.");

            // Update the Done status
            orderList.Done = request.Done;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderListExists(orderList.Id))
                    return NotFound();
                else
                    throw;
            }

            return Ok(new
            {
                Message = "Order status updated successfully",
                OrderListId = orderList.Id,
                Done = orderList.Done
            });
        }

        // PUT: api/OrderList/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderList(int id, OrderList orderList)
        {
            if (id != orderList.Id)
                return BadRequest();

            _context.Entry(orderList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderListExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/OrderList/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderList(int id)
        {
            if (_context.OrderLists == null)
                return NotFound();

            var orderList = await _context.OrderLists.FindAsync(id);
            if (orderList == null)
                return NotFound();

            _context.OrderLists.Remove(orderList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderListExists(int id)
        {
            return _context.OrderLists?.Any(e => e.Id == id) ?? false;
        }
    }
}
