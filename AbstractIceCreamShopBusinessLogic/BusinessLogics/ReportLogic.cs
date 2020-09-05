﻿using AbstractIceCreamShopBusinessLogic.BindingModels;
using AbstractIceCreamShopBusinessLogic.HelperModels;
using AbstractIceCreamShopBusinessLogic.Interfaces;
using AbstractIceCreamShopBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            var iceCreams = iceCreamLogic.Read(null);
            var list = new List<ReportIceCreamComponentViewModel>();

            foreach (var iceCream in iceCreams)
            {
                foreach (var pc in iceCream.IceCreamComponents)
                {
                    var record = new ReportIceCreamComponentViewModel
                    {
                        IceCreamName = iceCream.IceCreamName,
                        ComponentName = pc.Value.Item1,
                        Count = pc.Value.Item2
                    };

                    list.Add(record);
                }
            }
            return list;
        }
        /// <summary>
        /// Получение списка заказов за определенный период
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<IGrouping<DateTime, OrderViewModel>> GetOrders(ReportBindingModel model)
        {
            var list = orderLogic
            .Read(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .GroupBy(rec => rec.DateCreate.Date)
            .OrderBy(recG => recG.Key)
            .ToList();

            return list;
        }
        public void SaveProductsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список изделий",
                IceCreams = iceCreamLogic.Read(null)
            });
        }
        /// <summary>
        /// Сохранение компонент с указаеним продуктов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SaveProductComponentsToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список издлий с компонентами",
                IceCreamComponents = GetIceCreamComponent()
            });
        }

        /// <summary>
        /// Сохранение заказов в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        public void SaveOrdersToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                Orders = GetOrders(model)
            });
        }
    }

}
