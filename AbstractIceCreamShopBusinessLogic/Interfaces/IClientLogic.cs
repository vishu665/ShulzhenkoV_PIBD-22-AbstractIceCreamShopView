using AbstractIceCreamShopBusinessLogic.BindingModels;
using AbstractIceCreamShopBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractIceCreamShopBusinessLogic.Interfaces
{
    public interface IClientLogic
    {
        List<ClientViewModel> Read(ClientBindingModel model);

        void CreateOrUpdate(ClientBindingModel model);

        void Delete(ClientBindingModel model);
    }
}
