using Tryitter.Constants;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Tryitter.Services 
{
  public class TokenGenerator
  {
    public string Generate()
    {
      JwtSecurityTokenHandler tokenHandler = new();
      SecurityTokenDescriptor tokenDescriptor = new()
      {
        SigningCredentials = new SigningCredentials(
          new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenConstants.Secret)),
          SecurityAlgorithms.HmacSha256Signature
        ),
        Expires = DateTime.Now.AddDays(1)
      };

      SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(token);
    }
  }
}
