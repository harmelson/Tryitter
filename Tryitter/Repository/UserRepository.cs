using Microsoft.EntityFrameworkCore;
using Tryitter.Models;

namespace Tryitter.Repository
{
  public class UserRepository
  {
    protected readonly MyContext _context;
    public UserRepository(MyContext context)
    {
      _context = context;
    }

    public bool LoginUser(UserDTO user)
    {
      var findUser = _context.Users.FirstOrDefault(
        u => u.EmailUser == user.EmailUser && u.Password == user.Password
      );

      if (findUser != null)
      {
        return true;
      }

      return false;
    }
    public User AddUser(User user)
    {
      _context.Users.Add(user);
      _context.SaveChanges();

      return new User{
        IdUser = user.IdUser,
        EmailUser = user.EmailUser,
        NameUser = user.NameUser,
        Password = user.Password,
      };
    }

    public User UpdateUser(User user, int userId)
    {
      var userUpdated = _context.Users.Find(userId);
      if (userUpdated != null)
      {
        userUpdated.EmailUser = user.EmailUser;
        userUpdated.NameUser = user.NameUser;
        userUpdated.Password = user.Password;
        _context.SaveChanges();
      }

      return new User{
        IdUser = user.IdUser,
        EmailUser = user.EmailUser,
        NameUser = user.NameUser,
        Password = user.Password,
      };
    }

    public User GetUser(int userId)
    {
      var user = _context.Users.FirstOrDefault(u => u.IdUser == userId);

      if (user == null) return null!;

      return user;
    }

    public User GetUserByEmail(string email)
    {
      var user = _context.Users.FirstOrDefault(u => u.EmailUser == email);

      if (user == null) return null!;

      return user;
    }

    public bool DeleteUser(int userId)
    {
      var user = _context.Users.FirstOrDefault(u => u.IdUser == userId);

      if (user == null) return false;

      _context.Users.Remove(user);
      _context.SaveChanges();
      return true;
    }
  }
}
