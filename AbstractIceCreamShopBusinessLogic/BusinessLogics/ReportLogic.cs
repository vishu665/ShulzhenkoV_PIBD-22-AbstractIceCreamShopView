using AbstractIceCreamShopBusinessLogic.BindingModels;
using AbstractIceCreamShopBusinessLogic.Interfaces;
using AbstractIceCreamShopBusinessLogic.ViewModels;
using AbstractIceCreamShopBusinessLogic.HelperModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AbstractIceCreamShopBusinessLogic.BusinessLogics
{
    public class ReportLogic
    {
        private readonly IComponentLogic componentLogic;
        private readonly IIceCreamLogic iceCreamLogic;
        private readonly IOrderLogic orderLogic;
        public ReportLogic(IIceCreamLogic iceCreamLogic, IComponentLogic componentLogic,
       IOrderLogic orderLLogic)
        {
            this.iceCreamLogic = iceCreamLogic;
            this.componentLogic = componentLogic;
            this.orderLogic = orderLLogic;
        }

       public List<ReportIceCreamComponentViewModel> GetIceCreamComponent()
      {
            var components = componentLogic.Read(null);
            var iceCreams = iceCreamLogic.Read(null);
            var list = new List<ReportIceCreamComponentViewModel>();
            foreach (var component in components)
            {
                var record = new ReportIceCreamComponentViewModel
                {
                    ComponentName = component.ComponentName,
                    IceCreams = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var iceCream in iceCreams)
                {
                    if (iceCream.IceCreamComponents.ContainsKey(component.Id))
                    {
                        record.IceCreams.Add(new Tuple<string, int>(iceCream.IceCreamName,
                       iceCream.IceCreamComponents[component.Id].Item2));
                        record.TotalCount +=
                       iceCream.IceCreamComponents[component.Id].Item2;
                    }
                }
                list.Add(record);
            }
            return list;
        }
        /// <summary>
        /// Получение списка заказов за определенный период
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            return orderLogic.Read(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .Select(x => new ReportOrdersViewModel
            {
                DateCreate = x.DateCreate,
                IceCreamName = x.IceCreamName,
                Count = x.Count,
                Sum = x.Sum,
                Status = x.Status
            })
           .ToList();
        }
        /// <summary>
        /// Сохранение компонент в файл-Word
        /// </summary>
        /// <param name="model"></param>
        public void SaveComponentsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
          
        {
                FileName = model.FileName,
        Title = "Список компонент",Components = componentLogic.Read(null)
 });
        }
        /// <summary>
        /// Сохранение компонент с указаеним продуктов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SaveIceCreamComponentToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список компонент",
                IceCreamComponents = GetIceCreamComponent()
            });
        }
    
        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Orders = GetOrders(model)
            });
        }
    }

}


