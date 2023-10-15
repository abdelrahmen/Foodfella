using Foodfella.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Foodfella.EF.Repositories
{
	public class BaseRepository<T> : IBaseRepository<T> where T : class
	{
		private readonly AppDbContext context;

		public BaseRepository(AppDbContext _context)
		{
			context = _context;
		}
		public void Add(T entity) => context.Set<T>().Add(entity);

		public async Task AddAsync(T entity) => await context.Set<T>().AddAsync(entity);

		public async Task AddRangeAsync(IEnumerable<T> entities) => await context.Set<T>().AddRangeAsync(entities);

		public IEnumerable<T> Find(Func<T, bool> predicate) => context.Set<T>().Where(predicate).ToList();

		public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) => await context.Set<T>().Where(predicate).ToListAsync();

		public IEnumerable<T> FindPaged(Func<T, bool> predicate, int page, int pageSize)
		=> context.Set<T>()
				.Where(predicate)
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToList();


		public async Task<IEnumerable<T>> FindPagedAsync(Expression<Func<T, bool>> predicate, int page, int pageSize)
		=> await context.Set<T>()
				.Where(predicate)
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();


		public IEnumerable<T> GetAll() => context.Set<T>().ToList();


		public async Task<IEnumerable<T>> GetAllAsync() => await context.Set<T>().ToListAsync();

		public IEnumerable<T> GetPaged(int page, int pageSize) => context.Set<T>()
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToList();

		public async Task<IEnumerable<T>> GetPagedAsync(int page, int pageSize) => await context.Set<T>()
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

		public T GetById(int id) => context.Set<T>().Find(id);

		public async Task<T> GetByIdAsync(int id) => await context.Set<T>().FindAsync(id);

		public void Remove(T entity) => context.Set<T>().Remove(entity);

		public void RemoveRange(IEnumerable<T> entities) => context.Set<T>().RemoveRange(entities);

		public void Update(T entity) => context.Set<T>().Update(entity);

	}
}
