using EvosancomAPI.Application.Abstractions.Services;
using EvosancomAPI.Application.Repositories.Dealer;
using EvosancomAPI.Application.Repositories;
using EvosancomAPI.Domain.Entities;
using EvosancomAPI.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace EvosancomAPI.Application.Features.DealerOrders.Commands.CreateDealerOrder
{
	public class CreateDealerOrderCommandHandler : IRequestHandler<CreateDealerOrderCommandRequest, CreateDealerOrderCommandResponse>
	{
		private readonly IOrderWriteRepository _orderWriteRepository;
		private readonly IOrderItemWriteRepository _orderItemWriteRepository;
		private readonly IDealerReadRepository _dealerReadRepository;
		private readonly IProductReadRepository _productReadRepository;
		private readonly IPriceCalculationService _priceCalculationService;
		private readonly ILogger<CreateDealerOrderCommandHandler> _logger;

		public CreateDealerOrderCommandHandler(
			IOrderWriteRepository orderWriteRepository,
			IOrderItemWriteRepository orderItemWriteRepository,
			IDealerReadRepository dealerReadRepository,
			IProductReadRepository productReadRepository,
			IPriceCalculationService priceCalculationService,
			ILogger<CreateDealerOrderCommandHandler> logger)
		{
			_orderWriteRepository = orderWriteRepository;
			_orderItemWriteRepository = orderItemWriteRepository;
			_dealerReadRepository = dealerReadRepository;
			_productReadRepository = productReadRepository;
			_priceCalculationService = priceCalculationService;
			_logger = logger;
		}

		public async Task<CreateDealerOrderCommandResponse> Handle(
			CreateDealerOrderCommandRequest request,
			CancellationToken cancellationToken)
		{
			try
			{
				_logger.LogInformation("Creating order for dealer: {UserId}", request.UserId);

				// 1. Dealer kontrolü
				var dealer = await _dealerReadRepository.GetAll(false)
					.FirstOrDefaultAsync(d => d.UserId == request.UserId && !d.IsDeleted, cancellationToken);

				if (dealer == null)
				{
					return new CreateDealerOrderCommandResponse
					{
						Success = false,
						Message = "Bayi bulunamadı."
					};
				}

				if (!dealer.IsActive)
				{
					return new CreateDealerOrderCommandResponse
					{
						Success = false,
						Message = "Bayi hesabınız aktif değil. Lütfen yönetici ile iletişime geçin."
					};
				}

				// 2. Sipariş numarası oluştur
				var orderNumber = await GenerateOrderNumberAsync();

				// 3. Order entity oluştur
				var order = new Order
				{
					OrderNumber = orderNumber,
					UserId = request.UserId,
					OrderDate = DateTime.UtcNow,
					Status = OrderStatus.Pending,
					ShippingAddress = request.ShippingAddress,
					BillingAddress = request.BillingAddress ?? request.ShippingAddress,
					Notes = request.Notes,
					CreatedDate = DateTime.UtcNow
				};

				decimal totalAmount = 0;
				decimal totalDiscount = 0;

				// 4. OrderItem'ları oluştur
				var orderItems = new List<OrderItem>();
				foreach (var itemDto in request.OrderItems)
				{
					var product = await _productReadRepository.GetByIdAsync(itemDto.ProductId.ToString(), false);
					if (product == null)
					{
						return new CreateDealerOrderCommandResponse
						{
							Success = false,
							Message = $"Ürün bulunamadı: {itemDto.ProductId}"
						};
					}

					// Fiyat hesaplama
					var priceCalculation = await _priceCalculationService.CalculatePriceAsync(
						product.Id,
						dealer.Id,
						itemDto.HasCustomDimensions ? itemDto.CustomDimension : null
					);

					var unitPrice = priceCalculation.TotalPrice;
					var discountedPrice = priceCalculation.FinalPrice;
					var itemTotal = discountedPrice * itemDto.Quantity;

					var orderItem = new OrderItem
					{
						OrderId = order.Id,
						ProductId = product.Id,
						Quantity = itemDto.Quantity,
						UnitPrice = unitPrice,
						TotalPrice = itemTotal,
						HasCustomDimensions = itemDto.HasCustomDimensions,
						CreatedDate = DateTime.UtcNow
					};

					// Custom dimension varsa ekle
					if (itemDto.HasCustomDimensions && itemDto.CustomDimension != null)
					{
						orderItem.CustomDimension = new CustomDimension
						{
							OrderItemId = orderItem.Id,
							Width = itemDto.CustomDimension.Width,
							Height = itemDto.CustomDimension.Height,
							Depth = itemDto.CustomDimension.Depth,
							AdditionalCost = priceCalculation.CustomDimensionCost,
							CreatedDate = DateTime.UtcNow
						};
					}

					orderItems.Add(orderItem);
					totalAmount += unitPrice * itemDto.Quantity;
					totalDiscount += (unitPrice - discountedPrice) * itemDto.Quantity;
				}

				// 5. Order tutarlarını hesapla
				order.TotalAmount = totalAmount;
				order.DiscountAmount = totalDiscount;
				order.FinalAmount = totalAmount - totalDiscount;

				// 6. Tahmini teslimat tarihi hesapla
				var hasCustomDimension = orderItems.Any(oi => oi.HasCustomDimensions);
				var estimatedDays = hasCustomDimension ? 30 : 20; // Özel ölçü varsa daha uzun süre
				order.EstimatedDeliveryDate = DateTime.UtcNow.AddDays(estimatedDays);

				// 7. Veritabanına kaydet
				await _orderWriteRepository.AddAsync(order);
				await _orderWriteRepository.SaveAsync();

				foreach (var orderItem in orderItems)
				{
					orderItem.OrderId = order.Id;
					await _orderItemWriteRepository.AddAsync(orderItem);
				}
				await _orderItemWriteRepository.SaveAsync();

				_logger.LogInformation("Order created successfully. OrderId: {OrderId}, OrderNumber: {OrderNumber}",
					order.Id, order.OrderNumber);

				return new CreateDealerOrderCommandResponse
				{
					Success = true,
					Message = "Sipariş başarıyla oluşturuldu.",
					OrderId = order.Id.ToString(),
					OrderNumber = order.OrderNumber,
					TotalAmount = order.TotalAmount,
					DiscountAmount = order.DiscountAmount,
					FinalAmount = order.FinalAmount,
					EstimatedDeliveryDate = order.EstimatedDeliveryDate.Value
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while creating dealer order");
				return new CreateDealerOrderCommandResponse
				{
					Success = false,
					Message = "Sipariş oluşturulurken bir hata oluştu: " + ex.Message
				};
			}
		}

		private async Task<string> GenerateOrderNumberAsync()
		{
			var now = DateTime.UtcNow;
			var prefix = $"ORD-{now:yyyyMMdd}";

			// Bugünkü son sipariş numarasını bul
			var lastOrder = await _orderWriteRepository.Table
				.Where(o => o.OrderNumber.StartsWith(prefix))
				.OrderByDescending(o => o.OrderNumber)
				.FirstOrDefaultAsync();

			int sequence = 1;
			if (lastOrder != null)
			{
				var lastSequence = lastOrder.OrderNumber.Split('-').Last();
				if (int.TryParse(lastSequence, out int lastSeq))
				{
					sequence = lastSeq + 1;
				}
			}

			return $"{prefix}-{sequence:D4}";
		}
	}
}
