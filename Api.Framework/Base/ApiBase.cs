using System;
using Newtonsoft.Json;
using RestSharp;

namespace Api.Framework
{
    public abstract class ApiBase
    {
        protected RestClient RestClient;
        protected RestRequest RestRequest;
        protected IRestResponse RestResponse;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="restClient"></param>
        /// <param name="restRequest"></param>
        /// <param name="restResponse"></param>
        protected ApiBase(RestClient restClient, RestRequest restRequest, IRestResponse restResponse)
        {
            RestClient = restClient;
            RestRequest = restRequest;
            RestResponse = restResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <param name="dataFormat"></param>
        /// <returns></returns>
        protected RestRequest SetRequestMethod(Method method, DataFormat dataFormat)
        {
            RestRequest = new RestRequest(method)
            {
                RequestFormat = dataFormat
            };

            return RestRequest;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        protected RestClient SetRequestEndpoint(string endpoint)
        {
            RestClient = new RestClient(endpoint);
            return RestClient;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected IRestResponse SendRequestAndGetResponse(RestRequest request)
        {
            RestResponse = RestClient.Execute(request);
            return RestResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        protected void SendRequest(RestRequest request)
        {
            RestClient.Execute(request);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestObject"></param>
        /// <param name="applicationParameter"></param>
        /// <param name="parameterType"></param>
        protected void AddRequestHeader(Object requestObject, string applicationParameter, ParameterType parameterType)
        {
            var json = JsonConvert.SerializeObject(requestObject);
            RestRequest.AddParameter(applicationParameter, json, parameterType);
        }
    }
}
