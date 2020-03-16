using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AbstractIceCreamShopDatabaseImplement.Models
{
   public class IceCreamComponent
    {
        public int Id { get; set; }
        public int IceCreamId { get; set; }
        public int ComponentId { get; set; }
        [Required]
        public int Count { get; set; }
        public virtual Component Component { get; set; }
        public virtual IceCream IceCream { get; set; }
    }
}
