using Tryitter.Constants;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Tryitter.Models;

namespace Tryitter.Services 
{
  public class TokenGenerator
  {
    public string Generate(User user)
    {
      JwtSecurityTokenHandler tokenHandler = new();
      SecurityTokenDescriptor tokenDescriptor = new()
      {
        Subject = new ClaimsIdentity(new Claim[]
        {
          new Claim(ClaimTypes.Email, user.EmailUser.ToString()),
          new Claim(ClaimTypes.Name, user.NameUser.ToString()),
          new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
        }),
        SigningCredentials = new SigningCredentials(
          new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenConstants.Secret)),
          SecurityAlgorithms.HmacSha256Signature
        ),
        Expires = DateTime.Now.AddDays(1)
      };

      SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(token);
    }
    
    public JwtPayload Decode(string token)
    {
      var key = Encoding.ASCII.GetBytes(TokenConstants.Secret);
      var handler = new JwtSecurityTokenHandler();
      var validations = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
        
      handler.ValidateToken(token, validations, out var tokenSecure);
      
      var decodedValue = handler.ReadJwtToken(token);
      JwtPayload payload = decodedValue.Payload;

      return payload;
    }
  }
}
