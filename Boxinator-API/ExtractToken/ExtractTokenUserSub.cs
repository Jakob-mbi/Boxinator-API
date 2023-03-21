using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace Boxinator_API.ExtractToken
{
    public class GetSubClaimFromToken
    {

        public async Task<string>  ExtractTokenUserSub(string token, IConfiguration configuration)
        {
            string subClaim = "";


            // Extract the JWT token from the Authorization header
            var jwtToken = token.ToString().Substring("Bearer ".Length).Trim();

            // Retrieve the JsonWebKeySet from Keycloak instance
            var client = new HttpClient();
            var keyUri = configuration["JWT:key-uri"];
            var response = await client.GetAsync(keyUri);
            var responseString = await response.Content.ReadAsStringAsync();
            var keys = JsonConvert.DeserializeObject<JsonWebKeySet>(responseString);

            // Get the kid claim from the JWT token's header
            var handler = new JwtSecurityTokenHandler();
            var jwtHeader = handler.ReadJwtToken(jwtToken).Header;
            var kid = jwtHeader.Kid;

            // Find the appropriate key from the JsonWebKeySet based on the kid claim
            var key = keys.Keys.FirstOrDefault(k => k.Kid == kid);

            if (key == null)
            {
                throw new Exception("The key is null");
            }

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = true,
                ValidIssuer = configuration["JWT:issuer"],
                ValidateAudience = true,
                ValidAudience = configuration["JWT:audience"]
            };

            SecurityToken validatedToken;
            handler = new JwtSecurityTokenHandler();
            var claimsPrincipal = handler.ValidateToken(jwtToken, validationParameters, out validatedToken);

            // Get the sub claim from the JWT token
             subClaim = claimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

            if (subClaim != null)
            {
                return subClaim;
            }
            throw new Exception("The key is null");

          
        }
    }
}
    