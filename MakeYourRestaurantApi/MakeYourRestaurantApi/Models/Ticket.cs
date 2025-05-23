using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeYourRestaurantApiV1.Models
{
    public partial class Ticket
    {
        public Ticket()
        {
            OrderLists = new HashSet<OrderList>();
        }

        [Key]
        public int Id { get; set; }

        public int? TableNumber { get; set; }

        public DateTime? Date { get; set; }

        public double? TotalPrice { get; set; }

        public bool? Billed { get; set; }

        [ForeignKey("Restaurant")]
        public int? RestaurantId { get; set; }

        public string? MenuType { get; set; }


        public virtual Restaurant? Restaurant { get; set; }

        public virtual ICollection<OrderList> OrderLists { get; set; }
    }
}
