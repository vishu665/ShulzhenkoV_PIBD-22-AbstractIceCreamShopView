using AbstractIceCreamShopBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractIceCreamShopBusinessLogic.ViewModels
{
    public class ReportOrdersViewModel
    {
        public DateTime DateCreate { get; set; }
        public string IceCreamName { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public OrderStatus Status { get; set; }
    }
}
