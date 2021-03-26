using Microsoft.EntityFrameworkCore;
using NLayer_Cats_Mous.DAL.Entitis;
using System;
using System.Collections.Generic;
using System.Text;

namespace NLayer_Cats_Mous.DAL.DBContext
{
    class Context : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;DataBase=NLayerDBCats-Mous;Trusted_Connection=True;");
        }
    }
}
