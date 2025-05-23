using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeYourRestaurantApiV1.Models
{
    public partial class Category
    {
        public Category()
        {
            Meals = new HashSet<Meal>();
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey("Restaurant")]
        public int? RestaurantId { get; set; }

        [StringLength(50)]
        public string? Name { get; set; }

        [StringLength(200)]
        public string? Photo { get; set; }

        public virtual Restaurant? Restaurant { get; set; }
        public virtual ICollection<Meal> Meals { get; set; }
    }
}
