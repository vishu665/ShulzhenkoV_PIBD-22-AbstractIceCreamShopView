using AbstractIceCreamShopBusinessLogic.BindingModels;
using AbstractIceCreamShopBusinessLogic.BusinessLogics;
using AbstractIceCreamShopBusinessLogic.Interfaces;
using AbstractIceCreamShopBusinessLogic.ViewModels;
using AbstractIceCreamShopRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbstractIceCreamShopRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IOrderLogic _order;
        private readonly IIceCreamLogic _icecream;
        private readonly MainLogic _main;
        public MainController(IOrderLogic order, IIceCreamLogic icecream, MainLogic main)
        {
            _order = order;
            _icecream = icecream;
            _main = main;
        }

         [HttpGet]
        public List<IceCreamModel> GetIceCreamList() => _icecream.Read(null)?.Select(rec =>
       Convert(rec)).ToList();
        [HttpGet]
        public IceCreamModel GetProduct(int icecreamID) => Convert(_icecream.Read(new
       IceCreamBindingModel
        { Id = icecreamID })?[0]);
        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new
       OrderBindingModel
        { ClientId = clientId });
        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) =>
       _main.CreateOrder(model);
        private IceCreamModel Convert(IceCreamViewModel model)
        {
            if (model == null) return null;
            return new IceCreamModel
            {
                Id = model.Id,
                IceCreamName = model.IceCreamName,
                Price = model.Price
            };
        }
    } 
}