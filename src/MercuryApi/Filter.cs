using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MercuryApi
{
    /// <summary>
    /// OWIN pipeline filter which sniffs the incoming HTTP request for an <c>?include</c> string,
    /// and if present, extracts the value in this request into request-scoped storage.
    /// </summary>
    public class Filter
    {
    }
}
