using MockServer.Client.Net.Models;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MockServer.Client.Net.Unit
{
    public class ApiTests
    {
        [Fact]
        public async void GivenSetExpectationsIsCalled_ThenApiIsCalledToSetExpectations()
        {
            var expectedUri = "http://mockserverhost:1080/expectation";
            var messageHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            messageHandler
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK
               })
               .Verifiable();

            var httpClient = new HttpClient(messageHandler.Object);
            httpClient.BaseAddress = new Uri("http://mockserverhost:1080/");

            var mockServerClient = new MockServerClient(httpClient);

            await mockServerClient.SetExpectations(new Expectation());

            messageHandler.Protected().Verify(
               "SendAsync",
               Moq.Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req =>
                  req.Method == HttpMethod.Put
                  && req.RequestUri.ToString() == expectedUri
               ),
               ItExpr.IsAny<CancellationToken>()
            );
        }
        // [Fact]
        // public async void Test1()
        // {
        //     Console.WriteLine($"TESTING.....");
        //     using (var httpClient = new HttpClient())
        //     {
        //         httpClient.BaseAddress = new Uri("http://mockserverhost:1080/");
        //         var mockServerClient = new MockServer.Client.Net.MockServerClient(
        //             httpClient
        //         );

        //         await mockServerClient.SetExpectations(new Expectation
        //         {
        //             HttpRequest = new HttpRequest
        //             {
        //                 Path = "/hello",
        //                 Method = "GET",
        //                 Secure = false,
        //                 KeepAlive = true,
        //                 Body = "hellooooooo"

        //             },
        //             HttpResponse = new HttpResponse
        //             {
        //                 Delay = new Delay
        //                 {
        //                     TimeUnit = "SECONDS",
        //                     Value = 0
        //                 },
        //                 StatusCode = 200,
        //                 Body = "This is the response body"
        //             }
        //         });


        //         var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "/hello");
        //         var response = await httpClient.SendAsync(httpRequestMessage);
        //         var responseContent = await response.Content.ReadAsStringAsync();
        //         Console.WriteLine($"Response content... {responseContent}");

        //     }
        // }
    }
}
