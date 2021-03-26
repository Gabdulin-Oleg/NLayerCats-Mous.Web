using NLayer_Cats_Mous.DAL.DBContext;
using NLayer_Cats_Mous.DAL.Entitis;
using NLayer_Cats_Mous.DAL.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLayer_Cats_Mous.DAL.Repository
{
    public class OrderRepository : IRepository
    {

        public void Creat(Order order)
        {
            using (Context db = new Context())
            {
                db.Orders.Add(order);
                db.SaveChanges();
            }
        }

        public void Delete(Order order)
        {
            using (Context db = new Context())
            {
                db.Orders.Remove(order);
                db.SaveChanges();
            }
        }

        public List<Order> GetAllSuccessfulOrder()
        {
            using (Context db = new Context())
            {
                return db.Orders.Select(O => O).Where(O => O.OrderStatus == 2).ToList();
            }
        }

        public Order Find(string numberOrder)
        {
            using (Context db = new Context())
            {
                return db.Orders.Select(O => O).Where(O => O.OrderNumber == numberOrder).Single();
            }
        }

        public void UpData(Order order)
        {
            using (Context db = new Context())
            {
                db.Orders.Update(order);
                db.SaveChanges();
            }
        }

       
    }

}
