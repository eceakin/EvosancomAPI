using EvosancomAPI.Application;
using EvosancomAPI.Application.Abstractions.Services;
using EvosancomAPI.Application.Abstractions.Services.Configurations;
using EvosancomAPI.Application.Repositories;
using EvosancomAPI.Application.Repositories.Endpoint;
using EvosancomAPI.Domain.Entities.Identity;
using EvosancomAPI.Domain.Entities.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Persistence.Services
{
	public class AuthorizationEndpointService : IAuthorizationEndpointService
	{

		readonly IApplicationService _applicationService; //endpointleri getirir
		readonly IEndpointReadRepository _endpointReadRepository;
		readonly IEndpointWriteRepository _endpointWriteRepository;
		readonly IMenuReadRepository _menuReadRepository;
		readonly IMenuWriteRepository _menuWriteRepository;
		readonly RoleManager<ApplicationRole> _roleManager;
		public AuthorizationEndpointService(IApplicationService applicationService,
			IEndpointReadRepository endpointReadRepository,
			IEndpointWriteRepository endpointWriteRepository,
			IMenuReadRepository menuReadRepository,
			IMenuWriteRepository menuWriteRepository,
			RoleManager<ApplicationRole> roleManager)
		{
			_applicationService = applicationService;
			_endpointReadRepository = endpointReadRepository;
			_menuReadRepository = menuReadRepository;
			_endpointWriteRepository = endpointWriteRepository;
			_menuWriteRepository = menuWriteRepository;
			_roleManager = roleManager;
		}


		public async Task AssingRoleEndpointAsync(string[] roles, string menu, string code, Type type)
		{

			Menu _menu =await _menuReadRepository.GetSingleAsync(m => m.Name == menu);
			if (_menu == null)
			{
				_menu = (new()
				{
					Id = Guid.NewGuid(),
					Name = menu
				});
				_menuWriteRepository.AddAsync(_menu);
				await _menuWriteRepository.SaveAsync();
			}
			Endpoint? endpoint = 
				await _endpointReadRepository.Table.Include
				(e => e.Menu).Include(e=>e.Roles).FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);
		
			if (endpoint == null)
			{
				// bu koda karşılık endpoint yok veritabanında
				var action = _applicationService.GetAuthorizeDefinitionEndpoints(type)
					.FirstOrDefault(m => m.Name == menu)?
					.Actions.FirstOrDefault(a => a.Code == code)
					;
				if (action == null)
					throw new InvalidOperationException($"No action found for menu '{menu}' and code '{code}'.");


				endpoint = new()
				{
					Id = Guid.NewGuid(),
					Code = action.Code,
					ActionType = action.ActionType,
					HttpType = action.HttpType,
					Definition = action.Definition,
					Menu = _menu
				};

				await _endpointWriteRepository.AddAsync(endpoint);
	
				await _endpointWriteRepository.SaveAsync();

			}
			foreach (var role in endpoint.Roles)
			{
				endpoint.Roles.Remove(role);
			}

			var appRoles = await _roleManager.Roles.Where(r => roles.Contains(r.Name)).ToListAsync();
			foreach(var role in appRoles) {
				endpoint.Roles.Add(role);
			}
			await _endpointWriteRepository.SaveAsync();
		}

		public async Task<List<string>> GetRolesToEndpointAsync(string code, string menu)
		{
			// id ye göre endpoint i ve rollerini döner
			Endpoint? endpoint = await _endpointReadRepository.Table.Include(e => e.Roles)
				.Include(e => e.Menu).
				FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);
			if (endpoint != null)
			{
				return endpoint.Roles.Select(r => r.Name).ToList();
			}
			return null;
		}
	}
}
