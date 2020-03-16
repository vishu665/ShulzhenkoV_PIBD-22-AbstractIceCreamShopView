using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace AbstractIceCreamShopBusinessLogic.ViewModels
{
    public class IceCreamViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название изделия")]
        public string IceCreamName { get; set; }
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> IceCreamComponents { get; set; }
    }
}
