using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractIceCreamShopBusinessLogic.BindingModels
{
   public class CreateOrderBindingModel
    {
        public int IceCreamId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
    }
}
