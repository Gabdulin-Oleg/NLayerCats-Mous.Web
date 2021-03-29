using NLayer_Cats_Mous.DAL.Entitis;
using System.Collections.Generic;

namespace NLayer_Cats_Mous.DAL.Intefaces
{
    public interface IRepository
    {
        public List<Order> GetAllSuccessfulOrder();
        public void Creat(Order order);
        public Order Find(string numberOrder);
        public void UpDate(Order order);
        public void Delete(Order order);
        
    }
}
