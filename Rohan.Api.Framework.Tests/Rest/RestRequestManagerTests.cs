namespace Rohan.Api.Framework.Tests.Rest;

[TestFixture]
[AllureNUnit, Description("RestRequestManager Unit Tests")]
public class RestRequestManagerTests
{
    private RestRequestManager? _requestManager;
    private const string Resource = "/test";

    [SetUp]
    public void Setup()
    {
        _requestManager = new RestRequestManager(Resource, Method.Get);
    }

    [Test]
    public void AddBearerToken_ShouldAddAuthorizationHeader()
    {
        // Arrange
        const string token = "sample-token";

        // Act
        _requestManager?.AddBearerToken(token);
        var request = _requestManager?.Build();

        // Assert
        request.Should().NotBeNull();
        request?.Parameters.Should().ContainSingle(parameter => parameter.Name == "Authorization" && parameter.Value!.ToString() == $"Bearer {token}");
    }

    [Test]
    public void AddHeader_ShouldAddHeaderToRequest()
    {
        // Arrange
        const string key = "Custom-Header";
        const string value = "HeaderValue";

        // Act
        _requestManager?.AddHeader(key, value);
        var request = _requestManager?.Build();

        // Assert
        request.Should().NotBeNull();
        request?.Parameters.Should().ContainSingle(parameter => parameter.Name == key && parameter.Value!.ToString() == value);
    }

    [Test]
    public void AddHeaders_ShouldAddHeadersToRequest()
    {
        // Arrange
        var headers = new Dictionary<string, string> { { "Header1", "Value1" }, { "Header2", "Value2" } };

        // Act
        _requestManager?.AddHeaders(headers);
        var request = _requestManager?.Build();

        // Assert
        request.Should().NotBeNull();
        request?.Parameters.Should().Contain(parameter => parameter.Name == "Header1" && parameter.Value!.ToString() == "Value1");
        request?.Parameters.Should().Contain(parameter => parameter.Name == "Header2" && parameter.Value!.ToString() == "Value2");
    }

    [Test]
    public void AddQueryParameter_ShouldAddQueryParameterToRequest()
    {
        // Arrange
        const string key = "queryKey";
        const string value = "queryValue";

        // Act
        _requestManager?.AddQueryParameter(key, value);
        var request = _requestManager?.Build();

        // Assert
        request.Should().NotBeNull();
        request?.Parameters.Should().ContainSingle(parameter => parameter.Name == key && parameter.Value!.ToString() == value && parameter.Type == ParameterType.QueryString);
    }

    [Test]
    public void AddParameter_ShouldAddParameterToRequest()
    {
        // Arrange
        const string key = "paramKey";
        const string value = "paramValue";

        // Act
        _requestManager?.AddParameter(key, value);
        var request = _requestManager?.Build();

        // Assert
        request.Should().NotBeNull();
        request?.Parameters.Should().ContainSingle(parameter => parameter.Name == key && parameter.Value!.ToString() == value);
    }

    [Test]
    public void AddParameter_WithType_ShouldAddTypedParameterToRequest()
    {
        // Arrange
        const string key = "paramKey";
        const string value = "paramValue";
        const ParameterType parameterType = ParameterType.HttpHeader;

        // Act
        _requestManager?.AddParameter(key, value, parameterType);
        var request = _requestManager?.Build();

        // Assert
        request.Should().NotBeNull();
        request?.Parameters.Should().ContainSingle(parameter => parameter.Name == key && parameter.Value!.ToString() == value && parameter.Type == parameterType);
    }

    [Test]
    public void AddUrlSegment_ShouldAddUrlSegmentToRequest()
    {
        // Arrange
        const string key = "segmentKey";
        const string value = "segmentValue";

        // Act
        _requestManager?.AddUrlSegment(key, value);
        var request = _requestManager?.Build();

        // Assert
        request.Should().NotBeNull();
        request?.Parameters.Should().ContainSingle(parameter => parameter.Name == key && parameter.Value!.ToString() == value && parameter.Type == ParameterType.UrlSegment);
    }

    [Test]
    public void AddJsonBody_ShouldAddJsonBodyToRequest()
    {
        // Arrange
        var body = new { Name = "John Doe", Age = 30 };

        // Act
        _requestManager?.AddJsonBody(body);
        var request = _requestManager?.Build();

        // Assert
        request.Should().NotBeNull();
        request?.Parameters.Should().ContainSingle(parameter => parameter.Type == ParameterType.RequestBody && parameter.Value!.ToString()!.Contains("John Doe"));
    }

    [Test]
    public void AddXmlBody_ShouldAddXmlBodyToRequest()
    {
        // Arrange
        var body = new { Name = "John Doe", Age = 30 };

        // Act
        _requestManager?.AddXmlBody(body);
        var request = _requestManager?.Build();

        // Assert
        request.Should().NotBeNull();
        request?.Parameters.Should().ContainSingle(parameter => parameter.Type == ParameterType.RequestBody && parameter.Value!.ToString()!.Contains("John Doe"));
    }
}