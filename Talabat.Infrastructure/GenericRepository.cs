﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Infrastructure.Data;

namespace Talabat.Infrastructure
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly ApplicationDbContext _dbContext;

		public GenericRepository(ApplicationDbContext dbContext)
        {
			_dbContext = dbContext;
		}
        public async Task<IEnumerable<T>> GetAllAsync()
		{
			if (typeof(T) == typeof(Product))
				return (IEnumerable<T>) await  _dbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).ToListAsync();


			return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
		}

		public async Task<T?> GetAsync(int id)
		{
			if(typeof(T) == typeof(Product))
				return await _dbContext.Set<Product>().Where(P=> P.Id == id).Include(P => P.Brand).Include(P => P.Category).FirstOrDefaultAsync() as T ;


			return await _dbContext.Set<T>().FindAsync(id);
		}
	}
}
