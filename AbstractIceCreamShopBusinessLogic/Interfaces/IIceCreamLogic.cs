using System;
using System.Collections.Generic;
using System.Text;
using AbstractIceCreamShopBusinessLogic.BindingModels;
using AbstractIceCreamShopBusinessLogic.ViewModels;


namespace AbstractIceCreamShopBusinessLogic.Interfaces
{
   public interface IIceCreamLogic
    {
        List<IceCreamViewModel> GetList();
        IceCreamViewModel GetElement(int id);
        void AddElement(IceCreamBindingModel model);
        void UpdElement(IceCreamBindingModel model);
        void DelElement(int id);

    }
}
