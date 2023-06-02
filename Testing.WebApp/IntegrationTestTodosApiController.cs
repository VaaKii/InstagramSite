using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using App.Public.DTO.v1;
using App.Public.DTO.v1.Identity;
using App.Public.DTO.v1.Testing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;
using AppUser = App.Domain.Identity.AppUser;

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
        var response = await _client.GetAsync("api/v1.0/UserPost/GetUserPosts");
        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
    }

    [Fact]
    public async void Test_Get_Post_Not_Existing_Id()
    {
        var response = await _client.GetAsync($"api/v1.0/UserPost/GetUserPost?id=0");
            Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
    }

}