using EvosancomAPI.Application.DTOs.Order;

namespace EvosancomAPI.Application.Features.Orders.Queries.GetAllOrders
{
    public class GetOrdersQueryResponse
    {

        public List<OrderListDto> Orders { get; set; }
        public int TotalCount { get; set; }
    }
}
