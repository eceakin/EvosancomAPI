using EvosancomAPI.Application.Abstractions.Services;
using EvosancomAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Persistence.Services
{
	public class RoleService : IRoleService
	{
		readonly RoleManager<ApplicationRole> _roleManager;

		public RoleService(RoleManager<ApplicationRole> roleManeger)
		{
			_roleManager = roleManeger;
		}

		public async Task<bool> CreateRole(string name)
		{
			IdentityResult result = await _roleManager.CreateAsync(new()
			{
				Id = Guid.NewGuid().ToString(),
				Name = name
			});
			
			return result.Succeeded;
		}

		public async Task<bool> DeleteRole(string id)
		{
			IdentityResult result=await
				_roleManager.DeleteAsync(new ApplicationRole { Id = id });
			return result.Succeeded;
			}

		public object GetAllRoles()
		{
			return _roleManager.Roles.Select(r=> new { r.Id, r.Name }).ToList();
		}

		public async Task<(string id, string name)> GetRoleById(string id)
		{
			string role = await _roleManager.GetRoleIdAsync(new ApplicationRole { Id = id });
			return (id, role);
		}

		public async Task<bool> UpdateRole(string id, string name)
		{
			ApplicationRole role = await _roleManager.FindByIdAsync(id);
			if (role == null)
				return false;

			role.Name = name;                       // *** ASIL EKSİK OLAN KISIM ***
			role.NormalizedName = name.ToUpper();   // Identity için gerekli!
			IdentityResult result = await _roleManager.UpdateAsync(role);

			return result.Succeeded;
		}
	}
}
