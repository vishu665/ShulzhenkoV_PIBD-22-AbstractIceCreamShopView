using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractIceCreamShopListImplement.Models
{
   public class IceCream
    {
        public int Id { get; set; }
        public string IceCreameName { get; set; }
        public decimal Price { get; set; }
        public string IceCreamName { get; internal set; }
    }
}
