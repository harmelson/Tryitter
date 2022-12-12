using Microsoft.EntityFrameworkCore;
using Tryitter.Models;
using Tryitter.Services;

namespace Tryitter.Repository
{
  public class PostRepository
  {
    protected readonly MyContext _context;
    public PostRepository(MyContext context)
    {
      _context = context;
    }

    public Post AddPost(PostDTO messagePost, string token)
    {
      using var transaction = _context.Database.BeginTransaction();
      try
      {
        var tokenHandler = new TokenGenerator();
        int userId = Convert.ToInt32(tokenHandler.Decode(token)["nameid"]);

        var postIdGetter = !_context.Post.Any() ? 0 : Convert.ToInt32(_context.Post.OrderBy(c => c.IdPost).Last().IdPost);

        Console.WriteLine($"user id:{userId}");

        var post = _context.Post.Add( new Post {
          IdPost = postIdGetter +=1,
          MessagePost = messagePost.MessagePost,
          LikesPost = 0,
          SharesPost = 0
        });

        _context.PostUser.Add( new PostUser {
          IdPost = Convert.ToInt32(post.Property("IdPost").CurrentValue),
          IdUser = userId
        });

        postIdGetter +=1;

        _context.SaveChanges();
        transaction.Commit();

        var lastPost = _context.Post.OrderBy(c => c.IdPost).Last();

        return lastPost;
      }
      catch (DbUpdateException exception)
      {
        transaction.Rollback();
        throw exception;
      }
      
    }
  }
}
