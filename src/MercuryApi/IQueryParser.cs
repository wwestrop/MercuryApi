using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace MercuryApi
{
	/// <summary>
	/// Responsible for taking a string provided by the client in the URL of the HTTP request
	/// and from it, extracting the desired list of related entities that they want returned. 
	/// </summary>
	internal interface IQueryParser
	{
		string[][] Parse(IQueryCollection query);
	}

	internal class QueryParser : IQueryParser {

		public string[][] Parse(IQueryCollection query) {

			var allIncludePaths = ExtractFromQueryString(query);

			allIncludePaths = RemoveRedundantNavigations(allIncludePaths);

			// Separate the dotted navigation strings of into array of strings, one array element per property-navigation
			return allIncludePaths
				.Select(fullPath => fullPath.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries))
				.ToArray();
		}

		private string[] ExtractFromQueryString(IQueryCollection query) {

			var includeRequests = query
				.Where(q => q.Key.Equals("include", StringComparison.OrdinalIgnoreCase))
				.SelectMany(q => q.Value.ToList())
				.Select(s => WebUtility.UrlDecode(s))
				.SelectMany(s => s.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				.Select(s => s.Trim())
				.ToArray();

			return includeRequests;
		}

		/// TODO: move out of parser into separate optimisation step
		/// <summary>
		/// Removes unnecessary include paths (i.e. those that we will walk
		/// through anyway on the way to reaching a further entity)
		/// </summary>
		private string[] RemoveRedundantNavigations(string[] includePaths) {

			var remainingNavigationPaths = includePaths
				.Distinct()
				.OrderByDescending(s => s.Length)
				.Select((p, i) => new { Path = p, Index = i })
				.ToList();

			for (int i = remainingNavigationPaths.Count - 1; i >= 0; i--) {
				var me = remainingNavigationPaths[i];
				
				// If any path is shorter than this one, and is a prefix of this one, it is redundant
				// (because traversing the longer path covers the same ground as the shorter path anyway)
				if (remainingNavigationPaths.Any(x => x.Index < me.Index && x.Path.StartsWith(me.Path, StringComparison.OrdinalIgnoreCase))) {
					remainingNavigationPaths.RemoveAt(i);
				}
			}

			return remainingNavigationPaths.Select(t => t.Path).ToArray();
		}

	}
}
