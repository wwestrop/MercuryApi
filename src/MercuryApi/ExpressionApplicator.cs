using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MercuryApi.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Net;
using System.Reflection;
using System.Collections;

namespace MercuryApi
{
    public class ExpressionApplicator
    {
        //public void Build2(string includeExpression) {
        //}

        public void Build(string includeExpression) {

            if (string.IsNullOrWhiteSpace(includeExpression)) {
                return;
            }

            var allIncludePaths = WebUtility.UrlDecode(includeExpression)
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim().ToLower())
                .Distinct()
                .OrderByDescending(s => s.Length)
                .ToArray();

            allIncludePaths = RemoveBackfills(allIncludePaths);

            foreach(var path in allIncludePaths) {
                var splitByDot = path.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

                //var efNavigationPath = BuildNavigationPath(splitByDot);

                //var head = splitByDot.First();
                //var tail = splitByDot.Skip(1).ToArray();

                
                //var p = Expression.Parameter(typeof(IQueryable<>));
                //var pA = Expression.Property(p, "propertyName");

                //var l = Expression.Lambda(pA);

                // IQueryable<TEntity> source = null;
                // source = Apply(source, expr(head));
                // foreach (var subNavigation in tail) {
                //     source = ThenApply(source, expr(subNavigation));
                // }
            }

            // split on dots
            // for the first, build an expression
            // Apply with .Include
            // for subequent, build an expression
            // Apply with .ThenInclude


            // Or do the .Include with string navigation properties (preserves the dots in the string)
            // And somehow correct for the inflection
            CorrectCapitalisation<Customer>("Orders.Products.Manufacturer.Address");
        }

        /// <summary>
        /// Recursive function which, starting from an object of the specified type, navigates from one
        /// related entity to the next via the provided <c>navigationPathEntries</c>, and returns a
        /// single string, corrected for casing, which is suitable for an EntityFramework .Include(string) call
        /// </summary>
        public string BuildNavigationPath(Type entityType, string[] navigationPathEntries) {

            if (navigationPathEntries == null || navigationPathEntries.Length == 0) {
                throw new ArgumentNullException(nameof(navigationPathEntries));
            }

            var thisMemberAccess = navigationPathEntries[0];
            var subsequentMemberAccesses = navigationPathEntries.Skip(1).ToArray();

            // For the first level of dependency to walk, correct the casing of the string
            var caseCorrectedProperty = entityType.GetProperties()
                .SingleOrDefault(p => p.Name.Equals(navigationPathEntries[0], StringComparison.OrdinalIgnoreCase));

            if (caseCorrectedProperty == null) {
                throw new ArgumentException($"Cannot access member '{thisMemberAccess}' of type '{entityType.Name}'. Please check spelling and pluralisation.");
            }

            if (!subsequentMemberAccesses.Any()) {
                // Base case, there is no further to navigate
                return caseCorrectedProperty.Name;
            }
            else {
                // Else, correct the casing for the current level, if applicable, and append the results of further dependency-walking
                var navigatedMemberType = StreamlineCollectionType(caseCorrectedProperty.PropertyType);
                return caseCorrectedProperty.Name + "." + BuildNavigationPath(navigatedMemberType, subsequentMemberAccesses);
            }
        }

        private Type StreamlineCollectionType(Type type) {

            if (type.GetInterfaces().Any(i => i == typeof(IEnumerable))) {
                // TODO aware of the fact that this is testing non-generic Enumerable
                // TODO also aware that the first generic type a) may not exist in the case, and b) might not be the type of the collection
                return type.GenericTypeArguments[0];
            }

            /*var e = type.GetGenericTypeDefinition().GetGenericTypeDefinition();

            var ist = type.GetInterfaces();
            var is2 = type.GenericTypeArguments;

            var ooo = type.GetGenericTypeDefinition();
            var i2 = type.GetTypeInfo().GetInterfaces();
            var iii = type.GetTypeInfo().ImplementedInterfaces;
            var arr = type.IsArray;
            var t = type.IsConstructedGenericType;*/

            return type;
        }

        private string[] RemoveBackfills(string[] includePaths) {

            var includePaths2 = includePaths
                .Select((p, i) => new { Path = p, Index = i })
                .ToList();

            var result = new List<string>();

            for (int i = includePaths2.Count - 1; i >= 0; i--) {
                var me = includePaths2[i];

                if (includePaths2.Any(x => x.Index < me.Index && x.Path.StartsWith(me.Path))) {
                    includePaths2.RemoveAt(i);
                }
            }

            return includePaths2.Select(t => t.Path).ToArray();
        }

        private string CorrectCapitalisation<TEntity>(string input) {
            return input;
        }

        private IIncludableQueryable<TEntity, TIncludedMember> Apply<TEntity, TIncludedMember>(IQueryable<TEntity> source, Expression<Func<TEntity, TIncludedMember>> expression) 
            where TEntity : class
            where TIncludedMember : class {

            return source.Include(expression);
        }

        private IIncludableQueryable<TEntity, TIncludedMember> ThenApply<TEntity, TOriginallyIncludedMember, TIncludedMember>(IIncludableQueryable<TEntity, TOriginallyIncludedMember> source, Expression<Func<TOriginallyIncludedMember, TIncludedMember>> expression)
            where TEntity : class
            where TIncludedMember : class
            where TOriginallyIncludedMember : class {

            return source.ThenInclude(expression);
        }
    }
}
