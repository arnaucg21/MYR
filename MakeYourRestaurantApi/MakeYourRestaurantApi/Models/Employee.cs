using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeYourRestaurantApiV1.Models
{
    public partial class Employee
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Restaurant")]
        public int? RestaurantId { get; set; }

        [StringLength(25)]
        public string? Username { get; set; }

        [StringLength(25)]
        public string? Password { get; set; }

        [StringLength(25)]
        public string? Role { get; set; }

        public virtual Restaurant? Restaurant { get; set; }
    }
}
