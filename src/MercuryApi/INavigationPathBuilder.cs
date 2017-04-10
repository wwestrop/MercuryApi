using MercuryApi.Exceptions;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace MercuryApi
{
	/// <summary>
	/// Responsible for taking a client-provided expression of the related entities which they
	/// wish to be returned, and building an Entity Framework compatible navigation path string.
	/// </summary>
	internal interface INavigationPathBuilder {

		/// <param name="entityType">The type of the root entity, on which the provided navigation path will be applied</param>
		/// <param name="memberAccesses">The ordered list of navigation properties the client wishes to walk down and return</param>
		string Build(Type entityType, QueryParseResult memberAccesses);

	}

	/// <summary>
	/// In order to build an EF-compatible navigation path, the client-provided
	/// path is walked and inconsistencies in capitalisation are corrected. 
	/// </summary>
	/// <remarks>
	/// This does mean that if two members are exposed which differ only in capitalisation,
	/// this will fail. Also if the relationship names on your API-exposed resource differ
	/// from their Entity Framework internal representation (except for casing, it will also fail).
	/// </remarks>
	internal class NavigationPathBuilder : INavigationPathBuilder {

		/// <summary>
		/// Recursive function which, starting from an object of the specified type, navigates from one
		/// related entity to the next via the provided <c>navigationPathEntries</c>, and returns a
		/// single string, corrected for casing, which is suitable for an EntityFramework .Include(string) call
		/// </summary>
		public string Build(Type entityType, QueryParseResult memberAccesses) {
			return Build(entityType, memberAccesses.ToArray());
		}

		/// <summary>
		/// Recursive function which, starting from an object of the specified type, navigates from one
		/// related entity to the next via the provided <c>navigationPathEntries</c>, and returns a
		/// single string, corrected for casing, which is suitable for an EntityFramework .Include(string) call
		/// </summary>
		private string Build(Type entityType, string[] memberAccesses) {

			if (memberAccesses == null || memberAccesses.Length == 0) {
				throw new ArgumentNullException(nameof(memberAccesses));
			}

			var thisMemberAccess = memberAccesses[0];
			var subsequentMemberAccesses = memberAccesses.Skip(1).ToArray();

			// For the first level of dependency to walk, correct the casing of the request-string based upon what actually exists on the entity
			var caseCorrectedProperties = entityType.GetProperties()
				.Where(p => p.Name.Equals(memberAccesses[0], StringComparison.OrdinalIgnoreCase));
			if (caseCorrectedProperties.Count() > 1) {
				throw new AmbiguousNavigationException(thisMemberAccess, entityType);
			}

			var caseCorrectedProperty = caseCorrectedProperties.FirstOrDefault();
			if (caseCorrectedProperty == null) {
				throw new InvalidNavigationException(thisMemberAccess, entityType);
			}

			if (!subsequentMemberAccesses.Any()) {
				// Base case, there is no further to navigate
				return caseCorrectedProperty.Name;
			}
			else {
				// Else, correct the casing for the current level, if applicable, and append the results of further dependency-walking
				var navigatedMemberType = StreamlineCollectionType(caseCorrectedProperty.PropertyType);
				return caseCorrectedProperty.Name + "." + Build(navigatedMemberType, subsequentMemberAccesses);
			}
		}

		// TODO add caching of some sort to avoid repeated reflection
		private Type StreamlineCollectionType(Type type) {

			if (type.GetInterfaces().Any(i => i == typeof(IEnumerable))) {
				// TODO aware of the fact that this is testing non-generic Enumerable
				// TODO also aware that the first generic type a) may not exist in the case, and b) might not be the type of the collection
				return type.GenericTypeArguments[0];
			}

			return type;
		}

	}
}
