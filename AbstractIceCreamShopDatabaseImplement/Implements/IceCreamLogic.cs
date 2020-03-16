using AbstractIceCreamShopBusinessLogic.BindingModels;
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
    public class IceCreamLogic : IIceCreamLogic
    {
        public void CreateOrUpdate(IceCreamBindingModel model)
        {
            using (var context = new IceCreamShopDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        IceCream element = context.IceCreams.FirstOrDefault(rec =>
                       rec.IceCreamName == model.IceCreamName && rec.Id != model.Id);
                        if (element != null)
                        {
                            throw new Exception("Уже есть изделие с таким названием");
                        }
                        if (model.Id.HasValue)
                        {
                            element = context.IceCreams.FirstOrDefault(rec => rec.Id ==
                           model.Id);
                            if (element == null)
                            {
                                throw new Exception("Элемент не найден");
                            }
                        }
                        else
                        {
                            element = new IceCream();
                            context.IceCreams.Add(element);
                        }
                        element.IceCreamName = model.IceCreamName;
                        element.Price = model.Price;
                        context.SaveChanges();
                        if (model.Id.HasValue)
                        {
                            var productComponents = context.IceCreamComponents.Where(rec
                           => rec.IceCreamId == model.Id.Value).ToList();
                            // удалили те, которых нет в модели

                            context.IceCreamComponents.RemoveRange(productComponents.Where(rec =>
                            !model.IceCreamComponents.ContainsKey(rec.ComponentId)).ToList());
                            context.SaveChanges();
                            // обновили количество у существующих записей
                            foreach (var updateComponent in productComponents)
                            {
                                updateComponent.Count =
                               model.IceCreamComponents[updateComponent.ComponentId].Item2;

                                model.IceCreamComponents.Remove(updateComponent.ComponentId);
                            }
                            context.SaveChanges();
                        }
                        // добавили новые
                        foreach (var pc in model.IceCreamComponents)
                        {
                            context.IceCreamComponents.Add(new IceCreamComponent
                            {
                                IceCreamId = element.Id,
                                ComponentId = pc.Key,
                                Count = pc.Value.Item2
                            });
                            context.SaveChanges();
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Delete(IceCreamBindingModel model)
        {
            using (var context = new IceCreamShopDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.IceCreamComponents.RemoveRange(context.IceCreamComponents.Where(rec =>
                        rec.IceCreamId == model.Id));
                        IceCream element = context.IceCreams.FirstOrDefault(rec => rec.Id
                        == model.Id);
                        if (element != null)
                        {
                            context.IceCreams.Remove(element);
                            context.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Элемент не найден");
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public List<IceCreamViewModel> Read(IceCreamBindingModel model)
        {
            using (var context = new IceCreamShopDatabase())
            {
                return context.IceCreams
                .Where(rec => model == null || rec.Id == model.Id)
                .ToList()
               .Select(rec => new IceCreamViewModel
               {
                   Id = rec.Id,
                   IceCreamName = rec.IceCreamName,
                   Price = rec.Price,
                   IceCreamComponents = context.IceCreamComponents.Include(recPC => recPC.Component)
               .Where(recPC => recPC.IceCreamId == rec.Id)
               .ToDictionary(recPC => recPC.ComponentId, recPC =>
                (recPC.Component?.ComponentName, recPC.Count))
               })
               .ToList();
            }
        }
    }
}
