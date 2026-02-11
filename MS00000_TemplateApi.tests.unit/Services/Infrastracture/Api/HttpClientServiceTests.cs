using Microsoft.Extensions.Options;
using Moq;
using MS00000_TemplateApi.Model.Options;
using MS00000_TemplateApi.Services.Infrastracture.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit.Services.Infrastracture.Api;

public class HttpClientServiceTests
{
    private class TestHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage response;

        public TestHttpMessageHandler(HttpResponseMessage response)
        {
            this.response = response;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(response);
        }
    }

    [Fact]
    public async Task ExecuteAsync_RitornaResponseMockata()
    {
        // Arrange
        HttpResponseMessage fakeResponse = new(HttpStatusCode.OK);
        TestHttpMessageHandler handler = new(fakeResponse);
        HttpClient client = new(handler, disposeHandler: false);

        Mock<IHttpClientFactory> httpClientFactoryMock = new();
        httpClientFactoryMock
            .Setup(x => x.CreateClient("ServerApi"))
            .Returns(client);

        ServerApiOption option = new();
        Mock<IOptionsMonitor<ServerApiOption>> optionsMock = new();
        optionsMock
            .Setup(x => x.CurrentValue)
            .Returns(option);

        HttpClientService service = new(httpClientFactoryMock.Object, optionsMock.Object);

        HttpRequestMessage request = new(HttpMethod.Get, "https://example.com");
        CancellationToken token = CancellationToken.None;

        // Act
        HttpResponseMessage result = await service.ExecuteAsync(request, token);

        // Assert
        Assert.NotNull(result);
    }
}

