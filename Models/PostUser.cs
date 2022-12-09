namespace Tryitter.Models
{
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  public class PostUser
  {
    [Key]
    public int IdPostUser { get; set; }
    public int IdUser { get; set; }
    [ForeignKey("IdUser")]
    public User User { get; set; } = null!;
    [ForeignKey("IdPost")]
    public int IdPost { get; set; }
    public ICollection<Post> Post { get; set; } = null!;
  }
}
