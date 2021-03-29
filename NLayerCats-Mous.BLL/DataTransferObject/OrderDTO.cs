using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace NLayerCats_Mous.BLL.DataTransferObject
{
    public class OrderDTO
    {
        // Модель для взаимодействия с платежной системой 
        public int Id { get; set; }       
        public string OrderId { get; set; }
        public string OrderNumber { get; set; }
        public int OrderStatus { get; set; } 
        public int Amount { get; set; }
        public string ReturnUrl { get; set; }
        public string FaiUrl { get; set; }
        public string Description { get; set; }
       
       
    }
}
