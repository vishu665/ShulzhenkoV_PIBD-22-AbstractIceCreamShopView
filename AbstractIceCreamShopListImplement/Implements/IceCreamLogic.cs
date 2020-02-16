using AbstractIceCreamShopBusinessLogic.BindingModels;
using AbstractIceCreamShopBusinessLogic.Enums;
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
        public List<IceCreamViewModel> GetList()
        {
            List<IceCreamViewModel> result = new List<IceCreamViewModel>();
            for (int i = 0; i < source.IceCreams.Count; ++i)
            {// требуется дополнительно получить список компонентов для изделия и их         
                List<IceCreamComponentViewModel> iceCreamComponents = new
                List<IceCreamComponentViewModel>();
                for (int j = 0; j < source.IceCreamComponents.Count; ++j)
                {
                    if (source.IceCreamComponents[j].IceCreamId == source.IceCreams[i].Id)
                    {
                        string componentName = string.Empty;
                        for (int k = 0; k < source.Components.Count; ++k)
                        {
                            if (source.IceCreamComponents[j].ComponentId ==
                           source.Components[k].Id)
                            {
                                componentName = source.Components[k].ComponentName;
                                break;
                            }
                        }
                        iceCreamComponents.Add(new IceCreamComponentViewModel
                        {
                            Id = source.IceCreamComponents[j].Id,
                            IceCreamId = source.IceCreamComponents[j].IceCreamId,
                            ComponentId = source.IceCreamComponents[j].ComponentId,
                            ComponentName = componentName,
                            Count = source.IceCreamComponents[j].Count
                        });
                    }
                
                 }
                result.Add(new IceCreamViewModel
                {
                    Id = source.IceCreams[i].Id,
                    IceCreamName = source.IceCreams[i].IceCreamName,
                    Price = source.IceCreams[i].Price,
                    IceCreamComponents = iceCreamComponents
                });
            }
            return result;
        }
        public IceCreamViewModel GetElement(int id)
        {
            for (int i = 0; i < source.IceCreams.Count; ++i)
            {
                List<IceCreamComponentViewModel> iceCreamComponents = new
                 List<IceCreamComponentViewModel>();
                for (int j = 0; j < source.IceCreamComponents.Count; ++j)
                {
                    if (source.IceCreamComponents[j].IceCreamId == source.IceCreams[i].Id)
                    {
                        string componentName = string.Empty;
                        for (int k = 0; k < source.Components.Count; ++k)
                        {
                            if (source.IceCreamComponents[j].ComponentId == source.Components[k].Id)
                            {
                                componentName = source.Components[k].ComponentName;
                                break;
                            }
                        }
                        iceCreamComponents.Add(new IceCreamComponentViewModel
                        {
                            Id = source.IceCreamComponents[j].Id,
                            IceCreamId = source.IceCreamComponents[j].IceCreamId,
                            ComponentId = source.IceCreamComponents[j].ComponentId,
                            ComponentName = componentName,
                            Count = source.IceCreamComponents[j].Count
                        });
                    }
                }
                if (source.IceCreams[i].Id == id)
                {
                    return new IceCreamViewModel
                    {
                        Id = source.IceCreams[i].Id,
                        IceCreamName = source.IceCreams[i].IceCreamName,
                        Price = source.IceCreams[i].Price,
                        IceCreamComponents = iceCreamComponents
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(IceCreamBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.IceCreams.Count; ++i)
            {
                if (source.IceCreams[i].Id > maxId)
                {
                    maxId = source.IceCreams[i].Id;
                }
                if (source.IceCreams[i].IceCreamName == model.IceCreamName)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            source.IceCreams.Add(new IceCream
            {
                Id = maxId + 1,
                IceCreamName = model.IceCreamName,
                Price = model.Price
            });
            // компоненты для изделия
            int maxPCId = 0;
            for (int i = 0; i < source.IceCreamComponents.Count; ++i)
            {
                if (source.IceCreamComponents[i].Id > maxPCId)
                {
                    maxPCId = source.IceCreamComponents[i].Id;
                }
            }
            // убираем дубли по компонентам
            for (int i = 0; i < model.iceCreamComponents.Count; ++i)
            {
                for (int j = 1; j < model.iceCreamComponents.Count; ++j)
                {
                    if (model.iceCreamComponents[i].ComponentId ==
                    model.iceCreamComponents[j].ComponentId)
                    {
                        model.iceCreamComponents[i].Count +=
                        model.iceCreamComponents[j].Count;
                        model.iceCreamComponents.RemoveAt(j--);
                    }
                }
            }// добавляем компоненты
                for (int i = 0; i < model.iceCreamComponents.Count; ++i)
                {
                    source.IceCreamComponents.Add(new IceCreamComponent
                    {
                        Id = ++maxPCId,
                        IceCreamId = maxId + 1,
                        ComponentId = model.iceCreamComponents[i].ComponentId,
                        Count = model.iceCreamComponents[i].Count
                    });
                }
            
        }
            public void UpdElement(IceCreamBindingModel model)
            {
                int index = -1;
                for (int i = 0; i < source.IceCreams.Count; ++i)
                {
                    if (source.IceCreams[i].Id == model.Id)
                    {
                        index = i;
                    }
                    if (source.IceCreams[i].IceCreamName == model.IceCreamName &&
                    source.IceCreams[i].Id != model.Id)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                }
                if (index == -1)
                {
                    throw new Exception("Элемент не найден");
                }
                source.IceCreams[index].IceCreamName = model.IceCreamName;
                source.IceCreams[index].Price = model.Price;
                int maxPCId = 0;
                for (int i = 0; i < source.IceCreamComponents.Count; ++i)
                {
                    if (source.IceCreamComponents[i].Id > maxPCId)
                    {
                        maxPCId = source.IceCreamComponents[i].Id;
                    }
                }
                // обновляем существуюущие компоненты
                for (int i = 0; i < source.IceCreamComponents.Count; ++i)
                {
                    if (source.IceCreamComponents[i].IceCreamId == model.Id)
                    {
                        bool flag = true;
                        for (int j = 0; j < model.iceCreamComponents.Count; ++j)
                        {
                            // если встретили, то изменяем количество
                            if (source.IceCreamComponents[i].Id ==
                            model.iceCreamComponents[j].Id)
                            {
                                source.IceCreamComponents[i].Count =
                               model.iceCreamComponents[j].Count;
                                flag = false;
                                break;
                            }
                        }
                        // если не встретили, то удаляем
                        if (flag)
                        {
                            source.IceCreamComponents.RemoveAt(i--);
                        }
                    }
                }
                // новые записи
                for (int i = 0; i < model.iceCreamComponents.Count; ++i)
                {
                    if (model.iceCreamComponents[i].Id == 0)
                    {
                        // ищем дубли
                        for (int j = 0; j < source.IceCreamComponents.Count; ++j)
                        {
                            if (source.IceCreamComponents[j].IceCreamId == model.Id &&
                            source.IceCreamComponents[j].ComponentId ==
                           model.iceCreamComponents[i].ComponentId)
                            {
                                source.IceCreamComponents[j].Count +=
                               model.iceCreamComponents[i].Count;
                                model.iceCreamComponents[i].Id =
                               source.IceCreamComponents[j].Id;
                                break;
                            }
                        }
                        // если не нашли дубли, то новая запись
                        if (model.iceCreamComponents[i].Id == 0)
                        {
                            source.IceCreamComponents.Add(new IceCreamComponent
                            {
                                Id = ++maxPCId,
                                IceCreamId = model.Id,
                                ComponentId = model.iceCreamComponents[i].ComponentId,
                                Count = model.iceCreamComponents[i].Count
                            });
                        }
                    }
                }
            }
            public void DelElement(int id)
            {
                // удаляем записи по компонентам при удалении изделия
                for (int i = 0; i < source.IceCreamComponents.Count; ++i)
                {
                    if (source.IceCreamComponents[i].IceCreamId == id)
                    {
                        source.IceCreamComponents.RemoveAt(i--);
                    }
                }
                for (int i = 0; i < source.IceCreams.Count; ++i)
                {
                    if (source.IceCreams[i].Id == id)
                    {
                        source.IceCreams.RemoveAt(i);
                        return;
                    }
                }
                throw new Exception("Элемент не найден");
            }      
    } 
}
