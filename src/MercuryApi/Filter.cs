//using System;
//using Microsoft.AspNetCore.Mvc.Filters;

//namespace MercuryApi
//{
//    /// <summary>
//    /// OWIN pipeline filter which sniffs the incoming HTTP request for an <c>?include</c> string,
//    /// and if present, extracts the value in this request into request-scoped storage.
//    /// </summary>
//    internal class Filter : IActionFilter {

//        private readonly IQueryParser _parser;

//        internal Filter(IQueryParser parser) {
//            this._parser = parser;
//        }

//        public void OnActionExecuted(ActionExecutedContext context) {
//        }

//        public void OnActionExecuting(ActionExecutingContext context) {

//            var requestedIncludes = _parser.Parse(context.HttpContext.Request.Query);

//            context.HttpContext.Items[""] = requestedIncludes;

//            throw new NotImplementedException();
//        }

//    }
//}
