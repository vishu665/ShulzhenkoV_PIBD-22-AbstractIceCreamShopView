﻿using AbstractIceCreamShopBusinessLogic.BindingModels;
using AbstractIceCreamShopBusinessLogic.Interfaces;
using AbstractIceCreamShopBusinessLogic.ViewModels;
using AbstractIceCreamShopDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                element.ClientId = model.ClientId == null ? element.ClientId : (int)model.ClientId;
                element.Count = model.Count;
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
                 .Where(rec => model == null || rec.Id == model.Id && model.Id.HasValue
                    || model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo
                    || model.ClientId.HasValue && rec.ClientId == model.ClientId)
                 .Include(rec => rec.Client)
                 .Select(rec => new OrderViewModel
                 {
                     Id = rec.Id,
                     ClientId = rec.ClientId,
                     IceCreamId = rec.IceCreamId,
                     Count = rec.Count,
                     Sum = rec.Sum,
                     Status = rec.Status,
                     DateCreate = rec.DateCreate,
                     DateImplement = rec.DateImplement,
                     IceCreamName = rec.IceCream.IceCreamName,
                     ClientFIO = rec.Client.FIO
                 })
            .ToList();
            }
        }
    }
}
