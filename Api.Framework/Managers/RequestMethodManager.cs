using RestSharp;

namespace Api.Framework.Managers
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
        /// <param name="method"></param>
        /// <param name="dataFormat"></param>
        /// <returns></returns>
        public static RestRequest SetRequestMethod(this Method method, DataFormat dataFormat)
        {
            return new RestRequest(method)
            {
                RequestFormat = dataFormat
            };
        }
    }
}