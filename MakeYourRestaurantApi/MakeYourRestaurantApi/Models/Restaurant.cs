using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MakeYourRestaurantApiV1.Models
{
    public partial class Restaurant
    {
        public Restaurant()
        {
            Categories = new HashSet<Category>();
            Employees = new HashSet<Employee>();
            Meals = new HashSet<Meal>();
            Orders = new HashSet<Ticket>();
            Prices = new HashSet<Price>();
            Qrcodes = new HashSet<Qrcode>();
            RestaurantTables = new HashSet<RestaurantTable>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string? Name { get; set; }

        [StringLength(200)]
        public string? Address { get; set; }

        [StringLength(20)]
        public string? Phone { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Meal> Meals { get; set; }
        public virtual ICollection<Ticket> Orders { get; set; }
        public virtual ICollection<Price> Prices { get; set; }
        public virtual ICollection<Qrcode> Qrcodes { get; set; }
        public virtual ICollection<RestaurantTable> RestaurantTables { get; set; } 
    }
}
