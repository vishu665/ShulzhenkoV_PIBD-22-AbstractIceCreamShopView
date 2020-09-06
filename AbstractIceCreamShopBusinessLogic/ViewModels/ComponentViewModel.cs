using System;
using AbstractIceCreamShopBusinessLogic.Attributes;
using AbstractIceCreamShopBusinessLogic.Enums;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;


namespace AbstractIceCreamShopBusinessLogic.ViewModels
{
    public class ComponentViewModel : BaseViewModel
    {
        [Column(title: "Название компонента", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ComponentName { get; set; }
        public override List<string> Properties() => new List<string>
        {
            "Id",
            "ComponentName"
        };
    }
}
