namespace Tryitter.Models
{
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Text.Json.Serialization;
  public class User
  {
    [Key, JsonIgnore]
    public int IdUser { get; set; }
    public string EmailUser  { get; set; } = null!;
    public string NameUser  { get; set; } = null!;
    public string Password  { get; set; } = null!;
    [InverseProperty("User"), JsonIgnore]
    public ICollection<PostUser>? PostUsers  { get; set; } = null!;
  }

  public class UserDTO
  {
    public string EmailUser  { get; set; } = null!;
    public string Password  { get; set; } = null!;
  }
}
