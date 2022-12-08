namespace Tryitter.Models {
  using System.ComponentModel.DataAnnotations;
  public class Post
  {
    [Key]
    public int IdPost { get; set; }
    public string MessagePost  { get; set; }
    public int LikesPost  { get; set; }
    public int SharesPost  { get; set; }
  }
}
