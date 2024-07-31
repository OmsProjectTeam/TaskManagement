using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Configuration;

namespace LamarModa.Api.Auth;
public interface ITokenService
{
	string GenerateToken(ApplicationUser user, IList<string> roles);
}

public class TokenService : ITokenService
{
	private readonly IConfiguration _configuration;

	public TokenService(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public string GenerateToken(ApplicationUser user, IList<string> roles)
	{

		var claims = new List<Claim>
		{
			new(ClaimTypes.Email, user.Email),
			new(ClaimTypes.NameIdentifier, user.Id),
			new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
		};

		claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			issuer: _configuration["JWT:Issuer"],
			audience: _configuration["JWT:Audience"],
			claims: claims,
			expires: DateTime.Now.AddHours(1),
			signingCredentials: creds
		); ;

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}