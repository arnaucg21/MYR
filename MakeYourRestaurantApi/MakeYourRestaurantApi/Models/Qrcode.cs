using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeYourRestaurantApiV1.Models
{
    public partial class Qrcode
    {
        public Qrcode()
        {
            RestaurantTables = new HashSet<RestaurantTable>();
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey("Restaurant")]
        public int? RestaurantId { get; set; }

        public int? TableNumber { get; set; }

        [StringLength(200)]
        public string? Photo { get; set; }

        public virtual Restaurant? Restaurant { get; set; }

        public virtual ICollection<RestaurantTable> RestaurantTables { get; set; }
    }
}
