using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tryitter.Repository;
using Tryitter.Services;
using Tryitter.Models;

namespace Tryitter.Controllers
{
  [ApiController]
  [Route("login")]
  public class LoginController : Controller
  {
    private readonly UserRepository _repository;
    public LoginController(UserRepository repository)
    {
      _repository = repository;
    }

    [HttpPost]
    [AllowAnonymous]
     public IActionResult Login([FromBody] UserDTO user)
     {
        bool isAUser = _repository.LoginUser(user);

        if (isAUser)
        {
          TokenGenerator tokenGenerator = new();
          var newToken = tokenGenerator.Generate();

          return Ok(new { token = newToken});
        }

        return Unauthorized("Email or Password is wrong");
     }
  }
}