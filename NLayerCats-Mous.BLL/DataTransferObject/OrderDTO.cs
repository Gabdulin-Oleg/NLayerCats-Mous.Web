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
        public string Description { get; set; } 
    }
}
