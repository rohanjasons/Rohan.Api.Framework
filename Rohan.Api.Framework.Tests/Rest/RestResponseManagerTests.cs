using Rohan.Api.Framework.Tests.Objects;

namespace Rohan.Api.Framework.Tests.Rest;

[TestFixture]
[AllureNUnit, Description("RestResponseManager Unit Tests")]
public class RestResponseManagerTests
{
    private RestResponse? _response;
    private RestResponseManager? _responseManager;
    private CookieCollection? _cookies;

    [SetUp]
    [AllureBefore("Setup Mock RestResponse for Interrogation")]
    public void Setup()
    {
        _response = new RestResponse
        {
            StatusCode = HttpStatusCode.OK,
            Content = "{ \"Name\": \"John Doe\", \"Age\": 30 }",
            RawBytes = [1, 2, 3],
            ContentType = "application/json",
            ResponseUri = new Uri("http://example.com"),
            ErrorMessage = null,
            Headers = [new("Server", "TestServer")]
        };
        _cookies = [new Cookie("sessionId", "12345")];
        _responseManager = new RestResponseManager(_response, TimeSpan.FromMilliseconds(500), _cookies);
    }

    [Test]
    public void StatusCode_ShouldReturnCorrectStatusCode()
    {
        // Assert
        _responseManager?.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Test]
    public void Content_ShouldReturnCorrectContent()
    {
        // Assert
        _responseManager?.Content.Should().Be("{ \"Name\": \"John Doe\", \"Age\": 30 }");
    }

    [Test]
    public void RawBytes_ShouldReturnCorrectRawBytes()
    {
        // Assert
        _responseManager?.RawBytes.Should().Equal(1, 2, 3);
    }

    [Test]
    public void IsSuccessful_ShouldReturnTrueForSuccessStatusCode()
    {
        // Arrange
        _response!.StatusCode = HttpStatusCode.OK;
        _response.IsSuccessStatusCode = true;
        _responseManager = new RestResponseManager(_response, TimeSpan.FromMilliseconds(500), _cookies!);

        // Assert
        _responseManager.IsSuccessful.Should().BeTrue();
    }

    [Test]
    public void Headers_ShouldReturnCorrectHeaders()
    {
        // Assert
        _responseManager?.Headers.Should().ContainKey("Server").WhoseValue.Should().Be("TestServer");
    }

    [Test]
    public void Cookies_ShouldReturnCorrectCookies()
    {
        // Assert
        _responseManager?.Cookies.Should().ContainKey("sessionId").WhoseValue.Should().Be("12345");
    }

    [Test]
    public void ResponseTime_ShouldReturnCorrectDuration()
    {
        // Assert
        _responseManager?.ResponseTime.Should().Be(TimeSpan.FromMilliseconds(500));
    }

    [Test]
    public void ResponseUri_ShouldReturnCorrectUri()
    {
        // Assert
        _responseManager?.ResponseUri.Should().Be(new Uri("http://example.com"));
    }

    [Test]
    public void GetData_ShouldDeserialiseDynamicJsonContentCorrectly()
    {
        // Act
        var data = _responseManager?.GetData<dynamic>();

        // Assert
        ((string)data!.Name).Should().Be("John Doe");
        ((int)data!.Age).Should().Be(30);
    }

    [Test]
    public void GetDynaData_ShouldDeserialiseDynamicJsonContentCorrectly()
    {
        // Act
        var data = _responseManager?.GetDynamicData();

        // Assert
        ((string)data!.Name).Should().Be("John Doe");
        ((int)data.Age).Should().Be(30);
    }

    [Test]
    public void GetData_ShouldDeserialiseJsonContentCorrectly()
    {
        // Act
        var data = _responseManager?.GetData<Person>();

        // Assert
        data?.Name.Should().Be("John Doe");
        data?.Age.Should().Be(30);
    }

    [Test]
    public void GetXmlData_ShouldDeserializeXmlContentCorrectly()
    {
        // Arrange
        _response!.Content = "<Person><Name>John Doe</Name><Age>30</Age></Person>";

        // Act
        var data = _responseManager?.GetXmlData<Person>();

        // Assert
        data?.Name.Should().Be("John Doe");
        data?.Age.Should().Be(30);
    }

    [Test]
    public void ContentType_ShouldReturnCorrectContentType()
    {
        // Assert
        _responseManager?.ContentType.Should().Be("application/json");
    }

    [Test]
    public void Exception_ShouldReturnNullIfNoExceptionOccurred()
    {
        // Assert
        _responseManager?.Exception.Should().BeNull();
    }

    [Test]
    public void HasHeader_ShouldReturnTrueIfHeaderExists()
    {
        // Assert
        _responseManager?.HasHeader("Server").Should().BeTrue();
    }

    [Test]
    public void GetHeaderValue_ShouldReturnCorrectHeaderValue()
    {
        // Assert
        _responseManager?.GetHeaderValue("Server").Should().Be("TestServer");
    }

    [Test]
    public void HasCookie_ShouldReturnTrueIfCookieExists()
    {
        // Assert
        _responseManager?.HasCookie("sessionId").Should().BeTrue();
    }

    [Test]
    public void GetCookieValue_ShouldReturnCorrectCookieValue()
    {
        // Assert
        _responseManager?.GetCookieValue("sessionId").Should().Be("12345");
    }

    [Test]
    public void ContentLength_ShouldReturnCorrectContentLength()
    {
        // Assert
        _responseManager?.ContentLength.Should().Be(3);
    }

    [Test]
    public void Server_ShouldReturnCorrectServerValue()
    {
        // Assert
        _responseManager?.Server.Should().Be("TestServer");
    }

    [Test]
    public void ContentEncoding_ShouldReturnNullIfNotPresent()
    {
        // Assert
        _responseManager?.ContentEncoding.Should().BeNull();
    }

    [Test]
    public void Date_ShouldReturnNullIfNotPresent()
    {
        // Assert
        _responseManager?.Date.Should().BeNull();
    }

    [Test]
    public void HasStatusCode_ShouldReturnTrueForMatchingStatusCode()
    {
        // Assert
        _responseManager?.HasStatusCode(HttpStatusCode.OK).Should().BeTrue();
    }

    [Test]
    public void AssertStatusCode_ShouldNotThrowForMatchingStatusCode()
    {
        // Act & Assert
        var act = () => _responseManager?.AssertStatusCode(HttpStatusCode.OK);
        act.Should().NotThrow<InvalidOperationException>();
    }

    [Test]
    public void AssertIsSuccessful_ShouldNotThrowForSuccessfulResponse()
    {
        // Arrange
        _response!.IsSuccessStatusCode = true;

        // Act & Assert
        var act = _responseManager!.AssertIsSuccessful;
        act.Should().NotThrow<InvalidOperationException>();
    }
}