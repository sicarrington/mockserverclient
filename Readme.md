# MockServerClient

[![.NET](https://github.com/sicarrington/mockserverclient/actions/workflows/buildtest.yml/badge.svg?branch=master)](https://github.com/sicarrington/mockserverclient/actions/workflows/buildtest.yml)

Dot net wrapper for [mock server](https://www.mock-server.com).

## Example usage

Given mock server is running on port 1080, e.g.
```bash
docker run -p 1080:1080 -d --platform linux/amd64 mockserver/mockserver:latest
```

Setup a mock using the library in c# code

```csharp 
using (var httpClient = new HttpClient{ BaseAddress = new Uri("http://localhost:1080/")})
{
    var mockServerClient = new MockServerClient(httpClient);
    
    var expectedRequest = new RequestBuilder().WithPath("/helloworld").Create()

    await mockServerClient.SetExpectations(
        new Expectation
        {
            HttpRequest = expectedRequest,
            HttpResponse = new ResponseBuilder().WithStatusCode(200).Create()
        });
}
```

Test the mock
```bash
curl -I localhost:1080/helloworld
```

Verify the expected request was made

```csharp
await mockServerClient.Verify(expectedRequest.Create(), VerificationTimes.Once)
```

## Configuring Matches

### Header Matches
The expectation builders allow you to setup matching in a few different ways. See the examples below and  [the MockServer documentation](https://www.mock-server.com/mock_server/getting_started.html) for the kinds of syntax that are supported.

Example: requiring a named header with a specific value.
```csharp
new MockServerClient(httpClient)
    .When(RequestBuilder.Build()
        .WithPath("/mypath")
        .WithMethod(HttpMethod.Post)
        .WithHeaders(new Dictionary<string, IEnumerable<string>>{{"HeaderOne", new[]{"ValueOne"}}}))
    .Respond(responseBuilder => responseBuilder.WithStatusCode(200));
```

MockServer supports a schema value syntax enabling you to match header values by type. This is enabled via the `SchemaValue` class. 

Example: Requiring a header with the name `HeaderOne` which has an `integer` value.
```csharp
new MockServerClient(httpClient)
    .When(RequestBuilder.Build()
        .WithPath("/mypath")
        .WithMethod(HttpMethod.Post)
        .WithHeaders(headersResponseBuilder => headersResponseBuilder.WithHeader(
                headerResponseBuilder => headerResponseBuilder
                    .WithName("HeaderOne")
                    .WithValues(
                    headerValueResponseBuilder => headerValueResponseBuilder.WithValue(SchemaValue.Integer())))))
    .Respond(responseBuilder => responseBuilder.WithStatusCode(200));
```
Multiple expected header values can be configured by chaining calls to `WithValue` on the `headerValueResponseBuilder` and multiple headers can be configured by chaining calls to `WithHeader` on the `headersResponseBuilder`.

#### Optional Headers
Optionality can be configured in mockserver using the `?` operator. For example
```csharp
.WithHeaders(headersResponseBuilder => headersResponseBuilder.WithHeader(
    headerResponseBuilder => headerResponseBuilder
        .WithName("?HeaderTwo")
        .WithValues(
            headerValueResponseBuilder => headerValueResponseBuilder.WithValue("?TestValue"))))
            
```

#### Exclusive Headers
Exclusive headers/values can be configured using the `!` operator. i.e. when a header/value must not be included.
```csharp
.WithHeader(
    headerResponseBuilder => headerResponseBuilder
        .WithName("!HeaderThree"))
```

#### Regex Matching
Both header name and values can be matched by regex strings, e.g.
```csharp
.WithHeader(
    headerResponseBuilder => headerResponseBuilder
        .WithName("Header.*")
        .WithValues(
            headerValueResponseBuilder => headerValueResponseBuilder.WithValue(@"[A-Z0-9\\-]+")))
```

Similarly, regex can be used using the schema value syntax, e.g.
```csharp
.WithHeader(
    headerResponseBuilder => headerResponseBuilder
        .WithName("Header.*")
        .WithValues(
            headerValueResponseBuilder => headerValueResponseBuilder.WithValue(SchemaValue.StringWithPattern("[A-Z0-9\\-]+"))))
```

### Configuring Query String Matches
The expectation builders allow you to setup matching in a few different ways. See the examples below and  [the MockServer documentation](https://www.mock-server.com/mock_server/getting_started.html) for the kinds of syntax that are supported.

Example: Requiring a query string with the name `qs` with a specific value of `valueone`:
```csharp
new MockServerClient(httpClient)
    .When(RequestBuilder().Build()
        .WithMethod(HttpMethod.Get)
        .WithPath("/mypath")
        .WithQueryStringParameters(queryStringExpectationBuilder => queryStringExpectationBuilder
            .WithParameter(queryStringParameterExpectationBuilder => queryStringParameterExpectationBuilder
                .WithName("qs")
                .WithValue("valueone"))
                )
            )
    .Respond(new ResponseBuilder().WithStatusCode(200));
```

MockServer supports a schema value syntax enabling you to match query string parameters by type. This is enabled via the `SchemaValue` class. 

Example: Requiring a query string with the name `qs` which contains a guid/uuid value:
```csharp
        new MockServerClient(httpClient)
            .When(new RequestBuilder()
                .WithMethod(HttpMethod.Get)
                .WithPath("/mypath")
                .WithQueryStringParameters(queryStringExpectationBuilder => queryStringExpectationBuilder
                    .WithParameter(queryStringParameterExpectationBuilder => queryStringParameterExpectationBuilder
                        .WithName("qs")
                        .WithValue(SchemaValue.Uuid()))
                )
            )
        .Respond(new ResponseBuilder().WithStatusCode(200));
```

Multiple query string match configurations can be specified by chaining `WithParameter` calls.

Multiple query string value match configurations can be specified by chaining `WithValue` calls.

#### Optional Query Strings
MockServer supports specifying optional query strings. These can be setup by specifying a parameter name with a leading `?`, e.g.
```csharp
.WithParameter(queryStringParameterExpectationBuilder => queryStringParameterExpectationBuilder
                        .WithName("?qs")
                        .WithValue(SchemaValue.Uuid())
```

#### Exclusive Query Strings
Query strings which must not appear are supported by specifying a parameter with a leading `!`, e.g.
```csharp
.WithParameter(queryStringParameterExpectationBuilder => queryStringParameterExpectationBuilder
    .WithName("!qs")
    .WithValue(SchemaValue.Integer())
```

#### Regex Matching
Both querystring name and values can be matched by regex strings, e.g.
```csharp
.WithParameter(queryStringParameterExpectationBuilder => queryStringParameterExpectationBuilder
    .WithName("[A-z]{0,10}")
    .WithValue("[A-Z0-9\\-]+")
```


The same can be achieved using the schema value syntax, e.g.
```csharp
.WithParameter(queryStringParameterExpectationBuilder => queryStringParameterExpectationBuilder
    .WithName("[A-z]{0,10}")
    .WithValue(SchemaValue.StringWithPattern("[A-Z0-9\\-]+"))
```
This would produce a call to the MockServer REST API as follows:
```json
"queryStringParameters": {
    "[A-z]{0,10}": [{
        "schema": {
            "type": "string",
            "pattern": "[A-Z0-9\\-]+"
            }
        }]
    }
```

## Configuring Response Headers
Response headers can be configured as part of setting up a mock. Multiple headers and multiple values per header can be added. 

For example, a simple static header collection can be added as follows
```csharp
new MockServerClient(httpClient)
    .When(RequestBuilder.Build()
        .WithMethod(HttpMethod.Get)
        .WithPath("http://mockserver/test"))
    .Respond(
        responseBuilder => responseBuilder
            .WithStatusCode(200)
            .WithHeaders(new Dictionary<string, IEnumerable<string>>
                { { "HeaderA", new[] { "HeaderValueA", "HeaderValueB" } } }));
```

Headers can also be added via the builder - For example the code below sets up a header named `HeaderA` with values `ValueA` and `ValueB`. It also adds a header named `HeaderB` with a value derived from the request.
```csharp
new MockServerClient(httpClient)
    .When(RequestBuilder.Build()
        .WithMethod(HttpMethod.Get)
        .WithPath("http://mypath"))
    .Respond(
        responseBuilder => responseBuilder
            .WithStatusCode(200)
            .WithHeaders(headersResponseBuilder => headersResponseBuilder
                .WithHeader(headerResponseBuilder => headerResponseBuilder
                    .WithName("HeaderA")
                    .WithValues(headerValueResponseBuilder => headerValueResponseBuilder
                        .WithValue("ValueA")
                        .WithValue("ValueB")))
                .WithHeader(headerResponseBuilder => headerResponseBuilder
                    .WithName("HeaderB")
                    .WithValues(headerValueResponseBuilder => headerValueResponseBuilder
                        .WithValue(request => request.Path)))
         ));
```