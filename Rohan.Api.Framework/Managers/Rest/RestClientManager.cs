namespace Rohan.Api.Framework.Managers.Rest;

/// <summary>
/// Manages REST client operations, including executing requests and handling cookies.
/// </summary>
public sealed class RestClientManager : IDisposable
{
    private readonly RestClient _clientManager;
    private readonly CookieContainer _cookieContainer;
    private bool _disposed;

    /// <summary>
    /// Initialises a new instance of the RestClientManager with the given base URL.
    /// </summary>
    /// <param name="baseUrl">The base URL for the API.</param>
    public RestClientManager(string baseUrl)
    {
        _cookieContainer = new CookieContainer();
        var options = new RestClientOptions(new Uri(baseUrl))
        {
            ConfigureMessageHandler = handler =>
            {
                var httpClientHandler = new HttpClientHandler
                {
                    CookieContainer = _cookieContainer,
                    UseCookies = true,
                };
                return httpClientHandler;
            }
        };
        _clientManager = new RestClient(options);
    }

    /// <summary>
    /// Executes a request asynchronously and measures the elapsed time.
    /// </summary>
    /// <param name="request">The request to execute.</param>
    /// <returns>A RestResponseManager containing response details, elapsed time, and cookies.</returns>
    public async Task<RestResponseManager> TimedExecuteAsync(RestRequest request)
    {
        ThrowIfDisposed();
        var stopwatch = Stopwatch.StartNew();
        var response = await _clientManager.ExecuteAsync(request);
        stopwatch.Stop();

        var uri = response.ResponseUri ?? _clientManager.Options.BaseUrl;
        var cookies = _cookieContainer.GetCookies(uri!);

        return new RestResponseManager(response, stopwatch.Elapsed, cookies);
    }

    /// <summary>
    /// Executes a request asynchronously.
    /// </summary>
    /// <param name="request">The request to execute.</param>
    /// <returns>The RestResponse from the request.</returns>
    public async Task<RestResponse?> ExecuteAsync(RestRequest request)
    {
        ThrowIfDisposed();
        return await _clientManager.ExecuteAsync(request);
    }

    /// <summary>
    /// Executes a request synchronously.
    /// </summary>
    /// <param name="request">The request to execute.</param>
    /// <returns>The RestResponse from the request.</returns>
    public RestResponse? Execute(RestRequest request)
    {
        ThrowIfDisposed();
        return _clientManager.Execute(request);
    }

    /// <summary>
    /// Releases the resources used by the RestClientManager.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Disposes of the resources used by the object.
    /// </summary>
    /// <param name="disposing">Indicates whether the method is being called 
    /// from a `Dispose` method (true) or a finaliser (false).</param>
    private void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
        {
            _clientManager.Dispose();
        }
        _disposed = true;
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, nameof(RestClientManager));
    }
}