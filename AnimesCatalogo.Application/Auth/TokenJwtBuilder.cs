using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebApi.Application.Auth
{
    public class TokenJwtBuilder
    {
        private SecurityKey securityKey = null;
        private string subject = "";
        private string issuer = "";
        private string audience = "";
        private Dictionary<string, string> claims = new Dictionary<string, string>();
        private int expiryInMinutes = 10;

        public TokenJwtBuilder AddSecurityKey(SecurityKey securityKey)
        {
            this.securityKey = securityKey;
            return this;
        }
        public TokenJwtBuilder AddSubject(string subject)
        {
            this.subject = subject;
            return this;
        }
        public TokenJwtBuilder AddIssuer(string issuer)
        {
            this.issuer = issuer;
            return this;
        }
        public TokenJwtBuilder AddAudience(string audience)
        {
            this.audience = audience;
            return this;
        }
        public TokenJwtBuilder AddClaims(Dictionary<string, string> claims)
        {
            this.claims = claims;
            return this;
        }
        public TokenJwtBuilder AddExpiryInMinutes(int expiryInMinutes)
        {
            this.expiryInMinutes = expiryInMinutes;
            return this;
        }

        private void EnsureArguments()
        {
            if (securityKey == null)
                throw new ArgumentNullException("Security Key");

            if (string.IsNullOrEmpty(subject))
                throw new ArgumentNullException("Subject");

            if (string.IsNullOrEmpty(issuer))
                throw new ArgumentNullException("Issuer");

            if (string.IsNullOrEmpty(audience))
                throw new ArgumentNullException("Audience");
        }

        public TokenJwt Builder()
        {
            EnsureArguments();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }.Union(this.claims.Select(item => new Claim(item.Key, item.Value)));

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                signingCredentials: new SigningCredentials(
                    securityKey,
                    SecurityAlgorithms.Aes128CbcHmacSha256)
                );
            return new TokenJwt(token);
        }


    }
}
