using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MakeYourRestaurantApiV1.Models;

namespace MakeYourRestaurantApiV1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MealsController : ControllerBase
    {
        private readonly MakeYourRestaurantContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public MealsController(MakeYourRestaurantContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Meal>>> GetMeals()
        {
            return await _context.Meals.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Meal>> GetMeal(int id)
        {
            var meal = await _context.Meals.FindAsync(id);
            return meal is null ? NotFound() : meal;
        }

        [HttpPost]
        // in Controllers/MealsController.cs
        public async Task<IActionResult> PostMeal(MealDto dto)
        {
            // 1) Validate DTO (ModelState will catch [Required], etc.)
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 2) Map DTO → entity
            var meal = new Meal
            {
                Name = dto.Name,
                Details = dto.Details,
                Price = dto.Price,
                BestFood = dto.BestFood,
                MenuTypes = dto.MenuType,
                CategoryId = dto.CategoryId,
                RestaurantId = dto.RestaurantId
            };

            // 3) Handle optional photo URL
            if (!string.IsNullOrWhiteSpace(dto.PhotoFile))
            {
                // We assume the client passed a URL or relative path
                meal.Photo = dto.PhotoFile;
            }


            // 4) Persist
            _context.Meals.Add(meal);
            await _context.SaveChangesAsync();

            // 5) Return a trimmed-down response
            return CreatedAtAction(
                nameof(GetMeal),
                new { id = meal.Id },
                new
                {
                    meal.Id,
                    meal.Name,
                    meal.Price,
                    meal.Details,
                    meal.BestFood,
                    meal.MenuTypes,
                    meal.Photo,
                    meal.CategoryId,
                    meal.RestaurantId
                }
            );
        }


        // In your MealsController.cs

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeal(int id, MealDto dto)
        {
            // 1) Ensure the incoming model is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 2) Make sure the URL id matches the DTO's id (if you include it on the DTO)
            if (dto.Id != 0 && dto.Id != id)
                return BadRequest("Route ID and DTO Id must match.");

            // 3) Load the existing entity
            var meal = await _context.Meals.FindAsync(id);
            if (meal == null)
                return NotFound($"Meal with id {id} not found.");

            // 4) Map the DTO fields onto the entity
            meal.RestaurantId = dto.RestaurantId;
            meal.CategoryId = dto.CategoryId;
            meal.Name = dto.Name;
            meal.Price = dto.Price;
            meal.Details = dto.Details;
            meal.BestFood = dto.BestFood;
            meal.MenuTypes = dto.MenuType;
            // If you support file uploads:
            // if (dto.PhotoFile != null && dto.PhotoFile.Length > 0) { ... }

            // 5) Tell EF to save the changes
            _context.Entry(meal).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!_context.Meals.Any(e => e.Id == id)
)
            {
                return NotFound();
            }

            // 6) 204 No Content to indicate success
            return NoContent();
        }

        // GET /api/Meals/restaurant/{restaurantId}/menutype/{menuType}
        [HttpGet("restaurant/{restaurantId}/menutype/{menuType}")]
        public async Task<ActionResult<IEnumerable<MealDto>>> GetByMenuType(int restaurantId, string menuType)
        {
            var meals = await _context.Meals
                .Where(m => m.RestaurantId == restaurantId && m.MenuTypes == menuType)
                .Select(m => new MealDto
                {
                    RestaurantId = m.RestaurantId,
                    CategoryId = m.CategoryId,
                    Name = m.Name,
                    Price = m.Price,
                    Details = m.Details,
                    BestFood = m.BestFood,
                    MenuType = m.MenuTypes,
                    PhotoFile = m.Photo   // or whatever your DTO property is called
                })
                .ToListAsync();

            if (!meals.Any())
                return NotFound($"No {menuType} meals found.");

            return meals;
        }

        [HttpGet("category/{catId}")]
        public async Task<ActionResult<IEnumerable<MealDto>>> GetByCategory(int catId)
        {
            var ms = await _context.Meals.Where(m => m.CategoryId == catId).ToListAsync();
            return Ok(ms);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeal(int id)
        {
            var meal = await _context.Meals.FindAsync(id);
            if (meal == null)
                return NotFound();

            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("name/{mealId}")]
        public async Task<ActionResult<string>> GetMealNameById(int mealId)
        {
            var name = await _context.Meals
                .Where(e => e.Id == mealId)
                .Select(m => m.Name)
                .FirstOrDefaultAsync();

            return name == null ? NotFound("Meal name not found.") : Ok(name);
        }

        [HttpGet("price/{mealId}")]
        public async Task<ActionResult<double?>> GetMealPriceById(int mealId)
        {
            var price = await _context.Meals
                .Where(e => e.Id == mealId)
                .Select(m => m.Price)
                .FirstOrDefaultAsync();

            return price is null ? NotFound("Meal price not found.") : price;
        }

        [HttpGet("restaurant/{restaurantId}")]
        public async Task<ActionResult<IEnumerable<MealDto>>> GetMealsByRestaurant(int restaurantId)
        {
            var dtos = await _context.Meals
                .Where(m => m.RestaurantId == restaurantId)
                .Select(m => new MealDto
                {
                    Id = m.Id,
                    RestaurantId = m.RestaurantId,
                    CategoryId = m.CategoryId,
                    CategoryName = m.Category != null ? m.Category.Name : null,
                    Name = m.Name,
                    Price = m.Price,
                    Details = m.Details,
                    BestFood = m.BestFood,
                    MenuType = m.MenuTypes,
                    PhotoFile = m.Photo  
                })
                .ToListAsync();

            if (dtos.Count == 0)
                return NotFound("No meals found.");

            return Ok(dtos);
        }


        [HttpGet("restaurant/{restaurantId}/best")]
        public async Task<ActionResult<IEnumerable<Meal>>> GetBestMeals(int restaurantId)
        {
            var meals = await _context.Meals
                .Where(m => m.RestaurantId == restaurantId && m.BestFood == true)
                .ToListAsync();

            return meals.Count == 0 ? NotFound("No best food meals found.") : meals;
        }

        [HttpGet("restaurant/{restaurantId}/search/{name}")]
        public async Task<ActionResult<IEnumerable<Meal>>> GetMealsByName(int restaurantId, string name)
        {
            var meals = await _context.Meals
                .Where(m => m.RestaurantId == restaurantId && m.Name.Contains(name))
                .OrderBy(m => m.Name)
                .ToListAsync();

            return meals.Count == 0 ? NotFound("No meals matching name found.") : meals;
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Meal>>> GetMealsByCategory(int categoryId)
        {
            var meals = await _context.Meals
                .Where(m => m.CategoryId == categoryId)
                .ToListAsync();

            return meals.Count == 0 ? NotFound("No meals in this category.") : meals;
        }

        private bool MealExists(int id)
        {
            return _context.Meals?.Any(m => m.Id == id) ?? false;
        }
    }
}
