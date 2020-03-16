using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractIceCreamShopBusinessLogic.ViewModels
{
   public class ReportIceCreamComponentViewModel
    {
        public string ComponentName { get; set; }
        public int TotalCount { get; set; }
        public List<Tuple<string, int>> IceCreams { get; set; }
    }
}
