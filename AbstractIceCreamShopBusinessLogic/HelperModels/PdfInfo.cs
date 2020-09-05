using AbstractIceCreamShopBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractIceCreamShopBusinessLogic.HelperModels
{
    class PdfInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportIceCreamComponentViewModel> IceCreamComponents { get; set; }
    }
}
