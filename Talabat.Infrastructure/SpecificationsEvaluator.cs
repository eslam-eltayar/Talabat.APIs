﻿using Microsoft.EntityFrameworkCore;
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

			if (spec.OrderBy is not null)
				query = query.OrderBy(spec.OrderBy);

			else if (spec.OrderByDesc is not null)
				query = query.OrderByDescending(spec.OrderByDesc);


			if (spec.IsPaginationEnabled)
				query = query.Skip(spec.Skip).Take(spec.Take);

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
