namespace Rohan.Api.Framework.Managers.Rest;

/// <summary>
/// Manages the configuration and setup of REST requests.
/// </summary>
public class RestResponseManager
{
    private readonly RestResponse? _responseManager;
    private readonly CookieCollection? _cookies;

    /// <summary>
    /// Initialises a new instance of the RestResponseManager class with the specified response.
    /// </summary>
    /// <param name="response">The RestResponse object containing the response details.</param>
    public RestResponseManager(RestResponse response)
    {
        _responseManager = response;
    }

    /// <summary>
    /// Initialises a new instance of the RestResponseManager class.
    /// </summary>
    /// <param name="response">The RestResponse object containing the HTTP response details.</param>
    /// <param name="duration">The time taken to receive the response.</param>
    public RestResponseManager(RestResponse response, TimeSpan duration)
    {
        _responseManager = response;
        ResponseTime = duration;
    }

    /// <summary>
    /// Initialises a new instance of the RestResponseManager class with the specified response, response time, and cookies.
    /// </summary>
    /// <param name="response">The RestResponse object containing the HTTP response details.</param>
    /// <param name="duration">The time taken to receive the response.</param>
    /// <param name="cookies">The collection of cookies returned in the response.</param>
    public RestResponseManager(RestResponse response, TimeSpan duration, CookieCollection cookies)
    {
        _responseManager = response;
        ResponseTime = duration;
        _cookies = cookies;
    }

    /// <summary>
    /// Gets the HTTP status code returned by the server in the response.
    /// </summary>
    public HttpStatusCode StatusCode => _responseManager!.StatusCode;

    /// <summary>
    /// Gets the content of the response as a string.
    /// </summary>
    public string Content => _responseManager!.Content!;

    /// <summary>
    /// Gets the raw bytes of the response content.
    /// </summary>
    public byte[] RawBytes => _responseManager!.RawBytes!;

    /// <summary>
    /// Indicates whether the request was successful based on the HTTP status code.
    /// </summary>
    public bool IsSuccessful => _responseManager!.IsSuccessStatusCode;

    /// <summary>
    /// Gets any error message associated with the response, if available.
    /// </summary>
    public string ErrorMessage => _responseManager!.ErrorMessage!;

    /// <summary>
    /// Gets the collection of headers returned in the response.
    /// </summary>
    public IReadOnlyDictionary<string, string> Headers
    {
        get
        {
            var headers = new Dictionary<string, string>();
            foreach (var header in _responseManager!.Headers!)
            {
                headers[header.Name] = header.Value;
            }
            return headers;
        }
    }

    /// <summary>
    /// Gets the collection of cookies returned in the response.
    /// </summary>
    public IReadOnlyDictionary<string, string> Cookies
    {
        get
        {
            var cookies = new Dictionary<string, string>();
            foreach (Cookie cookie in _cookies!)
            {
                cookies[cookie.Name] = cookie.Value;
            }
            return cookies;
        }
    }

    /// <summary>
    /// Gets the total duration of the request.
    /// </summary>
    public TimeSpan ResponseTime { get; }

    /// <summary>
    /// Gets the URI that actually responded to the request.
    /// </summary>
    public Uri ResponseUri => _responseManager!.ResponseUri!;

    /// <summary>
    /// Deserialises the JSON content of the response to a specified type.
    /// </summary>
    /// <typeparam name="T">The type to which the JSON should be deserialised.</typeparam>
    /// <returns>A deserialised JSON object.</returns>
    public T GetData<T>()
    {
        return JsonConvert.DeserializeObject<T>(_responseManager!.Content!)!;
    }

    /// <summary>
    /// Deserialises the XML content of the response to a specified type.
    /// </summary>
    /// <typeparam name="T">The type to which the XML content should be deserialised.</typeparam>
    /// <returns>A deserialised XML object.</returns>
    public T GetXmlData<T>()
    {
        var serializer = new XmlSerializer(typeof(T));
        var reader = new StringReader(_responseManager!.Content!);
        try
        {
            return (T)serializer.Deserialize(reader)!;

        }
        finally
        {
            reader.Dispose();
        }
    }

    /// <summary>
    /// Gets the content type of the response.
    /// </summary>
    public string ContentType => _responseManager!.ContentType!;

    /// <summary>
    /// Gets the exception thrown during the request.
    /// </summary>
    public Exception Exception => _responseManager!.ErrorException!;

    /// <summary>
    /// Checks if a header exists in the response.
    /// </summary>
    /// <param name="headerName">The name of the header to check.</param>
    /// <returns>True if the header exists; otherwise, false.</returns>
    public bool HasHeader(string headerName)
    {
        return Headers.ContainsKey(headerName);
    }

