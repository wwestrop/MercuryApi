using System;

namespace MercuryApi.Exceptions
{
	/// <summary>
	/// Exception thrown when the string provided by the client doesn't correspond to
	/// a navigation property on the underlying entity (probably a spelling mistake). 
	/// </summary>
	public class InvalidNavigationException : Exception
	{
		public InvalidNavigationException(string attemptedNavigation, Type entityType)
			: base ($"Cannot access member '{attemptedNavigation}' of type '{entityType.Name}'. Please check spelling and pluralisation.") {
		}
	}
}
