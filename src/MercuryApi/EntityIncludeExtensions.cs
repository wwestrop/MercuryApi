using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MercuryApi
{
    public static class EntityIncludeExtensions
    {
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
            var entities = dbContext.Set<TEntity>();
            return entities;

        }
    }
}
