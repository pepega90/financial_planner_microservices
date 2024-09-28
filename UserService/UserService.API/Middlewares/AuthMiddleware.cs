using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace UserService.API.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _secret;

        public AuthMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _secret = configuration["Jwt:Secret"];
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                if (!AttachUserToContext(context, token))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsJsonAsync(new { message = "Unauthorized" });
                    return;
                }
            } else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new { message = "Unauthorized" });
                return;
            }
            await _next(context);
        }

        private bool AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = "UserService.Issuer",
                    ValidateAudience = true,
                    ValidAudience = "UserService.Audience",
                    ClockSkew = TimeSpan.Zero,
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userIdClaim = jwtToken.Claims.FirstOrDefault(e => e.Type == JwtRegisteredClaimNames.NameId);
                if (userIdClaim == null)
                {
                    Console.WriteLine("Nameidentifier claim not found in token");
                    return false;
                }
                var userId = userIdClaim.Value;
                context.Items["userId"] = userId;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while validating token: {ex.Message}");
            }
            return false;
        }
    }
}