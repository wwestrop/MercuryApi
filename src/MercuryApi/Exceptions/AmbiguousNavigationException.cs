using System;

namespace MercuryApi.Exceptions
{
	/// <summary>
	/// Exception thrown when we could find a navigation property requested by the client, 
	/// but for whatever reason, the client-provided string isn't enough to disambiguate 
	/// between two similar navigation properties. In the current implementation, this can 
	/// happen when the type has two navigation properties differing only in capitalisation. 
	/// </summary>
	public class AmbiguousNavigationException : Exception
	{
		public AmbiguousNavigationException(string requestedNavigation, Type entityType)
			:base ($"Entity type '{entityType.Name}' has multiple related entities which match the requested include path '{requestedNavigation}'") {
		}
	}
}
