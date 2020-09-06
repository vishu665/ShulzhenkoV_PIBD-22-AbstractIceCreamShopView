﻿using AbstractIceCreamShopBusinessLogic.BindingModels;
using AbstractIceCreamShopBusinessLogic.Interfaces;
using AbstractIceCreamShopBusinessLogic.ViewModels;
using AbstractIceCreamShopFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractIceCreamShopFileImplement.Implements
{
    public class ClientLogic : IClientLogic
    {
        private readonly FileDataListSingleton source;

        public ClientLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public void CreateOrUpdate(ClientBindingModel model)
        {
            Client element = source.Clients.FirstOrDefault(rec => rec.Email == model.Email && rec.Id != model.Id);

            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }

            if (model.Id.HasValue)
            {
                element = source.Clients.FirstOrDefault(rec => rec.Id == model.Id);

                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
            }
            else
            {
                int maxId = source.Clients.Count > 0 ? source.Clients.Max(rec => rec.Id) : 0;
                element = new Client { Id = maxId + 1 };
                source.Clients.Add(element);
            }

            element.Email = model.Email;
            element.FIO = model.ClientFIO;
            element.Password = model.Password;
        }

        public void Delete(ClientBindingModel model)
        {
            Client element = source.Clients.FirstOrDefault(rec => rec.Id == model.Id);

            if (element != null)
            {
                source.Clients.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public List<ClientViewModel> Read(ClientBindingModel model)
        {
            return source.Clients
            .Where(
                rec => model == null
                || rec.Id == model.Id
                || rec.Email == model.Email && rec.Password == model.Password
            )
            .Select(rec => new ClientViewModel
            {
                Id = rec.Id,
                ClientFIO = rec.FIO,
                Email = rec.Email,
                Password = rec.Password
            })
            .ToList();
        }
    }
}
