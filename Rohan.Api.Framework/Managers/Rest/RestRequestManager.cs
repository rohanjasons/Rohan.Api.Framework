namespace Rohan.Api.Framework.Managers.Rest;

/// <summary>
/// Manages the configuration and setup of REST requests.
/// </summary>
public class RestRequestManager
{
    private readonly RestRequest? _requestManager;

    /// <summary>
    /// Initialises a new instance of the RestRequestManager class with a specified resource and HTTP method.
    /// </summary>
    /// <param name="resource">The resource URI for the request.</param>
    /// <param name="method">The HTTP method to use for the request (e.g. GET, POST).</param>
    public RestRequestManager(string resource, Method method)
    {
        _requestManager = new RestRequest(resource, method);
    }

    /// <summary>
    /// Adds a Bearer token to the request's Authorisation header.
    /// </summary>
    /// <param name="token">The Bearer token to be added.</param>
    /// <returns>The current instance of RestRequestManager for method chaining.</returns>
    public RestRequestManager? AddBearerToken(string token)
    {
        _requestManager?.AddHeader("Authorization", $"Bearer {token}");
        return this;
    }

    /// <summary>
    /// Adds a header to the request.
    /// </summary>
    /// <param name="key">The name of the header.</param>
    /// <param name="value">The value of the header.</param>
    /// <returns>The current instance of RestRequestManager for method chaining.</returns>
    public RestRequestManager? AddHeader(string key, string value)
    {
        _requestManager?.AddHeader(key, value);
        return this;
    }

    /// <summary>
    /// Adds multiple headers to the request.
    /// </summary>
    /// <param name="headers">A dictionary containing header key-value pairs.</param>
    /// <returns>The current instance of RestRequestManager for method chaining.</returns>
    public RestRequestManager? AddHeaders(Dictionary<string, string> headers)
    {
        _requestManager?.AddHeaders(headers);
        return this;
    }

    /// <summary>
    /// Adds a query parameter to the request.
    /// </summary>
    /// <param name="key">The name of the query parameter.</param>
    /// <param name="value">The value of the query parameter.</param>
    /// <returns>The current instance of RestRequestManager for method chaining.</returns>
    public RestRequestManager? AddQueryParameter(string key, string value)
    {
        _requestManager?.AddQueryParameter(key, value);
        return this;
    }

    /// <summary>
    /// Adds a parameter to the request body.
    /// </summary>
    /// <param name="key">The name of the parameter.</param>
    /// <param name="value">The value of the parameter.</param>
    /// <returns>The current instance of RestRequestManager for method chaining.</returns>
    public RestRequestManager? AddParameter(string key, string value)
    {
        _requestManager?.AddParameter(key, value);
        return this;
    }

    /// <summary>
    /// Adds a parameter to the request body with a specified parameter type.
    /// </summary>
    /// <param name="key">The name of the parameter.</param>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="parameterType">The type of the parameter (e.g. QueryString, RequestBody).</param>
    /// <returns>The current instance of RestRequestManager for method chaining.</returns>
    public RestRequestManager? AddParameter(string key, string value, ParameterType parameterType)
    {
        _requestManager?.AddParameter(key, value, parameterType);
        return this;
    }

    /// <summary>
    /// Adds a URL segment to the request.
    /// </summary>
    /// <param name="key">The name of the URL segment.</param>
    /// <param name="value">The value of the URL segment.</param>
    /// <returns>The current instance of RestRequestManager for method chaining.</returns>
    public RestRequestManager? AddUrlSegment(string key, string value)
    {
        _requestManager?.AddUrlSegment(key, value);
        return this;
    }

    /// <summary>
    /// Adds a JSON body to the request.
    /// </summary>
    /// <param name="body">The object to be serialised as JSON and added to the request body.</param>
    /// <returns>The current instance of RestRequestManager for method chaining.</returns>
    public RestRequestManager? AddJsonBody(object body)
    {
        _requestManager?.AddJsonBody(body);
        return this;
    }

    /// <summary>
    /// Adds an XML body to the request.
    /// </summary>
    /// <param name="body">The object to be serialised as XML and added to the request body.</param>
    /// <returns>The current instance of RestRequestManager for method chaining.</returns>
    public RestRequestManager? AddXmlBody(object body)
    {
        _requestManager?.AddXmlBody(body);
        return this;
    }

    /// <summary>
    /// Builds and returns the configured RestRequest object.
    /// </summary>
    /// <returns>The configured RestRequest object.</returns>
    public RestRequest? Build()
    {
        return _requestManager;
    }
}