using AbstractIceCreamShopBusinessLogic.Interfaces;
using AbstractIceCreamShopBusinessLogic.BusinessLogics;
using AbstractIceCreamShopDatabaseImplement.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace AbstractIceCreamShopView
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IComponentLogic, ComponentLogic>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOrderLogic, OrderLogic>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IIceCreamLogic, IceCreamLogic>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<MainLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ReportLogic>(new
HierarchicalLifetimeManager());
            currentContainer.RegisterType<IClientLogic, ClientLogic>(new HierarchicalLifetimeManager());


            return currentContainer;
        }
    }
}
