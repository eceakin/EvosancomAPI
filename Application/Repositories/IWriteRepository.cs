using EvosancomAPI.Domain.Entities.Common;

namespace EvosancomAPI.Application.Repositories
{
	public interface IWriteRepository<T> : IRepository<T> where T :BaseEntity
	{
		Task<bool> AddAsync(T entity);
		Task<bool> AddRangeAsync(List<T> datas);

		Task<bool> RemoveAsync(string id);
		bool Remove(T model);
		bool RemoveRange(List<T> datas);
		bool UpdateAsync(T model);

		Task<int> SaveAsync();
	}
}

