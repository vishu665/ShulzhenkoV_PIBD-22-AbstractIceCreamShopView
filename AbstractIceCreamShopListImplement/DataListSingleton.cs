using AbstractIceCreamShopListImplement.Models;
using System;
using System.Collections.Generic;

namespace AbstractIceCreamShopListImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Component> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<IceCream> IceCreams { get; set; }
        public List<IceCreamComponent> IceCreamComponents { get; set; }
        private DataListSingleton()
        {
            Components = new List<Component>();
            Orders = new List<Order>();
            IceCreams = new List<IceCream>();
            IceCreamComponents = new List<IceCreamComponent>();
        }
        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }

    }
}
