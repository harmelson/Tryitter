namespace Tryitter.Models {
  using System.ComponentModel.DataAnnotations;
  public class PostUser
  {
    [Key]
    public int IdUser { get; set; }
    [Key]
    public int IdPost { get; set; }
  }
}
