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

        var post = _context.Post.Add( new Post {
          IdPost = postIdGetter +=1,
          MessagePost = messagePost.MessagePost
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
    public PostWithIdUserDTO GetPost(int idPost)
    {
      var post = _context.Post.Include(p => p.PostUser).FirstOrDefault(p => p.IdPost == idPost);

      return post == null
      ? null!
      : new PostWithIdUserDTO {
        IdUser = post.PostUser.IdUser,
        MessagePost = post.MessagePost,
        LikesPost = post.LikesPost,
        SharesPost = post.SharesPost,
      };
    }

    public Post UpdatePost(int idPost, PostDTO messagePost, string token)
    {
        var tokenHandler = new TokenGenerator();
        int userId = Convert.ToInt32(tokenHandler.Decode(token)["nameid"]);

        var post = _context.Post.Include(p => p.PostUser).FirstOrDefault(p => p.IdPost == idPost);

        if (post == null) return null!;
        if (post!.PostUser.IdUser != userId) return null!;

        post.MessagePost = messagePost.MessagePost;
        _context.SaveChanges();

        return post;
    }

    public bool DeletePost(int idPost, string token)
    {
      using var transaction = _context.Database.BeginTransaction();
      try
      {
        var tokenHandler = new TokenGenerator();
        int userId = Convert.ToInt32(tokenHandler.Decode(token)["nameid"]);

        var post = _context.Post.Include(p => p.PostUser).FirstOrDefault(p => p.IdPost == idPost);
        if (post == null) return false;
        if (post!.PostUser.IdUser != userId) return false;

        PostUser postUser = _context.PostUser.First(p => p.IdUser == userId);

        _context.Post.Remove(post);
        _context.PostUser.Remove(postUser);
        
        _context.SaveChanges();
        transaction.Commit();

        return true;
        
      }
      catch (DbUpdateException exception)
      {
        transaction.Rollback();
        throw exception;
      }
    }
  
    public PostWithIdUserDTO[] GetAllPostsByUserId(int idUser)
    {
      var posts = _context.Post.Where(p => p.PostUser.IdUser == idUser).ToArray();

      if (posts.Length == 0) return null!;

      PostWithIdUserDTO[] postWithIdUserDTO = new PostWithIdUserDTO[posts.Length];
      for (int i = 0; i < posts.Length; i++)
      {
        postWithIdUserDTO[i] = new PostWithIdUserDTO{
          IdUser = idUser,
          LikesPost = posts[i].LikesPost,
          MessagePost = posts[i].MessagePost,
          SharesPost = posts[i].SharesPost
        };
      }

      return postWithIdUserDTO;
    }
  }

}
