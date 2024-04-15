using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Infrastructure
{
	internal static class SpecificationsEvaluator<TEntity> where TEntity : BaseEntity
	{
		public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery /*DbSet*/, ISpecifications<TEntity> spec)
		{
			var query = inputQuery; // _dbContext.Set<Product>()

			if (spec.Criteria is not null)
				query = query.Where(spec.Criteria); // P => P.Id == 1

			// query = _dbContext.Set<Product>().Where(P => P.Id == 1)
			// Includes
			// 1. P => P.Brand
			// 2. P => P.Category


			query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

			// _dbContext.Set<Product>().Where(P => P.Id == 1).Include(P => P.Brand).Include(P => P.Category)


			return query;
		}
	}
}
