﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebApi.Entities;
using WebApi.TokenOperations.Models;

namespace WebApi.TokenOperations
{
    public class TokenHandler
    {
        public IConfiguration Configuration { get; set; }
        public TokenHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Token CreateAccessToken(User user)
        {
            Token tokenModel = new Token();
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));

            SigningCredentials credentials= new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            tokenModel.ExpireDate = DateTime.Now.AddMinutes(15);

            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer : Configuration["Token:Issuer"],
                audience : Configuration["Token:Audience"],
                expires : tokenModel.ExpireDate,
                notBefore: DateTime.Now,
                signingCredentials: credentials
                );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            tokenModel.AccessToken = tokenHandler.WriteToken( securityToken );
            tokenModel.RefreshToken = CreateRefreshToken();

            return tokenModel;
            
        }

        public string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
