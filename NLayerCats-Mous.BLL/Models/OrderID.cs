namespace NLayerCats_Mous.BLL.DataTransferObject
{
    // Форма на получение ID  заказа в системе оплаты
    public class OrderID
    {
        public string orderId { get; set; }
        public string formUrl { get; set; }
        public int errorCode { get; set; }
        public string errorMessage { get; set; }

    }
}
