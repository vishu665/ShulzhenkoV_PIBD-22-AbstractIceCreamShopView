using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AbstractIceCreamShopDatabaseImplement.Models
{
   public class IceCream
    {
        public int Id { get; set; }
        [Required]
        public string IceCreamName { get; set; }
        [Required]
        public decimal Price { get; set; }

        public virtual List<Order> Orders { get; set; }
        public virtual List<IceCreamComponent> IceCreamComponents { get; set; }
    }
}
