
using System;
using System.Collections.Generic;
using System.Text;

namespace NLayerCats_Mous.BLL.DataTransferObject
{
    class StatusOrder
    {
        //Фома на получение статуса заказа в системе оплаты
        public int orderStatus { get; set; }
        public int errorCode { get; set; }
        public string erroeMessage { get; set; }
        public int amount { get; set; }
        public string orderNumber { get; set; }

    }
}
