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
    [Authorize]
    public ActionResult Add([FromBody] PostDTO post)
    {
      if (post.MessagePost.Length < 1 || post.MessagePost.Length > 280) return BadRequest("message post must have at least 1 and less than 280 characteres");
      var token = Request.Headers.Authorization.ToString().Split(" ")[1];

      return Created("",_repository.AddPost(post, token));
    }

    [HttpGet("{id}")]
    [Authorize]
    public ActionResult GetById(int id)
    {
      var post = _repository.GetPost(id);

      if (post == null) return NotFound("post not found");

      return Ok(post);
    }

    [HttpPatch("{id}")]
    [Authorize]
    public ActionResult Update(int id, [FromBody] PostDTO messagePost)
    {
      if (messagePost.MessagePost.Length < 1 || messagePost.MessagePost.Length > 280) return BadRequest("message post must have at least 1 and less than 280 characteres");
      var token = Request.Headers.Authorization.ToString().Split(" ")[1];
      var post = _repository.UpdatePost(id, messagePost, token);

      if(post == null) return BadRequest("Unable to change a post that was not made by the logged user");

      return Ok(post);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public ActionResult Delete(int id)
    {
      var token = Request.Headers.Authorization.ToString().Split(" ")[1];
      var post = _repository.DeletePost(id, token);

      if (post == false) return BadRequest("post not found");

      return Ok("post deleted");
    }
  }
}