    /// <summary>
    /// Gets a header value by name.
    /// </summary>
    /// <param name="headerName">The name of the header to retrieve.</param>
    /// <returns>The value of the specified header, or null if the header is not found.</returns>
    public string GetHeaderValue(string headerName)
    {
        return Headers.TryGetValue(headerName, out var value) ? value : null!;
    }

    /// <summary>
    /// Checks if a cookie exists in the response.
    /// </summary>
    /// <param name="cookieName">The name of the cookie to check for.</param>
    /// <returns>True if the cookie exists; otherwise, false.</returns>
    public bool HasCookie(string cookieName)
    {
        return Cookies.ContainsKey(cookieName);
    }

    /// <summary>
    /// Gets a cookie value by name.
    /// </summary>
    /// <param name="cookieName">The name of the cookie to get.</param>
    /// <returns>The value of the specified cookie, or null if the cookie is not found.</returns>
    public string GetCookieValue(string cookieName)
    {
        return Cookies.TryGetValue(cookieName, out var value) ? value : null!;
    }

    /// <summary>
    /// Gets the length of the response content in bytes.
    /// </summary>
    public long ContentLength => RawBytes?.Length ?? 0;

    /// <summary>
    /// Gets the name of the server that sent the response.
    /// </summary>
    public string Server => GetHeaderValue("Server");

    /// <summary>
    /// Gets the encoding of the response content.
    /// </summary>
    public string ContentEncoding => GetHeaderValue("Content-Encoding");

    /// <summary>
    /// Gets the date and time when the response was sent.
    /// </summary>
    public DateTime? Date
    {
        get
        {
            var dateHeader = GetHeaderValue("Date");
            if (DateTime.TryParse(dateHeader,
                                  System.Globalization.CultureInfo.InvariantCulture,
                                  System.Globalization.DateTimeStyles.AssumeUniversal,
                                  out var date))
            {
                return date;
            }
            return null;
        }
    }

    /// <summary>
    /// Attempts to deserialise the JSON content of the response to a dynamic object.
    /// </summary>
    /// <returns>A dynamic object representing the JSON content of the response.</returns>
    public dynamic GetDynamicData()
    {
        return JsonConvert.DeserializeObject<dynamic>(_responseManager!.Content!)!;
    }

    /// <summary>
    /// Saves the response content to a file at the specified file path.
    /// </summary>
    /// <param name="filePath">The path of the file to save the content to.</param>
    public void SaveContentToFile(string filePath)
    {
        File.WriteAllBytes(filePath, RawBytes);
    }

    /// <summary>
    /// Checks if the response status code matches the expected status code.
    /// </summary>
    /// <param name="expectedStatusCode">The expected HTTP status code.</param>
    /// <returns>True if the status code matches; otherwise, false.</returns>
    public bool HasStatusCode(HttpStatusCode expectedStatusCode)
    {
        return StatusCode == expectedStatusCode;
    }

    /// <summary>
    /// Asserts that the response has the expected status code.
    /// </summary>
    /// <param name="expectedStatusCode">The expected HTTP status code.</param>
    /// <exception cref="InvalidOperationException">Thrown if the status code does not match the expected value.</exception>
    public void AssertStatusCode(HttpStatusCode expectedStatusCode)
    {
        if (StatusCode != expectedStatusCode)
        {
            throw new InvalidOperationException($"Expected status code {expectedStatusCode}, but got {StatusCode}.");
        }
    }

    /// <summary>
    /// Asserts that the response contains a specific header.
    /// </summary>
    /// <param name="headerName">The name of the header to check for.</param>
    /// <exception cref="InvalidOperationException">Thrown if the specified header is not found in the response.</exception>
    public void AssertHeaderExists(string headerName)
    {
        if (!HasHeader(headerName))
        {
            throw new InvalidOperationException($"Header '{headerName}' not found in the response.");
        }
    }

    /// <summary>
    /// Asserts that the response content is not null or empty.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the response content is null or empty.</exception>
    public void AssertContentIsNotEmpty()
    {
        if (string.IsNullOrEmpty(Content))
        {
            throw new InvalidOperationException("Response content is empty.");
        }
    }

    /// <summary>
    /// Asserts that the request was successful based on the HTTP status code.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the request was not successful.</exception>
    public void AssertIsSuccessful()
    {
        if (!IsSuccessful)
        {
            throw new InvalidOperationException($"Request was not successful. Status code: {StatusCode}, Error message: {ErrorMessage}");
        }
    }
}