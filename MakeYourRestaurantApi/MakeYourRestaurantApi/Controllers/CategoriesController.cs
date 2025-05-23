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
    public class CategoriesController : ControllerBase
    {
        private readonly MakeYourRestaurantContext _context;
        public CategoriesController(MakeYourRestaurantContext context)
            => _context = context;

        // map EF entity → DTO
        private static CategoryDto ToDto(Category c) => new CategoryDto
        {
            Id = c.Id,
            RestaurantId = c.RestaurantId ?? 0,
            Name = c.Name,
            Photo = c.Photo
        };

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
        {
            var list = await _context.Categories.ToListAsync();
            return list.Select(ToDto).ToList();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {
            var c = await _context.Categories.FindAsync(id);
            if (c == null) return NotFound();
            return ToDto(c);
        }

        // GET: api/Categories/restaurant/2
        [HttpGet("restaurant/{restaurantId}")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetByRestaurant(int restaurantId)
        {
            var list = await _context.Categories
                                     .Where(c => c.RestaurantId == restaurantId)
                                     .ToListAsync();
            if (!list.Any()) return NotFound();
            return list.Select(ToDto).ToList();
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Create(CategoryDto dto)
        {
            var entity = new Category
            {
                RestaurantId = dto.RestaurantId,
                Name = dto.Name,
                Photo = dto.Photo
            };
            _context.Categories.Add(entity);
            await _context.SaveChangesAsync();

            dto.Id = entity.Id;
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryDto dto)
        {
            if (id != dto.Id) return BadRequest();

            var entity = await _context.Categories.FindAsync(id);
            if (entity == null) return NotFound();

            entity.Name = dto.Name;
            entity.Photo = dto.Photo;
            entity.RestaurantId = dto.RestaurantId;

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET /api/Categories/{categoryId}/Meals
        [HttpGet("{categoryId}/Meals")]
        public async Task<ActionResult<IEnumerable<MealDto>>> GetMealsInCategory(int categoryId)
        {
            var meals = await _context.Meals
                .Where(m => m.CategoryId == categoryId)
                .ToListAsync();

            if (!meals.Any()) return NotFound();
            // map to your MealDto here (you already have that mapping elsewhere)
            return meals.Select(m => new MealDto
            {
                Id = m.Id,
                RestaurantId = m.RestaurantId,
                CategoryId = m.CategoryId,
                Name = m.Name,
                Price = m.Price,
                Details = m.Details,
                BestFood = m.BestFood,
                MenuType = m.MenuTypes,
                PhotoFile = null  // or however you handle images
            }).ToList();
        }

        // POST /api/Categories/{categoryId}/Meals/{mealId}
        [HttpPost("{categoryId}/Meals/{mealId}")]
        public async Task<IActionResult> AssignMeal(int categoryId, int mealId)
        {
            var meal = await _context.Meals.FindAsync(mealId);
            if (meal == null) return NotFound($"Meal {mealId} not found.");
            meal.CategoryId = categoryId;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE /api/Categories/{categoryId}/Meals/{mealId}
        [HttpDelete("{categoryId}/Meals/{mealId}")]
        public async Task<IActionResult> UnassignMeal(int categoryId, int mealId)
        {
            var meal = await _context.Meals.FindAsync(mealId);
            if (meal == null) return NotFound($"Meal {mealId} not found.");
            if (meal.CategoryId != categoryId) return BadRequest("Meal is not in this category.");
            meal.CategoryId = null;
            await _context.SaveChangesAsync();
            return NoContent();
        }


        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.Categories.FindAsync(id);
            if (entity == null) return NotFound();
            _context.Categories.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
