using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeYourRestaurantApiV1.Models
{
    public partial class RestaurantTable
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Restaurant")]
        public int? RestaurantId { get; set; }

        public int? TableNumber { get; set; }

        [ForeignKey("Qrcode")]
        public int? QrcodeId { get; set; }

        public virtual Restaurant? Restaurant { get; set; }
        public virtual Qrcode? Qrcode { get; set; }
    }
}
