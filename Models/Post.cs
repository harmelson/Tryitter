namespace Tryitter.Models
{
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Text.Json.Serialization;
  public class Post
  {
    [Key, JsonIgnore]
    public int IdPost { get; set; }
    [MinLength(1), MaxLength(280)]
    public string MessagePost  { get; set; } = null!;
    public int LikesPost  { get; set; }
    public int SharesPost  { get; set; }
    [InverseProperty("Post"), JsonIgnore]
    public PostUser PostUsers  { get; set; } = null!;
  }
}
