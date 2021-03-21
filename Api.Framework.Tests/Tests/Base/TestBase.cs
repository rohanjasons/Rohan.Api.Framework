using System.Collections.Generic;
using Api.Framework.Base;
using Api.Framework.Context;

namespace Api.Framework.Tests.Tests.Base
{
    public class TestBase : ApiBase
    {
        protected IEnumerable<RequestParameter> RequestParameters;
        protected const string ApiKey = "";
        protected const string BaseEndpoint = "https://api.themoviedb.org/3";
        protected const string Movie = "/movie/83";
        protected const string Rating = "/rating";
        protected readonly string AuthToken = "/authentication/token/new";
        protected readonly string AuthWLogin = "/authentication/token/validate_with_login";
        protected readonly string AuthSession = "/authentication/session/new";
        protected const string UserName = "";
        protected const string Password = "";
    }
}
