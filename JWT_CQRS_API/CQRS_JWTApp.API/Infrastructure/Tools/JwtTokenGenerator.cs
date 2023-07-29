﻿using CQRS_JWTApp.API.Core.Application.Dto;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CQRS_JWTApp.API.Infrastructure.Tools
{
    public class JwtTokenGenerator
    {
        public static TokenResponseDto GenerateToken(CheckUserResponseDto checkUserResponseDto)
        {
            List<Claim> claims = new();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, checkUserResponseDto.Id.ToString()));
            if (!string.IsNullOrEmpty(checkUserResponseDto.Role))
                claims.Add(new Claim(ClaimTypes.Role, checkUserResponseDto.Role));
            if (!string.IsNullOrEmpty(checkUserResponseDto.Username))
                claims.Add(new Claim("Username", checkUserResponseDto.Username));

            DateTime expireDate = DateTime.UtcNow.AddMinutes(JwtTokenDefaults.Expire);

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key));

            SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new
                (
                issuer: JwtTokenDefaults.ValidIssuer,
                audience: JwtTokenDefaults.ValidAudience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expireDate,
                signingCredentials: credentials
                );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            return new TokenResponseDto(handler.WriteToken(jwtSecurityToken), expireDate);
        }
    }
}
