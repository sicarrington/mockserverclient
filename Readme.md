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