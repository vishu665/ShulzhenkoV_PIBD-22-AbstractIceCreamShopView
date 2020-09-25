﻿using AbstractIceCreamShopBusinessLogic.BindingModels;
using AbstractIceCreamShopBusinessLogic.Enums;
using AbstractIceCreamShopBusinessLogic.ViewModels;
using AbstractIceCreamShopListImplement.Models;
using AbstractIceCreamShopBusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractIceCreamShopListImplement.Implements
{
   public class OrderLogic : IOrderLogic
    {
        private readonly DataListSingleton source;
        public OrderLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(OrderBindingModel model)
        {
            Order tempOrder = model.Id.HasValue ? null : new Order { Id = 1 };

            foreach (var order in source.Orders)
            {
                if (!model.Id.HasValue && order.Id >= tempOrder.Id)
                {
                    tempOrder.Id = order.Id + 1;
                }
                else if (model.Id.HasValue && order.Id == model.Id)
                {
                    tempOrder = order;
                }
            }

            if (model.Id.HasValue)
            {
                if (tempOrder == null)
                {
                    throw new Exception("Элемент не найден");
                }

                CreateModel(model, tempOrder);
            }
            else
            {
                source.Orders.Add(CreateModel(model, tempOrder));
            }
        }

        public void Delete(OrderBindingModel model)
        {
            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Orders[i].Id == model.Id)
                {
                    source.Orders.RemoveAt(i);
                    return;
                }
            }

            throw new Exception("Элемент не найден");
        }

        private Order CreateModel(OrderBindingModel model, Order order)
        {
            order.IceCreamId = model.IceCreamId;
            order.Count = model.Count;
            order.DateCreate = model.DateCreate;
            order.ClientId = (int)model.ClientId;
            order.DateImplement = model.DateImplement;
            order.Sum = model.Sum;
            order.Status = model.Status;

            return order;
        }

        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            List<OrderViewModel> result = new List<OrderViewModel>();

            foreach (var order in source.Orders)
            {
                if ((model != null && order.Id == model.Id)
                    || (model.DateFrom.HasValue && model.DateTo.HasValue && order.DateCreate >= model.DateFrom && order.DateCreate <= model.DateTo)
                    || (model.ClientId.HasValue && order.ClientId == model.ClientId)
                    || (model.FreeOrders.HasValue && model.FreeOrders.Value)
                    || (model.ImplementerId.HasValue && order.ImplementerId == model.ImplementerId && order.Status == OrderStatus.Выполняется)
                )
                {
                    result.Add(CreateViewModel(order));
                    break;
                }
            }

            return result;
        }

        private OrderViewModel CreateViewModel(Order order)
        {
            string iceCreamtName = null;

            foreach (var iceCream in source.IceCreams)
            {
                if (iceCream.Id == order.IceCreamId)
                {
                    iceCreamtName = iceCream.IceCreamName;
                }
            }

            if (iceCreamtName == null)
            {
                throw new Exception("Продукт не найден");
            }

            return new OrderViewModel
            {
                Id = order.Id,
                ClientId = order.ClientId,
                IceCreamId = order.IceCreamId,
                IceCreamName = iceCreamtName,
                Count = order.Count,
                Sum = order.Sum,
                Status = order.Status,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement
            };
        }
    }
}

