using NLayerCats_Mous.BLL.DataTransferObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NLayerCats_Mous.BLL.Service
{
    public interface IOrderService
    {
        Task ChecStatus(string numberOrder);
        Task<string> CreatOrder(OrderDTO orderDto);
        List<SuccessfulOrderDTO> GetSuccessfulOrders();
    }
}