namespace Tryitter.Models
{
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  public class Post
  {
    [Key]
    public int IdPost { get; set; }
    [MinLength(1), MaxLength(280)]
    public string MessagePost  { get; set; } = null!;
    public int LikesPost  { get; set; }
    public int SharesPost  { get; set; }
    [InverseProperty("Post")]
    public PostUser PostUsers  { get; set; } = null!;
  }
}
