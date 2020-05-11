using AbstractIceCreamShopDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace AbstractIceCreamShopDatabaseImplement
{
    public class AbstractIceCreamShopDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source = WIN-UNRO2TSBIL;Initial Catalog = AbstractIceCreamShopDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Component> Components { set; get; }
        public virtual DbSet<IceCream> IceCreams { set; get; }
        public virtual DbSet<IceCreamComponent> IceCreamComponents { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
    }
}
