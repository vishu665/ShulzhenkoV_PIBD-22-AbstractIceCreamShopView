using MigraDoc.DocumentObjectModel;
using System;
using System.Collections.Generic;
using System.Text;

using MigraDoc.DocumentObjectModel.Tables;

namespace AbstractIceCreamShopBusinessLogic.HelperModels
{
    class PdfRowParameters
    {
        public Table Table { get; set; }
        public List<string> Texts { get; set; }
        public string Style { get; set; }
        public ParagraphAlignment ParagraphAlignment { get; set; }

    }
}
