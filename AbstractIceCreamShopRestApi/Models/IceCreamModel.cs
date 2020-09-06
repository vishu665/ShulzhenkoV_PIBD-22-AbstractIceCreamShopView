using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbstractIceCreamShopRestApi.Models
{
    public class IceCreamModel
    {
        public int Id { get; set; }

        public string IceCreamName { get; set; }

        public decimal Price { get; set; }
    }
}
