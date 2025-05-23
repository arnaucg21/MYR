using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeYourRestaurantApiV1.Models
{
    public partial class Meal
    {
        public Meal()
        {
            OrderLists = new HashSet<OrderList>(); // Relation with OrderList
        }

        [Key]
        public int Id { get; set; }

        public int? CategoryId { get; set; } // Optional Category (nullable)

        public int RestaurantId { get; set; } // Required Restaurant ID

        public double? Price { get; set; } // Optional Price

        [StringLength(50)]
        public string Name { get; set; } // Name of the Meal

        [StringLength(200)]
        public string? Details { get; set; } // Details about the meal (optional)

        public bool BestFood { get; set; } // Whether it's a "BestFood", default to false

        [StringLength(200)]
        public string? Photo { get; set; } // Path to the photo (optional)

        [StringLength(50)]
        public string? MenuTypes { get; set; } // MenuType (e.g., Daily Menu, Buffet, etc.)

        // Navigation properties for related data
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; } // Optional Category relation

        [ForeignKey("RestaurantId")]
        public virtual Restaurant Restaurant { get; set; } // Relation to Restaurant

        public virtual ICollection<OrderList> OrderLists { get; set; } // Relation to OrderList
    }
}
