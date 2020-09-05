using AbstractIceCreamShopBusinessLogic.BindingModels;
using AbstractIceCreamShopBusinessLogic.Interfaces;
using AbstractIceCreamShopBusinessLogic.ViewModels;
using AbstractIceCreamShopFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractIceCreamShopFileImplement.Implements
{
    public class IceCreamLogic : IIceCreamLogic
    {
        private readonly FileDataListSingleton source;
        public IceCreamLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(IceCreamBindingModel model)
        {
            IceCream element = source.IceCreams.FirstOrDefault(rec => rec.IceCreamName ==
           model.IceCreamName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            if (model.Id.HasValue)
            {
                element = source.IceCreams.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
            }
            else
            {
                int maxId = source.IceCreams.Count > 0 ? source.Components.Max(rec =>
               rec.Id) : 0;
                element = new IceCream { Id = maxId + 1 };
                source.IceCreams.Add(element);
            }
            element.IceCreamName = model.IceCreamName;
            element.Price = model.Price;
            // удалили те, которых нет в модели
            source.IceCreamComponents.RemoveAll(rec => rec.IceCreamId == model.Id &&
           !model.IceCreamComponents.ContainsKey(rec.ComponentId));
            // обновили количество у существующих записей
            var updateComponents = source.IceCreamComponents.Where(rec => rec.IceCreamId ==
           model.Id && model.IceCreamComponents.ContainsKey(rec.ComponentId));
            foreach (var updateComponent in updateComponents)
            {
                updateComponent.Count =
               model.IceCreamComponents[updateComponent.ComponentId].Item2;
                model.IceCreamComponents.Remove(updateComponent.ComponentId);
            }
            // добавили новые
            int maxPCId = source.IceCreamComponents.Count > 0 ?
           source.IceCreamComponents.Max(rec => rec.Id) : 0;
            foreach (var pc in model.IceCreamComponents)
            {
                source.IceCreamComponents.Add(new IceCreamComponent
                {
                    Id = ++maxPCId,
                    IceCreamId = element.Id,
                    ComponentId = pc.Key,
                    Count = pc.Value.Item2
                });
            }
        }
        public void Delete(IceCreamBindingModel model)
        {
            // удаяем записи по компонентам при удалении изделия
            source.IceCreamComponents.RemoveAll(rec => rec.IceCreamId == model.Id);
            IceCream element = source.IceCreams.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.IceCreams.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public List<IceCreamViewModel> Read(IceCreamBindingModel model)
        {
            return source.IceCreams
            .Where(rec => model == null || rec.Id == model.Id)
            .Select(rec => new IceCreamViewModel
            {
                Id = rec.Id,
                IceCreamName = rec.IceCreamName,
                Price = rec.Price,
                IceCreamComponents = source.IceCreamComponents
            .Where(recPC => recPC.IceCreamId == rec.Id)
           .ToDictionary(recPC => recPC.ComponentId, recPC =>
            (source.Components.FirstOrDefault(recC => recC.Id ==
           recPC.ComponentId)?.ComponentName, recPC.Count))
            })
            .ToList();
        }
    }
}
