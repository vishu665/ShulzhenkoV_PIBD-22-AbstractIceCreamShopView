using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractIceCreamShopFileImplement.Models
{
   public class IceCreamComponent
    {
        public int Id { get; set; }
        public int IceCreamId { get; set; }
        public int ComponentId { get; set; }
        public int Count { get; set; }
    }
}
