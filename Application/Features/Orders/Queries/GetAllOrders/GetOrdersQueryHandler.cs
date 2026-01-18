using AutoMapper;
using EvosancomAPI.Application.DTOs.Order;
using EvosancomAPI.Application.Repositories;
using EvosancomAPI.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.Orders.Queries.GetAllOrders
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQueryRequest, GetOrdersQueryResponse>
    {
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly IMapper _mapper;

        public GetOrdersQueryHandler(IOrderReadRepository orderReadRepository, IMapper mapper)
        {
            _orderReadRepository = orderReadRepository;
            _mapper = mapper;
        }

        public async Task<GetOrdersQueryResponse> Handle(GetOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            var skip = (request.PageNumber - 1) * request.PageSize;
            var totalCount = await _orderReadRepository.GetAll(false).CountAsync(cancellationToken);

            var orders = await _orderReadRepository.GetAll(false)
                .Include(o => o.Basket)
                .ThenInclude(b => b.BasketItems)
                .OrderByDescending(o => o.OrderDate)
                .Skip(skip)
                .Take(request.PageSize)
                .Select(o => new OrderListDto
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    Status = GetOrderStatusText(o.Status),
                    TotalAmount = o.TotalAmount,
                    DiscountAmount = o.DiscountAmount,
                    FinalAmount = o.FinalAmount,
                    ShippingAddress = o.ShippingAddress,
                    EstimatedDeliveryDate = o.EstimatedDeliveryDate,
                    ActualDeliveryDate = o.ActualDeliveryDate,
                    ItemCount = o.Basket.BasketItems.Count
                })
                .ToListAsync(cancellationToken);

            return new GetOrdersQueryResponse
            {
                Orders = orders,
                TotalCount = totalCount
            };
        }

        private static string GetOrderStatusText(OrderStatus status)
        {
            return status switch
            {
                OrderStatus.Pending => "Beklemede",
                OrderStatus.Confirmed => "Onaylandı",
                OrderStatus.InProduction => "Üretimde",
                OrderStatus.QualityControl => "Kalite Kontrolde",
                OrderStatus.InWarehouse => "Depoda",
                OrderStatus.Shipped => "Kargoda",
                OrderStatus.Delivered => "Teslim Edildi",
                OrderStatus.Cancelled => "İptal Edildi",
                _ => status.ToString()
            };
        }
    }

}
