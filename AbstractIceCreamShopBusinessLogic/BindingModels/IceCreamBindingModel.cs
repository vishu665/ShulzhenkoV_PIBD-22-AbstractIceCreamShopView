using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractIceCreamShopBusinessLogic.BindingModels
{
    public class IceCreamBindingModel
    {
        public int Id { get; set; }
        public string IceCreamName { get; set; }
        public decimal Price { get; set; }
        public List<IceCreamComponentBindingModel> iceCreamComponents { get; set; }
    }
}
