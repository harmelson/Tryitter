namespace Tryitter.Models {
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  public class User
  {
    [Key]
    public int IdUser { get; set; }
    public string EmailUser  { get; set; } = null!;
    public string NameUser  { get; set; } = null!;
    public string Password  { get; set; } = null!;
    [InverseProperty("User")]
    public ICollection<PostUser> PostUsers  { get; set; } = null!;
  }
}
