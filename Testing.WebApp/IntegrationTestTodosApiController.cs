using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;

namespace Testing.WebApp;

public class IntegrationTestPostApiController : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _testOutputHelper;
    private string? _jwt;
    private string? _refreshToken;
    
    public IntegrationTestPostApiController(CustomWebApplicationFactory<Program> factory,
        ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
        _client = factory
            .CreateClient(new WebApplicationFactoryClientOptions()
                {
                    // dont follow redirects
                    AllowAutoRedirect = false
                }
            );
    }
    
    [Fact]
    public async void Test_Get_Register_Returns_Method_Not_Allowed()
    {
        var response = await _client.GetAsync("api/v1.0/identity/account/register");

        Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
    }

    [Fact]
    public async void Test_Get_Posts()
    {
        var response = await _client.GetAsync("api/v1.0/UserPost");
        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
    }

    [Fact]
    public async void Test_Get_Post_Not_Existing_Id()
    {
        var response = await _client.GetAsync($"api/v1.0/UserPost?id=783465294583746");
            Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
    }

}