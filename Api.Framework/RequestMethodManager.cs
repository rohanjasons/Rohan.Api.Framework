using RestSharp;

namespace Api.Framework
{
    /// <summary>
    /// Determines the request method that will be used for the request i.e. POST; GET; PUT etc
    /// Also determines the data format of the request i.e. JSON; XML; HTTP etc
    /// </summary>
    public static class RequestMethodManager
    {
        /// <summary>
        /// This method establishes the request method that a request will be sent via
        /// This also sets the DataFormat of the request 
        /// </summary>
        /// <param name="restRequest"> Container for data used to make requests </param>
        /// <param name="method"> Method to use when making requests </param>
        /// <param name="dataFormat"> Data format of the request </param>
        /// <returns> Returns the container for data used to make requests </returns>
        public static RestRequest SetRequestMethod(RestRequest restRequest, Method method, DataFormat dataFormat)
        {
            restRequest = new RestRequest(method)
            {
                RequestFormat = dataFormat
            };

            return restRequest;
        }
    }
}