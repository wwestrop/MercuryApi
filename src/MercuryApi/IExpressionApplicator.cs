﻿using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MercuryApi
{
	/// <summary>
	/// Responsible for passing down the required Include paths to Entity Framework to apply them to the DbContext
	/// </summary>
	internal interface IExpressionApplicator
	{
		IQueryable<TEntity> Include<TEntity>(IQueryable<TEntity> source, string navigationPropertyPath) where TEntity : class;
	}

	internal class ExpressionApplicator : IExpressionApplicator {
		public IQueryable<TEntity> Include<TEntity>(IQueryable<TEntity> source, string navigationPropertyPath) where TEntity : class {
			return source.Include(navigationPropertyPath);
		}
	}
}
