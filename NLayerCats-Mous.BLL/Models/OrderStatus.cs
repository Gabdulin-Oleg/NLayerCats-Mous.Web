namespace NLayerCats_Mous.BLL.DataTransferObject
{
    //Фома на получение статуса заказа в системе оплаты
    public class OrderStatus
    {
        public int orderStatus { get; set; }
        public int errorCode { get; set; }
        public string erroeMessage { get; set; }
        public int amount { get; set; }
        public string orderNumber { get; set; }

    }
}
