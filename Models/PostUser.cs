namespace Tryitter.Models
{
  using Microsoft.EntityFrameworkCore;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  [PrimaryKey(nameof(IdUser), nameof(IdPost))]
  public class PostUser
  {
    [ForeignKey("IdUser")]
    public int IdUser { get; set; }
    public User User { get; set; } = null!;
    [ForeignKey("IdPost")]
    public int IdPost { get; set; }
    public ICollection<Post> Post { get; set; } = null!;
  }
}
