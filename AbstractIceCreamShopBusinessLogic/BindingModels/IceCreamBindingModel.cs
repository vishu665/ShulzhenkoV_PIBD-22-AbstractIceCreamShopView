using System;
using System.Collections.Generic;
using System.Text;
using AbstractIceCreamShopBusinessLogic.Enums;

namespace AbstractIceCreamShopBusinessLogic.BindingModels
{
    public class IceCreamBindingModel
    {
        public int? Id { get; set; }
        public string IceCreamName { get; set; }
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> IceCreamComponents { get; set; }
    }
}
