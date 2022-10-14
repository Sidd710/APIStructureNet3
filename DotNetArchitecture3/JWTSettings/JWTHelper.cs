using System.Collections.Generic;
using System.Security.Claims;
using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using DotNetArchitecture3.CustomModel;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using DotNetArchitecture3.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Drawing;

namespace DotNetArchitecture3.JWTSettings
{
    public class JWTHelper : IJWTHelper
    {
        private readonly IConfiguration _iconfigration;
        private readonly ElectionCampaignContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JWTHelper(IConfiguration iconfigration,ElectionCampaignContext context, IHttpContextAccessor httpContextAccessor)
        {
            _iconfigration = iconfigration;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public string Authentication(string UserName, string Password)
        {
            var Userdetails = _context.UserDetails.Where(x=>x.Mobile==UserName).ToList().FirstOrDefault();

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iconfigration["JWT:key"]));

            var claims = new ClaimsIdentity(new Claim[]
                    {
                           new Claim(ClaimTypes.Name,Userdetails.Name),
                           new Claim(ClaimTypes.Email,Userdetails.Mobile),
                           new Claim("UserId",Userdetails.UserId.ToString()),
                           new Claim("AadharCard",Userdetails.AadharCard)

                    });

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddDays(365),
                SigningCredentials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);


            //var authClaims = new Claim[]
            //{
            //               new Claim(ClaimTypes.Name,Userdetails.Name),
            //               new Claim(ClaimTypes.Email,Userdetails.Mobile),
            //               new Claim("UserId",Userdetails.UserId.ToString()),
            //               new Claim("AadharCard",Userdetails.AadharCard)
            //};


            //var authSignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iconfigration["JWT:key"]));

            //var token = new JwtSecurityToken
            //(
            //    issuer: _iconfigration["JWT:Issuer"],
            //    audience: _iconfigration["JWT:Audience"],
            //    expires: DateTime.Now.AddHours(24),
            //    claims: authClaims,
            //    signingCredentials: new SigningCredentials(authSignInKey, SecurityAlgorithms.HmacSha256)
            //);
            //return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public string GetUserDetails()
        {
                

            //var GetclaimsUser = _httpContextAccessor.HttpContext.User;

            //var identity = GetclaimsUser.Identity as ClaimsIdentity;

            //foreach (var claim in identity.Claims)
            //{
            //    if (claim.Type.Contains("EmailAddress"))
            //    {
            //        var EmailName = claim.Value;
            //        return EmailName;
            //    }
            //}

            //return null; 

            var GetclaimsUser = _httpContextAccessor.HttpContext.User;

            var identity = GetclaimsUser.Identities as ClaimsIdentity;

            //var i = identity[0].            // Gets list of claims.

            List<Claim> claim = GetclaimsUser.Claims.ToList();

            // Gets name from claims. Generally it's an email address.

            string userId = claim.Where(x => x.Type == "UserId").FirstOrDefault().Value;

            return userId;
        }


    }
}
