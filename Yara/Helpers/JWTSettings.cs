using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Yara.Helpers;

public static class JWTSettings
{
	public static void GenerateToken(IServiceCollection services, IConfiguration configuration)
	{
		var key = Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]);

		services.AddAuthentication(x =>
		{
			x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
		.AddJwtBearer(x =>
		{
			x.RequireHttpsMetadata = false;
			x.SaveToken = true;
			x.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ValidateIssuer = false,
				ValidateAudience = false
			};
		});

	}
}
