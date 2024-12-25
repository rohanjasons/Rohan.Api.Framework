# Rohan.Api.Framework

A C# library that provides simplified classes for making RESTful API calls using RestSharp. It offers a more streamlined and fluent interface for constructing requests, managing clients, and handling responses.

## Table of Contents

  - [Overview](#overview)
  - [Features](#features)
  - [Setting Up Your Machine](#setting-up-your-machine)
    - [Configure Nuget Package Source](#configure-nuget-package-source)
  - [Installation](#installation)
  - [Usage](#usage)
    - [Creating a RestClientManager](#creating-a-restclientmanager)
    - [Building a RestRequestManager](#building-a-restrequestmanager)
    - [Executing Requests](#executing-requests)
    - [Handling Responses](#handling-responses)
    - [Disposing of RestClientManager](#disposing-of-restclientmanager)
  - [Classes](#classes)
    - [RestClientManager](#restclientmanager)
    - [RestRequestManager](#restrequestmanager)
    - [RestResponseManager](#restresponsemanager)
  - [Examples](#examples)
    - [GET Request Example](#get-request-example)
    - [POST Request with JSON Body](#post-request-with-json-body)
    - [Handling Cookies and Headers Example](#handling-cookies-and-headers-example)
    - [XML Response Handling Example](#xml-response-handling-example)
  - [Notes](#notes)
  - [Contributing](#contributing)

## Overview

`Rohan.Api.Framework` simplifies the process of making HTTP requests by wrapping around the popular [RestSharp](https://restsharp.dev/). It provides manager classes that offer a more fluent and intuitive way to construct requests, manage clients, and handle responses.

## Features

- **Simplified Client Management**: Easily create and manage HTTP clients.
- **Fluent Request Building**: Construct requests with a fluent interface.
- **Enhanced Response Handling**: Access response details and perform assertions.
- **Supports Asynchronous and Synchronous Execution**: Choose between async and sync methods.
- **Cookie Management**: Automatically handles cookies during requests, making it easy to access cookies from responses.
- **Timing and Performance**: Measure the duration of requests.

## Setting Up Your Machine

### Configure NuGet Package Source

1. Open Visual Studio.
2. Go to **Tools** > **Options** > **NuGet Package Manager** > **Package Sources**.
3. Click the green plus button (+) in the upper-right corner.
4. Enter the following:

   - **Name**: `testPackages`
   - **Source**: `TBC`

   > **Note**: You need to perform these steps on every machine that requires access to your packages. If you prefer a one-time setup that can be checked into your repository, consider using the `NuGet.exe` instructions.

## Installation

To use `Rohan.Api.Framework`, ensure you have the RestSharp library installed:

```shell
Install-Package RestSharp
```

Include the `Rohan.Api.Framework` namespace in your project: 

```csharp
using Rohan.Api.Framework;
```

## Usage

### Creating a RestClientManager

Instantiate `RestClientManager` with the base URL of your API:

```csharp
var clientManager = new RestClientManager("https://api.example.com");
```

### Building a RestRequestManager

Use `RestRequestManager` to construct your request:

```csharp
var requestManager = new RestRequestManager("/endpoint", Method.GET)
	.AddHeader("Accept", "application/json")
	.AddQueryParameter("key", "value");

var request = requestManager.Build();
```

### Executing Requests

Executing the request asynchronously: 

```csharp
var response = await clientManager.ExecuteAsync(request);
```

Or execute the request and get timing information: 

```csharp
var timedResponse = await clientManager.TimedExecuteAsync(request);
Console.WriteLine($"Response Time: {timedResponse.ResponseTime.TotalMilliseconds} ms");
```

### Handling Responses

Access response details using `RestResponseManager`:

```csharp
var restResponse = new RestResponseManager(response);

if (restResponse.IsSuccessful)
{
    var data = restResponse.GetData<MyDataType>();
    // Process data
}
else
{
    Console.WriteLine($"Error: {restResponse.ErrorMessage}");
}
```

### Disposing of RestClientManager

It is important to properly dispose of `RestClientManager` to release resources:

```csharp
clientManager.Dispose();
```

## Classes

### RestClientManager

Manage the HTTP client for sending requests.

**Constructor**

```csharp
public RestClientManager(string baseUrl)
```

- `baseUrl`: The base URL for the API.

**Methods**

- `Task<RestResponseManager> TimedExecuteAsync(RestRequest request)`: Executes a request asynchronously and measures response time.
- `Task<RestResponse> ExecuteAsync(RestRequest request)`: Executes a request asynchronously.
- `RestResponse Execute(RestRequest request)`: Executes a request synchronously.
- `void Dispose()`: Disposes of the `RestClientManager` to release resources.

### RestRequestManager 

Provides a fluent interface for building HTTP requests.

**Constructor**

```csharp
public RestRequestManager(string resource, Method method)
```

- `resource`: The endpoint or resource path.
- `method`: The HTTP method (`GET`, `POST`, `PUT`, `DELETE`, etc.).

**Methods**

- `RestRequestManager AddBearerToken(string token)`: Adds an Authorisation header with a Bearer token.
- `RestRequestManager AddHeader(string key, string value)`: Adds a header to the request.
- `RestRequestManager AddHeaders(Dictionary<string, string> headers)`: Adds multiple headers.
- `RestRequestManager AddQueryParameter(string key, string value)`: Adds a query parameter.
- `RestRequestManager AddParameter(string key, string value)`: Adds a parameter to the request body.
- `RestRequestManager AddParameter(string key, string value, ParameterType parameterType)`: Adds a parameter with a specific type.
- `RestRequestManager AddUrlSegment(string key, string value)`: Adds a URL segment for templated URLs.
- `RestRequestManager AddJsonBody(object body)`: Adds a JSON body to the request.
- `RestRequestManager AddXmlBody(object body)`: Adds an XML body to the request.
- `RestRequest Build()`: Builds and returns the RestRequest object.

### RestResponseManager

Wraps the response and provides access to its details.

**Constructors** 

```csharp
public RestResponseManager(RestResponse response)
public RestResponseManager(RestResponse response, TimeSpan duration)
public RestResponseManager(RestResponse response, TimeSpan duration, CookieCollection cookies)
```

**Properties**

- `HttpStatusCode StatusCode`: HTTP status code.
- `string Content`: Response content as a string.
- `byte[] RawBytes`: Raw bytes of the response content.
- `bool IsSuccessful`: Indicates if the request was successful.
- `string ErrorMessage`: Error message if any.
- `IReadOnlyDictionary<string, string> Headers`: Dictionary of response headers.
- `IReadOnlyDictionary<string, string> Cookies`: Dictionary of cookies from the response.
- `TimeSpan ResponseTime`: Duration of the request.
- `Uri ResponseUri`: URI that responded to the request.
- `string ContentType`: Content type of the response.
- `Exception Exception`: Exception thrown during the request, if any.
- `long ContentLength`: Size of the response content.
- `string Server`: Server that sent the response.
- `string ContentEncoding`: Encoding of the response content.
- `DateTime? Date`: Date and time when the response was sent.

**Methods**

- `T GetData<T>()`: Deserialises JSON content to type T.
- `T GetXmlData<T>()`: Deserialises XML content to type T.
- `dynamic GetDynamicData()`: Deserialises content to a dynamic object.
- `void SaveContentToFile(string filePath)`: Saves response content to a file.
- `bool HasHeader(string headerName)`: Checks if a specific header exists.
- `string GetHeaderValue(string headerName)`: Retrieves a header value.
- `bool HasCookie(string cookieName)`: Checks if a specific cookie exists.
- `string GetCookieValue(string cookieName)`: Retrieves a cookie value.
- `bool HasStatusCode(HttpStatusCode expectedStatusCode)`: Checks if the status code matches.
- `void AssertStatusCode(HttpStatusCode expectedStatusCode)`: Asserts the status code matches.
- `void AssertHeaderExists(string headerName)`: Asserts that a header exists.
- `void AssertContentIsNotEmpty()`: Asserts that the content is not empty.
- `void AssertIsSuccessful()`: Asserts that the request was successful.
  
## Examples

### GET Request Example

```csharp
var clientManager = new RestClientManager("https://api.example.com");

var requestManager = new RestRequestManager("/users/{id}", Method.GET)
    .AddUrlSegment("id", "1")
    .AddHeader("Accept", "application/json");

var request = requestManager.Build();
var response = await clientManager.ExecuteAsync(request);
var restResponse = new RestResponseManager(response);

if (restResponse.IsSuccessful)
{
    var user = restResponse.GetData<User>();
    Console.WriteLine($"User Name: {user.Name}");
}
else
{
    Console.WriteLine($"Error: {restResponse.ErrorMessage}");
}
```

### POST Request with JSON Body

```csharp
var clientManager = new RestClientManager("https://api.example.com");

var newUser = new { Name = "John Doe", Email = "john.doe@example.com" };

var requestManager = new RestRequestManager("/users", Method.POST)
    .AddJsonBody(newUser)
    .AddBearerToken("your_access_token");
var request = requestManager.Build();

var response = await clientManager.ExecuteAsync(request);
var restResponse = new RestResponseManager(response);

if (restResponse.IsSuccessful)
{
    var createdUser = restResponse.GetData<User>();
    Console.WriteLine($"Created User ID: {createdUser.Id}");
}
else
{
    Console.WriteLine($"Error: {restResponse.ErrorMessage}");
}
```

### Handling Cookies and Headers Example

```csharp
var clientManager = new RestClientManager("https://api.example.com");

var requestManager = new RestRequestManager("/products/{id}", Method.GET)
    .AddUrlSegment("id", "42");

var request = requestManager.Build();
var response = await clientManager.ExecuteAsync(request);
var restResponse = new RestResponseManager(response);

if (restResponse.IsSuccessful)
{
    var product = restResponse.GetData<Product>();
    Console.WriteLine($"Product Name: {product.Name}");

    if (restResponse.HasCookie("session"))
    {
        var sessionCookie = restResponse.GetCookieValue("session");
        Console.WriteLine($"Session Cookie: {sessionCookie}");
    }
    
    if (restResponse.HasHeader("Server"))
    {
        Console.WriteLine($"Server: {restResponse.Server}");
    }
}
else
{
    Console.WriteLine($"Error: {restResponse.ErrorMessage}");
}
```

### XML Response Handling Example

```csharp
var clientManager = new RestClientManager("https://api.example.com");

var requestManager = new RestRequestManager("/users", Method.GET)
    .AddHeader("Accept", "application/xml");

var request = requestManager.Build();
var response = await clientManager.ExecuteAsync(request);
var restResponse = new RestResponseManager(response);

if (restResponse.IsSuccessful)
{
    var user = restResponse.GetXmlData<User>();
    Console.WriteLine($"User Name: {user.Name}");
}
else
{
    Console.WriteLine($"Error: {restResponse.ErrorMessage}");
}
```

## Notes

- Ensure proper exception handling when executing requests.
- Use the assertion methods in `RestResponseManager` to validate responses.
- The library is built on top of RestSharp; familiarity with RestSharp is beneficial.
- Be cautious with nullable reference types and handle potential `null` values appropriately.

## Contributing 

Contributions are welcome! Feel free to submit issues or pull requests to enhance the functionality.
