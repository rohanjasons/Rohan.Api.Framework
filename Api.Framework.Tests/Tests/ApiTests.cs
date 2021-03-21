using System.Collections.Generic;
using System.Net;
using Api.Framework.Actions;
using Api.Framework.Context;
using Api.Framework.Tests.Helper;
using Api.Framework.Tests.Tests.Base;
using RestSharp;
using Shouldly;
using Xunit;

namespace Api.Framework.Tests.Tests
{
    public class ApiTests : TestBase
    {
        public ApiTests()
        {
            RequestParameters = new List<RequestParameter>
            {
                new RequestParameter()
                {
                    ParameterKey = "api_key", ParameterValue = ApiKey, ParameterType = ParameterType.QueryString
                }
            };
        }

        [Fact]
        public void TheMovieDbTests()
        {
            var authentication = BaseEndpoint + AuthToken;
            // gets request token
            var response = CallManager.TriggerCall(Method.GET, authentication, DataFormat.Json, RequestParameters);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var text = response.Content.Substring(70);
            string json = $"{{ \"username\": \"{UserName}\", \"password\": \"{Password}\", \"request_token\": \"{text.RegexHelper()}\" }}";
            string authLoginEndpoint = BaseEndpoint + AuthWLogin;

            // validates request token 
            var post = CallManager.TriggerCall(Method.POST, authLoginEndpoint, DataFormat.Json, RequestParameters, json);
            post.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}