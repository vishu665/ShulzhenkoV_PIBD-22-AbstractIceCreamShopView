using AbstractIceCreamShopBusinessLogic.BindingModels;
using AbstractIceCreamShopBusinessLogic.Interfaces;
using AbstractIceCreamShopBusinessLogic.ViewModels;
using AbstractIceCreamShopFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace AbstractIceCreamShopFileImplement.Implements
{
    public class IceCreamLogic : IIceCreamLogic
    {
        private readonly FileDataListSingleton source;
        public IceCreamLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public List<IceCreamViewModel> GetList()
        {
            List<IceCreamViewModel> result = source.IceCreams
            .Select(rec => new IceCreamViewModel
            {
                Id = rec.Id,
                IceCreamName = rec.IceCreamName,
                Price = rec.Price,
                IceCreamComponents = source.IceCreamComponents
            .Where(recPC => recPC.IceCreamId == rec.Id)
           .Select(recPC => new IceCreamComponentViewModel
           {
               Id = recPC.Id,
               IceCreamId = recPC.IceCreamId,
               ComponentId = recPC.ComponentId,
               ComponentName = source.Components.FirstOrDefault(recC => recC.Id == recPC.ComponentId)?.ComponentName,
               Count = recPC.Count
           })
           .ToList()
            })
            .ToList();
            return result;
        }
        public IceCreamViewModel GetElement(int id)
        {
            IceCream element = source.IceCreams.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new IceCreamViewModel
                {
                    Id = element.Id,
                    IceCreamName = element.IceCreamName,
                    Price = element.Price,
                    IceCreamComponents = source.IceCreamComponents
                    .Where(recPC => recPC.IceCreamId == element.Id)
                    .Select(recPC => new IceCreamComponentViewModel
                    {
                        Id = recPC.Id,
                        IceCreamId = recPC.IceCreamId,
                        ComponentId = recPC.ComponentId,
                        ComponentName = source.Components.FirstOrDefault(recC => recC.Id == recPC.ComponentId)?.ComponentName,
                        Count = recPC.Count
                    })
                    .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(IceCreamBindingModel model)
        {
            IceCream element = source.IceCreams.FirstOrDefault(rec => rec.IceCreamName ==
           model.IceCreamName);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            int maxId = source.IceCreams.Count > 0 ? source.IceCreams.Max(rec => rec.Id): 0;
            source.IceCreams.Add(new IceCream
            {
                Id = maxId + 1,
                IceCreamName = model.IceCreamName,
                Price = model.Price
            });
            // компоненты для изделия
            int maxPCId = source.IceCreamComponents.Count > 0 ?
           source.IceCreamComponents.Max(rec => rec.Id) : 0;
            // убираем дубли по компонентам
            var groupComponents = model.iceCreamComponents
            .GroupBy(rec => rec.ComponentId)
           .Select(rec => new
           {
               ComponentId = rec.Key,
               Count = rec.Sum(r => r.Count)
           });
            // добавляем компоненты
            foreach (var groupComponent in groupComponents)
            {
                source.IceCreamComponents.Add(new IceCreamComponent
                {
                    Id = ++maxPCId,
                    IceCreamId = maxId + 1,
                    ComponentId = groupComponent.ComponentId,
                    Count = groupComponent.Count
                });
            }
        }
        public void UpdElement(IceCreamBindingModel model)
        {
            IceCream element = source.IceCreams.FirstOrDefault(rec => rec.IceCreamName ==
           model.IceCreamName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            element = source.IceCreams.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.IceCreamName = model.IceCreamName;
            element.Price = model.Price;
            int maxPCId = source.IceCreamComponents.Count > 0 ?
           source.IceCreamComponents.Max(rec => rec.Id) : 0;
            // обновляем существуюущие компоненты
            var compIds = model.iceCreamComponents.Select(rec =>
           rec.ComponentId).Distinct();
            var updateComponents = source.IceCreamComponents.Where(rec => rec.IceCreamId
           == model.Id && compIds.Contains(rec.ComponentId));
            foreach (var updateComponent in updateComponents)
            {
                updateComponent.Count = model.iceCreamComponents.FirstOrDefault(rec =>
               rec.Id == updateComponent.Id).Count;
            }
            source.IceCreamComponents.RemoveAll(rec => rec.IceCreamId == model.Id &&
           !compIds.Contains(rec.ComponentId));
            // новые записи
            var groupComponents = model.iceCreamComponents
            .Where(rec => rec.Id == 0)
           .GroupBy(rec => rec.ComponentId)
           .Select(rec => new
           {
               ComponentId = rec.Key,
               Count = rec.Sum(r => r.Count)
           });
            foreach (var groupComponent in groupComponents)
            {
                IceCreamComponent elementPC =
               source.IceCreamComponents.FirstOrDefault(rec => rec.IceCreamId == model.Id &&
               rec.ComponentId == groupComponent.ComponentId);
                if (elementPC != null)
                {
                    elementPC.Count += groupComponent.Count;
                }
                else
                {
                    source.IceCreamComponents.Add(new IceCreamComponent
                    {
                        Id = ++maxPCId,
                        IceCreamId = model.Id,
                        ComponentId = groupComponent.ComponentId,
                        Count = groupComponent.Count
                    });
                }
            }
        }
        public void DelElement(int id)
        {
            IceCream element = source.IceCreams.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                // удаяем записи по компонентам при удалении изделия
                source.IceCreamComponents.RemoveAll(rec => rec.IceCreamId == id);
                source.IceCreams.Remove(element);

            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
