using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Tryitter.Models;
using Tryitter.Services;
using System.Text.Json;
using System.Text;

namespace Tryitter.Test;
public class TestUser : IClassFixture<WebApplicationFactory<Program>>
{
  private readonly WebApplicationFactory<Program> _factory;

    public TestUser(WebApplicationFactory<Program> factory)
    {
      _factory = factory;
    }
  
  [Theory(DisplayName = "POST /login deve responder com status 200 - 299 para rotas")]
  [InlineData("/login", "billg@net.com", "unclebill", 1, "12345678")]
  public async Task LoginEndpointReturnSuccess(string url, string emailUser, string nameUser, int id, string password)
  {
    string token = new TokenGenerator().Generate(new User { EmailUser = emailUser, IdUser = id, NameUser = nameUser });
    var client = _factory.CreateClient();

    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    var response = await client.PostAsync(url, new StringContent(JsonSerializer.Serialize(new UserDTO { EmailUser = emailUser, Password = password }), Encoding.UTF8, "application/json"));
  
    response.EnsureSuccessStatusCode();
  }

  [Theory(DisplayName = "POST /user deve responder com status 200 - 299 para rotas")]
  [InlineData("/user", "billg@net.com", "unclebill", "12345678")]
  public async Task PostEndpointReturnSuccess(string url, string emailUser, string nameUser, string password)
  {
    var client = _factory.CreateClient();

    var response = await client.PostAsync(url, new StringContent(JsonSerializer.Serialize(new User { EmailUser = emailUser, NameUser = nameUser, Password = password }), Encoding.UTF8, "application/json"));
  
    response.EnsureSuccessStatusCode();
  }

  [Theory(DisplayName = "DELETE /user/{id} deve responder com status 200 - 299 para rotas")]
  [InlineData("/user", "bilaaaaaalg@net.com", "unclebill", "12345678", 10)]
  public async Task DeleteEndpointReturnSuccess(string url, string emailUser, string nameUser, string password, int id)
  {
    User user = new() {EmailUser = emailUser, NameUser = nameUser, Password = password};
    user.IdUser = id;

    string token = new TokenGenerator().Generate(new User { EmailUser = emailUser, IdUser = user.IdUser, NameUser = nameUser });
    var client = _factory.CreateClient();

  
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    var response = await client.DeleteAsync($"{url}/{id}");
  
    response.EnsureSuccessStatusCode();
  }
}
