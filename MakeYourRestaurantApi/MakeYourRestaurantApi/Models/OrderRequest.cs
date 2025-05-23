using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeYourRestaurantApiV1.Models
{
    public class OrderRequest
    {
        public int RestaurantId { get; set; }
        public int TableNumber { get; set; }
        public string MenuType { get; set; }
        public int TicketId { get; set; }
        public List<MealOrder> Meals { get; set; }
    }

    public class MealOrder
    {
        public int MealId { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateOrderDoneRequest
    {
        public int TicketId { get; set; }
        public int MealId { get; set; }
        public bool Done { get; set; }
    }
}
