using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Tryitter.Repository;
using Tryitter.Models;

namespace Tryitter.Controllers
{
  [ApiController]
  [Route("user")]
  [AllowAnonymous]
  public class UserController : Controller
  {
    private readonly UserRepository _repository;
    public UserController(UserRepository repository)
    {
      _repository = repository;
    }

    [HttpPost]
    public IActionResult Add([FromBody] User user)
    {
      Regex regex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
      if (user.EmailUser == null || user.NameUser == null || user.Password == null)
      {
        return BadRequest("All fields must be passed");
      }

      if (!regex.IsMatch(user.EmailUser)) return BadRequest("EmailUser must be valid");
      if (user.EmailUser.Length < 8) return BadRequest("EmailUser must have at least 8 characters");

      if (user.Password.Length < 8) return BadRequest("Password must have at least 8 characters");

      return Created("",_repository.AddUser(user));
    }
  }
}
