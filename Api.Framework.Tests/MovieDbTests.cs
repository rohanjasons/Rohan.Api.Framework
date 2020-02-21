using Api.Framework.Tests.Helper;
using RestSharp;
using Shouldly;
using System.Net;
using Xunit;

namespace Api.Framework.Tests
{
    public class MovieDbTests : ApiBase
    {
        private const string apiKey = "";
        const string baseEndpoint = "https://api.themoviedb.org/3";
        const string movie = "/movie/83";
        const string rating = "/rating";
        readonly string authToken = "/authentication/token/new";
        readonly string authWLogin = "/authentication/token/validate_with_login";
        readonly string authSession = "/authentication/session/new";
        const string userName = "";
        const string password = "";

        [Fact]
        public void SuccessfullyRateMovie()
        {
            string authentication = baseEndpoint + authToken;

            // gets request token
            RestRequest = RequestMethodManager.SetRequestMethod(RestRequest, Method.GET, DataFormat.Json);
            IRestRequest = ParameterManager.AddRequestQueryParameter(RestRequest, "api_key", apiKey);
            RestClient = EndpointManager.SetRequestEndpoint(RestClient, authentication);
            RestResponse = RequestManager.SendRequestAndGetResponse(RestClient, RestResponse, IRestRequest);

            RestResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

            var text = RestResponse.Content.Substring(70);
            string json = $"{{ \"username\": \"{userName}\", \"password\": \"{password}\", \"request_token\": \"{RegexHelpers.RegexHelper(text)}\" }}";
            string authLoginEndpoint = baseEndpoint + authWLogin;

            // validates request token 
            RestRequest = RequestMethodManager.SetRequestMethod(RestRequest, Method.POST, DataFormat.Json);
            IRestRequest = ParameterManager.AddRequestQueryParameter(RestRequest, "api_key", apiKey);
            IRestRequest = RequestBodyManager.AddJsonRequestToBody(RestRequest, json);
            RestClient = EndpointManager.SetRequestEndpoint(RestClient, authLoginEndpoint);
            RestResponse = RequestManager.SendRequestAndGetResponse(RestClient, RestResponse, IRestRequest);

            RestResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

            string requestToken = $"{{ \"request_token\": \"{RegexHelpers.RegexHelper(text)}\" }}";
            string authEndpoint = baseEndpoint + authSession;
            // requests session id to be used in subsequent requests
            RestRequest = RequestMethodManager.SetRequestMethod(RestRequest, Method.POST, DataFormat.Json);
            IRestRequest = ParameterManager.AddRequestQueryParameter(RestRequest, "api_key", apiKey);
            IRestRequest = RequestBodyManager.AddJsonRequestToBody(RestRequest, requestToken);
            RestClient = EndpointManager.SetRequestEndpoint(RestClient, authEndpoint);
            RestResponse = RequestManager.SendRequestAndGetResponse(RestClient, RestResponse, IRestRequest);

            RestResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

            string endpoint = baseEndpoint + movie + rating;
            string sessionResponse = RestResponse.Content.Substring(28);

            var ratingBody = "{ \"value\" : \"8.5\" }";
            var someTest = RegexHelpers.RegexHelper(sessionResponse);
            // posts a rating against a movie passing the session id
            RestRequest = RequestMethodManager.SetRequestMethod(RestRequest, Method.POST, DataFormat.Json);
            IRestRequest = HeaderManager.AddRequestHeader(RestRequest, "Content-Type", "application/json;charset=utf-8");
            IRestRequest = ParameterManager.AddRequestQueryParameter(RestRequest, "api_key", apiKey);
            IRestRequest = ParameterManager.AddRequestQueryParameter(RestRequest, "session_id", RegexHelpers.RegexHelper(sessionResponse));
            IRestRequest = RequestBodyManager.AddJsonRequestToBody(RestRequest, ratingBody);
            RestClient = EndpointManager.SetRequestEndpoint(RestClient, endpoint);
            RestResponse = RequestManager.SendRequestAndGetResponse(RestClient, RestResponse, IRestRequest);

            RestResponse.StatusCode.ShouldBe(HttpStatusCode.Created);
        }

        [Fact]
        public void GetTest()
        {
            string endpoint = baseEndpoint + movie;

            RestRequest = RequestMethodManager.SetRequestMethod(RestRequest, Method.GET, DataFormat.Json);
            IRestRequest = ParameterManager.AddRequestQueryParameter(RestRequest, "api_key", apiKey);
            RestClient = EndpointManager.SetRequestEndpoint(RestClient, endpoint);
            RestResponse = RequestManager.SendRequestAndGetResponse(RestClient, RestResponse, IRestRequest);

            RestResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public void PostTest()
        {
            string endpoint = baseEndpoint + movie + rating;

            RestRequest = RequestMethodManager.SetRequestMethod(RestRequest, Method.POST, DataFormat.Json);
            IRestRequest = HeaderManager.AddRequestHeader(RestRequest, "Content-Type", "application/json;charset=utf-8");
            IRestRequest = ParameterManager.AddRequestQueryParameter(RestRequest, "api_key", apiKey);
            IRestRequest = ParameterManager.AddRequestParameter(RestRequest, "value", "8.5", ParameterType.RequestBody);
            RestClient = EndpointManager.SetRequestEndpoint(RestClient, endpoint);
            RestResponse = RestClient.Execute(IRestRequest);
        }

        [Fact]
        public void PostTest2()
        {
            string endpoint = baseEndpoint + movie + rating;
            var json = "{ 'value' : '8.5' }";

            RestRequest = RequestMethodManager.SetRequestMethod(RestRequest, Method.POST, DataFormat.Json);
            IRestRequest = HeaderManager.AddRequestHeader(RestRequest, "Content-Type", "application/json;charset=utf-8");
            IRestRequest = ParameterManager.AddRequestParameter(RestRequest, "api_key", apiKey, ParameterType.QueryString);
            IRestRequest = RequestBodyManager.AddJsonRequestToBody(RestRequest, json);
            RestClient = EndpointManager.SetRequestEndpoint(RestClient, endpoint);
            RestResponse = RestClient.Execute(IRestRequest);
        }
    }
}