using NLayerCats_Mous.BLL.DataTransferObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NLayerCats_Mous.BLL.Interface

{
    public interface IOrderService
    {
        Task ChecStatus(string numberOrder);
        Task<string> GetInformationFromSever(OrderDTO order, string urlAddress);
        List<SuccessfulOrderDTO> GetSuccessfulOrders();
        Task<string> RegisteredOrder(OrderDTO orderDto);
    }
}