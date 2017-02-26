using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace MercuryApi
{
	/// <summary>
	/// Behaves the same as a standard EntityFramework <c>DbContext</c>, but includes an additional method,
	/// <c>IncludedSet&lt;TEntity&gt;()</c> - this is what you use to retrieve an <c>IQueryable</c> with
	/// the related entities specified on the HTTP request's <c>include</c> parameter already included. 
	/// </summary>
	public abstract class MercuryDbContext : DbContext {

		private readonly IHttpContextAccessor _httpContextAccess;
		private readonly IQueryParser _httpQueryParser;
		private readonly INavigationPathBuilder _navPathBuilder;
		private readonly IExpressionApplicator _expressionApplicator;

		internal MercuryDbContext(DbContextOptions options, 
			IHttpContextAccessor httpContextAccess, 
			IQueryParser httpQueryParser, 
			INavigationPathBuilder navPathBuilder, 
			IExpressionApplicator expressionApplicator)
		: base(options) {

			_httpContextAccess = httpContextAccess;
			if (httpContextAccess == null) {
				throw new ArgumentNullException(nameof(httpContextAccess));
			}

			_httpQueryParser = httpQueryParser;
			if (httpQueryParser == null) {
				throw new ArgumentNullException(nameof(httpQueryParser));
			}

			_navPathBuilder = navPathBuilder;
			if (navPathBuilder == null) {
				throw new ArgumentNullException(nameof(navPathBuilder));
			}

			_expressionApplicator = expressionApplicator;
			if (expressionApplicator == null) {
				throw new ArgumentNullException(nameof(expressionApplicator));
			}	
		}

		public MercuryDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccess) 
			: this(options, httpContextAccess, new QueryParser(), new NavigationPathBuilder(), new ExpressionApplicator()) {		  
		}

		/// <summary>
		/// Automatically applies the include statements specified on the HTTP request 
		/// to the underlying Entity Framework IQueryable to load up as much as the 
		/// client has requested via the url <c>?include=</c> queryString. 
		/// </summary>
		/// <remarks>
		/// If you need to do further server-side processing based upon related entities
		/// which you do not intend to return to the client, you can chain a call to
		/// <c>.Include()</c> to make sure these are fetched back from the database also
		/// </remarks>
		public IQueryable<TEntity> IncludedSet<TEntity>() where TEntity : class {

			var rawDbSet = this.Set<TEntity>();
			IQueryable<TEntity> result = rawDbSet;

			// TODO optimise here in a separate step maybe?? (RemoveBackfills) - currently done within parser
			var requestedIncludeQuery = _httpContextAccess.HttpContext.Request.Query;
			var parsedIncludes = _httpQueryParser.Parse(requestedIncludeQuery);

			foreach (var requestedInclude in parsedIncludes) {
				string navPath = _navPathBuilder.Build(typeof(TEntity), requestedInclude);
				result = _expressionApplicator.Include(result, navPath);
			}

			return result;
		}

	}
}
