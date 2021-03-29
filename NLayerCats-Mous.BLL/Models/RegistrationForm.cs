namespace NLayerCats_Mous.BLL.Models
{
    // Форма для регистрации  Заказа в системе оплаты
    public class RegistrationForm
    {
        public string UserName { get; set; }
        public string Password { get; set; }       
        public string OrderNumber { get; set; }
        public int Amount { get; set; }
        public string ReturnUrl { get; set; }
        public string FaiUrl { get; set; }
        public string Description { get; set; }

    }
}
