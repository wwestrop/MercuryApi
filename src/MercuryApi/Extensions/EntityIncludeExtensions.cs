using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MercuryApi.Extensions
{
    public static class EntityIncludeExtensions
    {
        // TODO how am I going to get this accessor inserted into a static, yet used per-request ?
        // Maybe not an extension method on DbContext, but an instance of MySpecialDbContext, derived from DbContext, with the method on it??????
        internal static IHttpContextAccessor _httpContextAccessor;
        internal static IQueryParser _parser;
        internal static IExpressionApplicator _applicator;
        internal static INavigationPathBuilder _pathBuilder;

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
        public static IQueryable<TEntity> IncludeFromHttpQuery<TEntity>(DbContext dbContext) where TEntity : class {

            IQueryable<TEntity> entities = dbContext.Set<TEntity>();

            var rawFilterRequest = _httpContextAccessor.HttpContext.Request.Query;
            var parsedFilterRequest = _parser.Parse(rawFilterRequest);

            // optimise here (RemoveBackfills) - currently done within parser

            foreach(var includePath in parsedFilterRequest) {
                var efIncludeString = _pathBuilder.Build(typeof(TEntity), includePath);
                entities = _applicator.Include(entities, efIncludeString);
            }
            
            return entities;
        }
    }
}
