namespace NLayerCats_Mous.BLL.Models
{
    // Форма для отправки в систему оплаты на получение статуса заказа
    public class GetStatusForm
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string orderId { get; set; }

    }
}
