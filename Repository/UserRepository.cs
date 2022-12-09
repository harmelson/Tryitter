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
  }
}