using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly JwtSetting _jwtSetting;

        public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSetting> options)
        {
            _dateTimeProvider = dateTimeProvider;
            _jwtSetting = options.Value;
        }

        public string GenerateToken(Guid userId, string firstName, string lastName)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Secret)),
                    SecurityAlgorithms.HmacSha256
                    );

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, firstName.ToString()),
                new Claim(JwtRegisteredClaimNames.FamilyName, lastName.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var securityToken = new JwtSecurityToken(
                    issuer: _jwtSetting.Issuer,
                    expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSetting.ExpityMinutes),
                    claims: claims,
                    signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
