using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Tryitter.Models;
using Tryitter.Services;

namespace Tryitter.Test;
public class TestUser : IClassFixture<WebApplicationFactory<Program>>
{
  private readonly WebApplicationFactory<Program> _factory;

    public TestUser(WebApplicationFactory<Program> factory)
    {
      _factory = factory;
    }
  
  [Theory(DisplayName = "POST /user deve responder com status 200 - 299 para rotas")]
  [InlineData("/login", "billg@net.com", "unclebill", "12345678")]
  public async Task PostEndpointReturnSuccess(string url, string emailUser, string nameUser, string password)
  {
    User user = new() {EmailUser =  emailUser, NameUser = nameUser, Password = password};

    string token = new TokenGenerator().Generate(user);
    var client = _factory.CreateClient();
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    var response = await client.GetAsync(url);

    response.EnsureSuccessStatusCode();
  }
}
