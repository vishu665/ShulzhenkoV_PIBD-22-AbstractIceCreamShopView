using AbstractIceCreamShopBusinessLogic.BindingModels;
using AbstractIceCreamShopBusinessLogic.Enums;
using AbstractIceCreamShopBusinessLogic.Interfaces;
using AbstractIceCreamShopBusinessLogic.ViewModels;
using AbstractIceCreamShopFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractIceCreamShopFileImplement.Implements
{
    public class MainLogic : IMainLogic

    {
        private readonly FileDataListSingleton source;
        public MainLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public List<OrderViewModel> GetOrders()

        {
            List<OrderViewModel> result = source.Orders.Select(rec => new OrderViewModel
            {
                Id = rec.Id,
                IceCreamId = rec.IceCreamId,
                IceCreamName = source.IceCreams.FirstOrDefault()?.IceCreamName,
                Count = rec.Count,
                Sum = rec.Sum,
                DateCreate = rec.DateCreate,
                DateImplement = rec.DateImplement,
                Status = rec.Status

            })
            .ToList();
            return result;

        }

        public void CreateOrder(OrderBindingModel model)
        {
            int maxId = source.Orders.Count > 0 ? source.Orders.Max(rec => rec.Id) : 0;
            source.Orders.Add(new Order
            {
                Id = maxId + 1,
                IceCreamId = model.IceCreamId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = OrderStatus.Принят

            });
        }

        public void TakeOrderInWork(OrderBindingModel model)

        {
            Order element = source.Orders.FirstOrDefault(rec => rec.Status != OrderStatus.Принят);
            if (element != null)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }
            element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            element.DateImplement = DateTime.Now;
            element.Status = OrderStatus.Выполняется;
        }

        public void FinishOrder(OrderBindingModel model)
        {
            Order element = source.Orders.FirstOrDefault(rec => rec.Status != OrderStatus.Выполняется);
            if (element != null)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = OrderStatus.Готов;
        }

        public void PayOrder(OrderBindingModel model)
        {
            Order element = source.Orders.FirstOrDefault(rec => rec.Status != OrderStatus.Готов);
            if (element != null)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }

            element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = OrderStatus.Оплачен;
        }
    }
}
