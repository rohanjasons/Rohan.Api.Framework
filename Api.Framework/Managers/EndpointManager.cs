using System;
using RestSharp;

namespace Api.Framework.Managers
{
    /// <summary>
    /// This class is for the purpose of setting an endpoint to be used within a request
    /// </summary>
    public static class EndpointManager
    {
        /// <summary>
        /// Set this url as the endpoint to submit the request to.
        /// </summary>
        /// <param name="endpoint"> Endpoint required for request </param>
        /// <returns> Returns a restClient with an endpoint </returns>
        public static RestClient SetRequestEndpoint(this string endpoint)
        {
            try
            {
                return new RestClient(endpoint);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
    }
}