using System.IdentityModel.Tokens.Jwt;

namespace WebApi.Application.Auth
{
    public class TokenJwt
    {
        private JwtSecurityToken _jwtSecurityKey;

        internal TokenJwt(JwtSecurityToken jwtSecurityKey)
        {
            _jwtSecurityKey = jwtSecurityKey;
        }

        public DateTime ValidTo => _jwtSecurityKey.ValidTo;

        public string value => new JwtSecurityTokenHandler().WriteToken(_jwtSecurityKey);
    }
}
