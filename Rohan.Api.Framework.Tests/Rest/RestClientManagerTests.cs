namespace Rohan.Api.Framework.Tests.Rest;

[TestFixture]
[AllureNUnit, Description("RestClientManager Unit Tests")]
public class RestClientManagerTests
{
    private RestClientManager? _clientManager;
    // TODO: Will remove once tests are mocked
    private const string BaseUrl = "https://api.restful-api.dev/objects";

    [SetUp]
    [AllureBefore("Setup RestClient")]
    public void Setup()
    {
        _clientManager = new RestClientManager(BaseUrl);
    }

    [TearDown]
    [AllureAfter("Dispose of RestClient")]
    public void TearDown()
    {
        _clientManager?.Dispose();
    }

    [Test]
    public async Task TimedExecuteAsync_ShouldReturnResponseWithElapsedTime()
    {
        // Arrange
        var request = new RestRequest("/1");

        // Act
        var responseManager = await _clientManager?.TimedExecuteAsync(request)!;

        // Assert
        responseManager.Should().NotBeNull();
        responseManager.ResponseTime.Should().BeGreaterThan(TimeSpan.Zero);
    }

    [Test]
    public async Task ExecuteAsync_ShouldReturnResponse()
    {
        // Arrange
        var request = new RestRequest("/1");

        // Act
        var response = await _clientManager?.ExecuteAsync(request)!;

        // Assert
        response.Should().NotBeNull();
        response?.StatusCode.Should().NotBe(HttpStatusCode.NotFound);
    }

    [Test]
    public void Execute_ShouldReturnResponse()
    {
        // Arrange
        var request = new RestRequest("/1", Method.Post);

        // Act
        var response = _clientManager?.Execute(request)!;

        // Assert
        response.Should().NotBeNull();
        response?.StatusCode.Should().NotBe(HttpStatusCode.NotFound);
    }
}