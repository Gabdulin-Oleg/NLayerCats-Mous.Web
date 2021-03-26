using System;
using System.Collections.Generic;
using System.Text;

namespace NLayerCats_Mous.BLL.DataTransferObject
{
     class IdOrder
    {
        // Форма на получение ID  заказа в системе оплаты
        public string orderId { get; set; }
        public string formUrl { get; set; }
        public int errorCode { get; set; }
        public string errorMessage { get; set; }

    }
}
