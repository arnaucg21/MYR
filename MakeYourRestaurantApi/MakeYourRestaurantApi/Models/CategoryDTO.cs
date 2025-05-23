using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeYourRestaurantApiV1.Models
{
    public class CategoryDto
    {
        public int Id { get; set; }              
        public int RestaurantId { get; set; }    
        public string Name { get; set; }      
        public string? Photo { get; set; }   
    }

}
