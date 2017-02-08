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

            // TODO don't do toLower here, as other implementations of the path builder might try to be cleverer about 
            // mapping the resource typename, inflection, whatever onto the DBContext model. actually existing impl is doing case-insensitive comparison anyway
            
            var allIncludePaths = WebUtility.UrlDecode(query.ToString())    // TODO query.ToString() won't work, actually have to do something sensible here
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim().ToLower())
                .Distinct()
                .ToArray();

            allIncludePaths = RemoveRedundantNavigations(allIncludePaths);

            // Separate the dotted navigation strings of into array of strings, one array element per property-navigation
            return allIncludePaths
                .Select(fullPath => fullPath.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries))
                .ToArray();
        }

        /// TODO: move out of parser into separate optimisation step
        /// <summary>
        /// Removes unnecessary include paths (i.e. those that we will walk
        /// through anyway on the way to reaching a further entity)
        /// </summary>
        private string[] RemoveRedundantNavigations(string[] includePaths) {

            var remainingNavigationPaths = includePaths
                .OrderByDescending(s => s.Length)
                .Select((p, i) => new { Path = p, Index = i })
                .ToList();

            for (int i = remainingNavigationPaths.Count - 1; i >= 0; i--) {
                var me = remainingNavigationPaths[i];
                
                // If any path is shorter than this one, and is a prefix of this one, it is redundant
                // (because traversing the longer path covers the same ground as the shorter path anyway)
                if (remainingNavigationPaths.Any(x => x.Index < me.Index && x.Path.StartsWith(me.Path))) {
                    remainingNavigationPaths.RemoveAt(i);
                }
            }

            return remainingNavigationPaths.Select(t => t.Path).ToArray();
        }

    }
}
