using AbstractIceCreamShopBusinessLogic.BindingModels;
using AbstractIceCreamShopBusinessLogic.Enums;
using AbstractIceCreamShopBusinessLogic.BusinessLogics;
using AbstractIceCreamShopBusinessLogic.Interfaces;
using AbstractIceCreamShopBusinessLogic.ViewModels;
using AbstractIceCreamShopListImplement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractIceCreamShopListImplement.Implements
{
    public class IceCreamLogic : IIceCreamLogic
    {
        private readonly DataListSingleton source;

        public IceCreamLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(IceCreamBindingModel model)
        {
            IceCream tempProduct = model.Id.HasValue? null : new IceCream { Id = 1 };
            foreach (var iceCream in source.IceCreams)
            {
                if (iceCream.IceCreamName == model.IceCreamName && iceCream.Id != model.Id)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
                if (!model.Id.HasValue && iceCream.Id >= tempProduct.Id)
                {
                    tempProduct.Id = iceCream.Id + 1;
                }
                else if (model.Id.HasValue && iceCream.Id == model.Id)
                {
                    tempProduct = iceCream;
                }
            }
            if (model.Id.HasValue)
            {
                if (tempProduct == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, tempProduct);
            }
            else
            {
                source.IceCreams.Add(CreateModel(model, tempProduct));
            }
        }
        public void Delete(IceCreamBindingModel model)
        {
            // удаляем записи по компонентам при удалении изделия
            for (int i = 0; i < source.IceCreamComponents.Count; ++i)
            {
                if (source.IceCreamComponents[i].IceCreamId == model.Id)
                {
                    source.IceCreamComponents.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.IceCreams.Count; ++i)
            {
                if (source.IceCreams[i].Id == model.Id)
                {
                    source.IceCreams.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private IceCream CreateModel(IceCreamBindingModel model, IceCream iceCream)
        {
            iceCream.IceCreamName = model.IceCreamName;
            iceCream.Price = model.Price;
            //обновляем существуюущие компоненты и ищем максимальный идентификатор
            int maxPCId = 0;
            for (int i = 0; i < source.IceCreamComponents.Count; ++i)
            {
                if (source.IceCreamComponents[i].Id > maxPCId)
                {
                    maxPCId = source.IceCreamComponents[i].Id;
                }
                if (source.IceCreamComponents[i].IceCreamId == iceCream.Id)
                {
                    // если в модели пришла запись компонента с таким id
                    if
                    (model.IceCreamComponents.ContainsKey(source.IceCreamComponents[i].ComponentId))
                    {
                        // обновляем количество
                        source.IceCreamComponents[i].Count =
                        model.IceCreamComponents[source.IceCreamComponents[i].ComponentId].Item2;
                        // из модели убираем эту запись, чтобы остались только не
                        //    просмотренные

                        model.IceCreamComponents.Remove(source.IceCreamComponents[i].ComponentId);
                    }
                    else
                    {
                        source.IceCreamComponents.RemoveAt(i--);
                    }
                }
            }
            // новые записи
            foreach (var pc in model.IceCreamComponents)
            {
                source.IceCreamComponents.Add(new IceCreamComponent
                {
                    Id = ++maxPCId,
                    IceCreamId = iceCream.Id,
                    ComponentId = pc.Key,
                    Count = pc.Value.Item2
                });
            }
            return iceCream;
        }
        public List<IceCreamViewModel> Read(IceCreamBindingModel model)
        {
            List<IceCreamViewModel> result = new List<IceCreamViewModel>();
            foreach (var component in source.IceCreams)
            {
                if (model != null)
                {
                    if (component.Id == model.Id)
                    {
                        result.Add(CreateViewModel(component));
                        break;
                    }
                    continue;
                }
                result.Add(CreateViewModel(component));
            }
            return result;
        }
        private IceCreamViewModel CreateViewModel(IceCream iceCream)
        {
            // требуется дополнительно получить список компонентов для изделия с
            //  названиями и их количество
            Dictionary<int, (string, int)> iceCreamComponents = new Dictionary<int,
    (string, int)>();
            foreach (var pc in source.IceCreamComponents)
            {
                if (pc.IceCreamId == iceCream.Id)
                {
                    string componentName = string.Empty;
                    foreach (var component in source.Components)
                    {
                        if (pc.ComponentId == component.Id)
                        {
                            componentName = component.ComponentName;
                            break;
                        }
                    }
                    iceCreamComponents.Add(pc.ComponentId, (componentName, pc.Count));
                }
            }
            return new IceCreamViewModel
            {
                Id = iceCream.Id,
                IceCreamName = iceCream.IceCreamName,
                Price = iceCream.Price,
                IceCreamComponents = iceCreamComponents
            };
        }
    }
}
