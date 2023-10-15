using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Foodfella.Core.Interfaces
{
	public interface IBaseRepository<T> where T : class
	{
		// Create
		void Add(T entity);
		Task AddAsync(T entity);
		Task AddRangeAsync(IEnumerable<T> entities);

		// Read
		T GetById(int id);
		Task<T> GetByIdAsync(int id);
		IEnumerable<T> Find(Func<T, bool> predicate);
		Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
		IEnumerable<T> GetAll();
		IEnumerable<T> GetPaged(int page, int pageSize);
		Task<IEnumerable<T>> GetAllAsync();

		// Update
		void Update(T entity);

		// Delete
		void Remove(T entity);
		void RemoveRange(IEnumerable<T> entities);
	}
}
