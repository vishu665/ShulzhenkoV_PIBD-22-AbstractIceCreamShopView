using System;
using System.Collections.Generic;
using System.Text;
using AbstractIceCreamShopBusinessLogic.BindingModels;
using AbstractIceCreamShopBusinessLogic.ViewModels;


namespace AbstractIceCreamShopBusinessLogic.Interfaces
{
   public interface IIceCreamLogic
    {
        List<IceCreamViewModel> Read(IceCreamBindingModel model);
        void CreateOrUpdate(IceCreamBindingModel model);
        void Delete(IceCreamBindingModel model);

    }
}
