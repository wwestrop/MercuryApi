using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MercuryApi {
    internal interface IRequestScopedStorage {
        T Read<T, U>(U key);
    }

    public class RequestScopedStorage {

        private readonly IHttpContextAccessor _httpContext;

        public RequestScopedStorage(IHttpContextAccessor httpContext) {
            this._httpContext = httpContext;
        }

        public void Foo() {

        }

    }

    /*internal class RequestScopedStorage : IRequestScopedStorage {

        public T Read<T, U>(U key) {
            HttpContext.i   // <-- need to get this off of a ControllerBase
        }

    }*/
}
