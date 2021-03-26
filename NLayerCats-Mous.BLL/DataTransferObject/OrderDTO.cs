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
        public string UserName { get; set; }
        public string Password { get; set; }
        public string OrderId { get; set; }
        public string OrderNumber { get; set; }
        public int OrderStatus { get; set; } 
        public int Amount { get; set; }
        public string ReturnUrl { get; set; }
        public string FaiUrl { get; set; }
        public string Description { get; set; }

        public OrderDTO()
        {
            UserName = "client8";
            Password = GetPassword(UserName);
            
        }
        private string GetPassword(string userName)
        {
            int result = 0;
            string str = $"{userName}-spasem-mir";

            var arrayBytes = Encoding.ASCII.GetBytes(str);

            for (int i = 0; i < arrayBytes.Length; i++)
            {
                result += arrayBytes[i];
            }

            return result.ToString();
        }
       
    }
}
