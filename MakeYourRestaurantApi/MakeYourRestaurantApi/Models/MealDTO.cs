using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeYourRestaurantApiV1.Models
{
    public class MealDto
    {
        public int Id { get; set; }  
        [Required]
        public int RestaurantId { get; set; }

        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public double? Price { get; set; }

        [StringLength(200)]
        public string? Details { get; set; }

        public bool BestFood { get; set; } = false;

        [StringLength(200)]
        public string? MenuType { get; set; }

        public string? PhotoFile { get; set; }
    }

}
