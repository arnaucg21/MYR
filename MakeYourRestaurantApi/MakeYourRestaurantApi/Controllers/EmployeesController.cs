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
    public class EmployeesController : ControllerBase
    {
        private readonly MakeYourRestaurantContext _context;

        public EmployeesController(MakeYourRestaurantContext context)
        {
            _context = context;
        }

        // GET: api/Employees/restaurant/{restaurantId}
        [HttpGet("restaurant/{restaurantId}")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetByRestaurant(int restaurantId)
        {
            var emps = await _context.Employees
                .Where(e => e.RestaurantId == restaurantId)
                .ToListAsync();

            if (emps.Count == 0)
                return NotFound("No employees found.");

            // map to DTO
            var dtos = emps.Select(e => new EmployeeDto
            {
                Id = e.Id,
                RestaurantId = (int)e.RestaurantId,
                Username = e.Username,
                Password = e.Password,
                Role = e.Role
            }).ToList();

            return dtos;
        }

        // GET: api/Employees/auth/{username}/{password}
        [HttpGet("auth/{username}/{password}")]
        public async Task<ActionResult<EmployeeDto>> Authenticate(string username, string password)
        {
            var e = await _context.Employees
                .FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
            if (e == null)
                return NotFound("Invalid credentials.");

            return new EmployeeDto
            {
                Id = e.Id,
                RestaurantId = (int)e.RestaurantId,
                Username = e.Username,
                Password = e.Password,
                Role = e.Role
            };
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> Post([FromBody] EmployeeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var e = new Employee
            {
                RestaurantId = dto.RestaurantId,
                Username = dto.Username,
                Password = dto.Password,
                Role = dto.Role
            };

            _context.Employees.Add(e);
            await _context.SaveChangesAsync();

            dto.Id = e.Id;
            return CreatedAtAction(nameof(GetByRestaurant),
                                   new { restaurantId = e.RestaurantId },
                                   dto);
        }

        // PUT: api/Employees/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EmployeeDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var e = await _context.Employees.FindAsync(id);
            if (e == null)
                return NotFound($"Employee {id} not found.");

            // map updates
            e.Username = dto.Username;
            e.Password = dto.Password;
            e.Role = dto.Role;
            e.RestaurantId = dto.RestaurantId;

            _context.Entry(e).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!EmployeeExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Employees/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var e = await _context.Employees.FindAsync(id);
            if (e == null) return NotFound();

            _context.Employees.Remove(e);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool EmployeeExists(int id)
            => _context.Employees.Any(x => x.Id == id);
    }
}
