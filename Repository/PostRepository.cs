using Microsoft.EntityFrameworkCore;
using Tryitter.Models;

namespace Tryitter.Repository
{
  public class PostRepository
  {
    protected readonly MyContext _context;
    public PostRepository(MyContext context)
    {
      _context = context;
    }

    public Post AddPost(PostDTO post)
    {
      _context.Posts.Add( new Post {
        // IdPost = _context.Posts.Last().IdPost +=1,
        MessagePost = post.MessagePost
      });

      var lastPost = _context.Posts.Last();

      return lastPost;
      // _context.Users.Add(user);
      // _context.SaveChanges();
      // return new User{
      //   IdUser = user.IdUser,
      //   EmailUser = user.EmailUser,
      //   NameUser = user.NameUser,
      //   Password = user.Password,
      // };
    }
  }
}
