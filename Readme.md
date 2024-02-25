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


## Configuring Query String Matches
The `QueryStringExpectationBuilder` and `QueryStringParameterExpectationBuilder` can be used to set up expectations for query strings. [See the MockServer documentation](https://www.mock-server.com/mock_server/getting_started.html) for the kinds of syntax that are supported.

Example: Requiring a query string with the name `qs` with a specific value of `valueone`:
```csharp
new MockServerClient(httpClient)
    .When(RequestBuilder().Build()
        .WithMethod(HttpMethod.Get)
        .WithPath("/mypath")
        .WithQueryStringParameters(QueryStringExpectationBuilder
            .Build()
            .WithParameter(QueryStringParameterExpectationBuilder
                .Build()
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
                .WithQueryStringParameters(QueryStringExpectationBuilder
                    .Build()
                    .WithParameter(QueryStringParameterExpectationBuilder
                        .Build()
                        .WithName("qs")
                        .WithValue(SchemaValue.Uuid()))
                )
            )
        .Respond(new ResponseBuilder().WithStatusCode(200));
```

Multiple query string match configurations can be specified by chaining `WithParameter` calls.

Multiple query string value match configurations can be specified by chaining `WithValue` calls.

### Optional Query Strings
MockServer supports specifying optional query strings. These can be setup by specifying a parameter name with a leading `?`, e.g.
```csharp
.WithParameter(QueryStringParameterExpectationBuilder
                        .Build()
                        .WithName("?qs")
                        .WithValue(SchemaValue.Uuid())
```

### Exclusive Query Strings
Query strings which must not appear are supported by specifying a parameter with a leading `!`, e.g.
```csharp
.WithParameter(QueryStringParameterExpectationBuilder
    .Build()
    .WithName("!qs")
    .WithValue(SchemaValue.Integer())
```

### Regex Matching
Both querystring name and values can be matched by query strings, e.g.
```csharp
.WithParameter(QueryStringParameterExpectationBuilder
    .Build()
    .WithName("[A-z]{0,10}")
    .WithValue("[A-Z0-9\\-]+")
```
This would produce a call to the MockServer REST API as follows:
```json
    "queryStringParameters": {
        "[A-z]{0,10}": ["[A-Z0-9\\-]+"]
    }
```


The same can be achieved using the schema value syntax, e.g.
```csharp
.WithParameter(QueryStringParameterExpectationBuilder
    .Build()
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