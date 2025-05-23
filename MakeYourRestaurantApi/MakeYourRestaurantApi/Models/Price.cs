using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeYourRestaurantApiV1.Models
{
    public partial class Price
    {
        public Price()
        {
            Meals = new HashSet<Meal>();
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey("Restaurant")]
        public int? RestaurantId { get; set; }

        [StringLength(10)]
        public string? Value { get; set; }

        public double? MinRange { get; set; }

        public double? MaxRange { get; set; }

        public virtual Restaurant? Restaurant { get; set; }
        public virtual ICollection<Meal> Meals { get; set; }
    }
}
