using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using Api.Framework.Base;
using Api.Framework.Context;
using Api.Framework.Managers;
using RestSharp;

namespace Api.Framework.Actions
{
    /// <summary>
    /// This class allows you to trigger any request type using the trigger call method 
    /// </summary>
    public class CallManager : ApiBase
    {
        /// <summary>
        /// This method will receive the parameters to create the Delete request and send the response back
        /// </summary>
        /// <param name="methodType"></param>
        /// <param name="endPoint"> The URL request need to triggered </param>
        /// <param name="dataFormat"> Type of body format </param>
        /// <param name="parameters"> List of all parameters with its type </param>
        /// <param name="requestBody"> Request object that is sent to the API </param>
        /// <returns> Response from API </returns>
        public static IRestResponse TriggerCall(Method methodType, string endPoint, DataFormat dataFormat, [Optional] IEnumerable<RequestParameter> parameters, [Optional] object requestBody)
        {
            RestRequest = methodType.SetRequestMethod(dataFormat);
            if (parameters != null)
                IRestRequest = RestRequest.AddRequestParameter(parameters);
            if (requestBody != Missing.Value)
                IRestRequest = RestRequest.AddJsonRequestToBody(requestBody);
            RestClient = endPoint.SetRequestEndpoint();
            RestResponse = RestClient.SendRequestAndGetResponse(IRestRequest);
            return RestResponse;
        }
    }
}