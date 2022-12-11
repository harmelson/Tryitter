using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tryitter.Repository;
using Tryitter.Models;

namespace Tryitter.Controllers
{
  [ApiController]
  [Route("post")]
  public class PostController : Controller
  {
    private readonly PostRepository _repository;
    public PostController(PostRepository repository)
    {
      _repository = repository;
    }

    [HttpPost]
    [AllowAnonymous]
    public ActionResult Add([FromBody] PostDTO post)
    {
      if (post.MessagePost.Length < 1 || post.MessagePost.Length > 280) return BadRequest("message post must have at least 1 and less than 280 characteres");

      return Created("",_repository.AddPost(post));
    }
  }
}
