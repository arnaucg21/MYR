using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeYourRestaurantApiV1.Models
{
    public class QrCodeDto
    {

        public int Id { get; set; }
        public int RestaurantId { get; set; }

        public int TableNumber { get; set; }

        public string? PhotoFile { get; set; }
    }

}
