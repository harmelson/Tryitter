using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
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

        Regex regex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        if (user.EmailUser == "" || user.Password == "")
        {
          return BadRequest("All fields must be passed");
        }

        if (!regex.IsMatch(user.EmailUser)) return BadRequest("EmailUser must be valid");

        if (user.EmailUser.Length < 8) return BadRequest("EmailUser must have at least 8 characters");

        if (user.Password.Length < 8) return BadRequest("Password must have at least 8 characters");

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
