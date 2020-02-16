using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractIceCreamShopBusinessLogic.BindingModels
{
    public class IceCreamComponentBindingModel
    {
        public int Id { get; set; }

         public int IceCreamId { get; set; }
        public int ComponentId { get; set; }
        public int Count { get; set; }

    }
}
