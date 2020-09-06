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
        public List<Client> Clients { get; set; }
        public List<Implementer> Implementers { get; set; }
        public List<MessageInfo> MessageInfoes { get; set; }

        private DataListSingleton()
        {
            Components = new List<Component>();
            Orders = new List<Order>();
            IceCreams = new List<IceCream>();
            IceCreamComponents = new List<IceCreamComponent>();
            Clients = new List<Client>();
            Implementers = new List<Implementer>();
            MessageInfoes = new List<MessageInfo>();

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
