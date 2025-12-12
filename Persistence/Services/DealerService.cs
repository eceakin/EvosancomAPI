using EvosancomAPI.Application.Abstractions.Services;
using EvosancomAPI.Application.DTOs.Dealer;
using EvosancomAPI.Application.Repositories.Dealer;
using EvosancomAPI.Application;
using EvosancomAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EvosancomAPI.Application.Repositories;
using Microsoft.AspNetCore.Identity;
using EvosancomAPI.Domain.Entities.Identity;

namespace EvosancomAPI.Persistence.Services
{
	public class DealerService : IDealerService
	{
		private readonly IDealerWriteRepository _dealerWriteRepository;
		private readonly IDealerReadRepository _dealerReadRepository;
		private readonly IProductReadRepository _productReadRepository;
		private readonly UserManager<ApplicationUser> _userManager;
		public DealerService(IDealerWriteRepository dealerWriteRepository, IDealerReadRepository dealerReadRepository, IProductReadRepository productReadRepository, UserManager<ApplicationUser> userManager)
		{
			_dealerWriteRepository = dealerWriteRepository;
			_dealerReadRepository = dealerReadRepository;
			_productReadRepository = productReadRepository;
			_userManager = userManager;
		}

		public async Task CreateDealerAsync(CreateDealerDto createDealerDto)
		{
			// İleride buraya "AppUser var mı?" kontrolü veya "Kullanıcıya Role Ata" işlemi eklenebilir.
			await _dealerWriteRepository.AddAsync(new Dealer
			{
				Id = Guid.NewGuid(),
				UserId = createDealerDto.UserId,
				DiscountRate = createDealerDto.DiscountRate,
				SalesQuota = createDealerDto.SalesQuota,
				CompanyName = createDealerDto.CompanyName,
				CurrentPeriodSales = 0
			});
			await _dealerWriteRepository.SaveAsync();
		}

		public async Task UpdateDealerAsync(UpdateDealerDto updateDealerDto)
		{
			var dealer = await _dealerReadRepository.GetByIdAsync(updateDealerDto.Id);
			if (dealer != null)
			{
				dealer.DiscountRate = updateDealerDto.DiscountRate;
				dealer.SalesQuota = updateDealerDto.SalesQuota;
				dealer.CompanyName = updateDealerDto.CompanyName;
				// UpdateAsync generic repository'de bool döner, ef core tracking sayesinde SaveAsync yeterli olabilir ama senin yapında UpdateAsync çağırıyoruz.
				_dealerWriteRepository.UpdateAsync(dealer);
				await _dealerWriteRepository.SaveAsync();
			}
			else
			{
				throw new Exception("Bayi bulunamadı.");
			}
		}

		public async Task DeleteDealerAsync(string id)
		{
			await _dealerWriteRepository.RemoveAsync(id);
			await _dealerWriteRepository.SaveAsync();
		}

		public async Task<List<DealerListDto>> GetAllDealersAsync()
		{
			var dealers = _dealerReadRepository.GetAll(tracking: false);
			return await dealers.Select(d => new DealerListDto
			{
				Id = d.Id.ToString(),
				UserId = d.UserId,
				CompanyName = d.CompanyName,
				DiscountRate = d.DiscountRate,
				SalesQuota = d.SalesQuota,
				CreatedDate = d.CreatedDate
			}).ToListAsync();
		}

		public async Task<List<DealerProductListDto>> GetProductsForDealerAsync(string identifier)
		{
			// 1. Gelen değer "dealeruser" (username) ise önce ID'sini bulalım.
			string realUserId = identifier;

			if (!Guid.TryParse(identifier, out Guid _))
			{
				// Değer GUID değil, demek ki Username geldi.
				var user = await _userManager.FindByNameAsync(identifier);
				if (user == null)
					throw new Exception("Kullanıcı bulunamadı!");

				realUserId = user.Id.ToString();
			}

			// 2. Artık elimizde kesinlikle ID var. Sorguyu yapalım.
			var dealer = await _dealerReadRepository.GetSingleAsync(d => d.UserId == realUserId);

			if (dealer == null)
				throw new Exception("Bu kullanıcıya tanımlı bir Bayi kaydı bulunamadı.");

			// 3. Ürünleri ve iskontoyu hazırla
			var products = _productReadRepository.GetAll(tracking: false);

			var dealerProducts = await products.Select(p => new DealerProductListDto
			{
				Id = p.Id.ToString(),
				Name = p.Name,
				StockQuantity = p.StockQuantity,
				BasePrice = p.BasePrice,
				AppliedDiscountRate = dealer.DiscountRate,
				DiscountedPrice = p.BasePrice - (p.BasePrice * dealer.DiscountRate)
			}).ToListAsync();

			return dealerProducts;
		}
	}

}
