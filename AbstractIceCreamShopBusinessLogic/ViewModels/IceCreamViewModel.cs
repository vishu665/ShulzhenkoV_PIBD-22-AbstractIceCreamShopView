using System;
using AbstractIceCreamShopBusinessLogic.Attributes;
using AbstractIceCreamShopBusinessLogic.Enums;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace AbstractIceCreamShopBusinessLogic.ViewModels
{
    public class IceCreamViewModel : BaseViewModel
    {
        [DataMember]
        [Column(title: "Название изделия", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string IceCreamName { get; set; }
        [Column(title: "Цена", width: 100)]
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public Dictionary<int, (string, int)> IceCreamComponents { get; set; }
        public override List<string> Properties() => new List<string>
        {
            "Id",
            "IceCreamName",
            "Price"
        };
    }
}
