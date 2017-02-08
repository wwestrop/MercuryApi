using Microsoft.AspNetCore.Http;

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

    /*internal class QueryParser : IQueryParser {

        public string[][] Parse(string includeExpression) {
            throw new NotImplementedException();
        }

    }*/
}
