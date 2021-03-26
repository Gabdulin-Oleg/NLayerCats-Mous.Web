using System;
using System.Collections.Generic;
using System.Text;

namespace NLayer_Cats_Mous.DAL.Entitis
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string OrderId { get; set; }
        public int OrderStatus { get; set; }
        public int  Amount { get; set; }
        public string Description { get; set; }
    }
}
