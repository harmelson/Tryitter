using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Tryitter.Repository;
using Tryitter.Models;

namespace Tryitter.Controllers
{
  [ApiController]
  [Route("user")]
  public class UserController : Controller
  {
    private readonly UserRepository _repository;
    public UserController(UserRepository repository)
    {
      _repository = repository;
    }

    [HttpPost]
    [AllowAnonymous]
    public ActionResult Add([FromBody] User user)
    {
      Regex regex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

      if (user.EmailUser == "" || user.NameUser == "" || user.Password == "")
      {
        return BadRequest(new { message = "All fields must be passed" });
      }

      if (!regex.IsMatch(user.EmailUser)) return BadRequest(new { message = "EmailUser must be valid" });
      if (user.EmailUser.Length < 8) return BadRequest(new { message = "EmailUser must have at least 8 characters"});

      if (user.Password.Length < 8) return BadRequest(new { message = "Password must have at least 8 characters" });

      return Created("",_repository.AddUser(user));
    }

    [HttpPut("{id}")]
    [Authorize]
    public ActionResult Update([FromBody] User user, int id)
    {
      Regex regex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

      if (user.EmailUser == "" || user.NameUser == "" || user.Password == "")
      {
        return BadRequest(new { message = "All fields must be passed" });
      }

      if (!regex.IsMatch(user.EmailUser)) return BadRequest(new { message = "EmailUser must be valid" });
      if (user.EmailUser.Length < 8) return BadRequest(new{ message = "EmailUser must have at least 8 characters" });

      if (user.Password.Length < 8) return BadRequest(new { message = "Password must have at least 8 characters" });

      return Ok(_repository.UpdateUser(user, id));
    }

    [HttpGet("{id}")]
    [Authorize]
    public ActionResult GetById(int id)
    {
      var user = _repository.GetUser(id);

      if (user == null) return NotFound(new { message = "user not found" });

      return Ok(user);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public ActionResult Delete(int id)
    {
      bool isDeleted = _repository.DeleteUser(id);

      if (!isDeleted) return NotFound(new { message = "user not found" });
      
      return Ok(new { message= "user deleted"});
    }
  }
}
