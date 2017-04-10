using System;
using System.Collections;
using System.Collections.Generic;

namespace MercuryApi
{
    internal class QueryParseResult : IReadOnlyList<string>
    {
		private readonly string[] _navigations;

		public QueryParseResult(string[] navigations) {
			if (navigations == null) {
				throw new ArgumentNullException(nameof(navigations));
			}

			this._navigations = navigations;
		}

		public string this[int index] => this._navigations[index];

		public int Count => this._navigations.Length;

		public IEnumerator<string> GetEnumerator() {
			return ((IEnumerable<string>)this._navigations).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return this._navigations.GetEnumerator();
		}
	}
}
