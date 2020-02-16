using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace AbstractIceCreamShopBusinessLogic.ViewModels
{
    public class IceCreamViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название мороженого")]
        public string IceCreamName { get; set; }
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        public List<IceCreamComponentViewModel> IceCreamComponents { get; set; }
    }
}
