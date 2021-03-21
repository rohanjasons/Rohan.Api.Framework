using System;
using RestSharp;

namespace Api.Framework.Managers
{
    /// <summary>
    /// This class is used to add the request body to the RestRequest container to be sent to the API
    /// </summary>
    public static class RequestBodyManager
    {
        /// <summary>
        /// This method adds the JSON request body to the container that is sent to the API
        /// </summary>
        /// <param name="restRequest"> Container for data that is sent to API </param>
        /// <param name="requestObject"> Request object that is sent to the API </param>
        /// <returns> Container for data that is sent to API </returns>
        public static IRestRequest AddJsonRequestToBody(this RestRequest restRequest, object requestObject)
        {
            try
            {
                return restRequest.AddJsonBody(requestObject);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        /// <summary>
        /// This method adds the XML request body to the container that is sent to the API
        /// </summary>
        /// <param name="restRequest">  Container for data that is sent to API </param>
        /// <param name="requestObject"> Request object that is sent to the API </param>
        /// <returns> Container for data that is sent to API </returns>
        public static IRestRequest AddXmlRequestToBody(this RestRequest restRequest, object requestObject)
        {
            try
            {
                return restRequest.AddXmlBody(requestObject);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
    }
}