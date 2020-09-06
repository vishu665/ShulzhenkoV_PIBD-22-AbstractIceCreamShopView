﻿using AbstractIceCreamShopBusinessLogic.BindingModels;
using AbstractIceCreamShopBusinessLogic.Interfaces;
using AbstractIceCreamShopBusinessLogic.ViewModels;
using AbstractIceCreamShopDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using AbstractIceCreamShopBusinessLogic.Enums;

namespace AbstractIceCreamShopDatabaseImplement.Implements
{
    public class OrderLogic : IOrderLogic
    {
        public void CreateOrUpdate(OrderBindingModel model)
        {
            using (var context = new IceCreamShopDatabase())
            {
                Order element;
                if (model.Id.HasValue)
                {
                    element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Order { };
                    context.Orders.Add(element);
                }
                element.IceCreamId = model.IceCreamId == 0 ? element.IceCreamId : model.IceCreamId;
                element.ClientId = model.ClientId.Value;
                element.Count = model.Count;
                element.ImplementerId = model.ImplementerId;
                element.Sum = model.Sum;
                element.Status = model.Status;
                element.DateCreate = model.DateCreate;
                element.DateImplement = model.DateImplement;
                context.SaveChanges();
            }
        }
        public void Delete(OrderBindingModel model)
        {
            using (var context = new IceCreamShopDatabase())
            {
                Order element = context.Orders.FirstOrDefault(rec => rec.Id ==
                        model.Id);
                if (element != null)
                {
                    context.Orders.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            using (var context = new IceCreamShopDatabase())
            {
                return context.Orders
               .Where(rec => model == null || (rec.Id == model.Id && model.Id.HasValue) ||
               (model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo) ||
               (model.ClientId.HasValue && rec.ClientId == model.ClientId) ||
               (model.FreeOrders.HasValue && model.FreeOrders.Value && !rec.ImplementerId.HasValue) ||
               (model.ImplementerId.HasValue && rec.ImplementerId == model.ImplementerId && rec.Status == OrderStatus.Выполняется))
               .Include(rec => rec.IceCream)
               .Include(rec => rec.Client)
               .Include(rec => rec.Implementer)
               .Select(rec => new OrderViewModel
               {
                   Id = rec.Id,
                   ClientId = rec.ClientId,
                   ImplementerId = rec.ImplementerId,
                   IceCreamId = rec.IceCreamId,
                   DateCreate = rec.DateCreate,
                   DateImplement = rec.DateImplement,
                   Status = rec.Status,
                   Count = rec.Count,
                   Sum = rec.Sum,
                   IceCreamName = rec.IceCream.IceCreamName,
                   ClientFIO = rec.Client.FIO,
                   ImplementerFIO = rec.ImplementerId.HasValue ? rec.Implementer.ImplementerFIO : string.Empty,

               })
               .ToList();
            }
        }
    }
}
