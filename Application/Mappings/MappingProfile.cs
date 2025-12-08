using AutoMapper;
using EvosancomAPI.Application.Features.Products.DTOs;
using EvosancomAPI.Domain.Entities.Identity;
using EvosancomAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvosancomAPI.Domain.Enums;

namespace EvosancomAPI.Application.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			// ============================================
			// USER MAPPINGS
			// ============================================
			/*
						CreateMap<ApplicationUser, UserDto>()
							.ForMember(dest => dest.Roles, opt => opt.Ignore());

						CreateMap<RegisterDto, ApplicationUser>()
							.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
							.ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => true))
							.ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
							.ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
			*/
			// ============================================
			// PRODUCT MAPPINGS
			// ============================================

			CreateMap<Product, ProductDto>()
				.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.ProductCategory.Name));

			CreateMap<Product, ProductListDto>()
				.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.ProductCategory.Name));

			CreateMap<ProductCategory, ProductCategoryDto>()
				.ForMember(dest => dest.ProductCount, opt => opt.MapFrom(src => src.Products.Count(p => !p.IsDeleted)));

			// ============================================
			// ORDER MAPPINGS
			// ============================================
			/*
			CreateMap<Order, OrderDto>()
				.ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
				.ForMember(dest => dest.StatusText, opt => opt.MapFrom(src => GetOrderStatusText(src.Status)))
				.ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

			CreateMap<Order, OrderListDto>()
				.ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
				.ForMember(dest => dest.StatusText, opt => opt.MapFrom(src => GetOrderStatusText(src.Status)))
				.ForMember(dest => dest.ItemCount, opt => opt.MapFrom(src => src.OrderItems.Count));

			CreateMap<OrderItem, OrderItemDto>()
				.ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
				.ForMember(dest => dest.CustomDimension, opt => opt.MapFrom(src => src.CustomDimension));

			CreateMap<CustomDimension, CustomDimensionDto>();

			CreateMap<CreateOrderItemDto, OrderItem>()
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.OrderId, opt => opt.Ignore())
				.ForMember(dest => dest.UnitPrice, opt => opt.Ignore())
				.ForMember(dest => dest.TotalPrice, opt => opt.Ignore());

			CreateMap<CustomDimensionDto, CustomDimension>()
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.OrderItemId, opt => opt.Ignore());
		}
		*/
			
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
	} }
