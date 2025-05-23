using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeYourRestaurantApiV1.Models
{
    public partial class OrderList
    {
        [Key]
        public int Id { get; set; }

        public int TicketId { get; set; }

        public int? MealId { get; set; }

        public int? Quantity { get; set; }

        public bool? Done { get; set; }

        [ForeignKey("MealId")]
        public virtual Meal? Meal { get; set; }

        [ForeignKey("TicketId")]
        public virtual Ticket Ticket { get; set; }
    }
}
