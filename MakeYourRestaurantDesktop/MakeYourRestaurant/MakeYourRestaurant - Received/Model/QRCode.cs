//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MakeYourRestaurant___Received.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class QRCode
    {
        public int ID { get; set; }
        public Nullable<int> RestaurantId { get; set; }
        public Nullable<int> TableNumber { get; set; }
        public string Photo { get; set; }
    
        public virtual Restaurant Restaurant { get; set; }
    }
}
