using Common.CoreToken.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;

namespace Common.CoreToken
{
    public class TokenHelper
    {

        private static int overtime = 12;
        private static string secret = "defaultSecret";
        private static  string issuer = "CoreDff";
        private static string audience = "Audience";
        static  TokenHelper()
        {
            var overtimeStr = AppSettingsHelpper.Configuration.GetSection("AppSettings:Token:overtime").Value;
            if (!int.TryParse(overtimeStr, out overtime))
                overtime = 12;
            secret = AppSettingsHelpper.Configuration.GetSection("AppSettings:Token:secret").Value;
            issuer = AppSettingsHelpper.Configuration.GetSection("AppSettings:Token:issuer").Value;
            audience = AppSettingsHelpper.Configuration.GetSection("AppSettings:Token:audience").Value;
        }
        /// <summary>
        /// 获取JWT字符串并存入缓存
        /// </summary>
        /// <param name="tm"></param>
        /// <param name="expireSliding"></param>
        /// <param name="expireAbsoulte"></param>
        /// <returns></returns>
        public static string IssueJWT(TokenModel tokenModel)
        {
            DateTime UTC = DateTime.UtcNow;
            Claim[] claims = new Claim[]
            {
              //发布者
                new Claim(JwtRegisteredClaimNames.Iss, issuer),
                 //发布者
                new Claim(JwtRegisteredClaimNames.Aud, audience),
                //主题/身份证,
                new Claim(JwtRegisteredClaimNames.Sub,tokenModel.Sub),
                //JWT ID,JWT的唯一标识
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //JWT颁发的时间，采用标准unix时间，用于验证过期
                new Claim(JwtRegisteredClaimNames.Iat, UTC.ToString(), ClaimValueTypes.Integer64),
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = audience,//jwt的接收该方
                Issuer = issuer,//jwt签发者
                Expires = UTC.AddHours(overtime),//指定token的生命周期，unix时间戳格式
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)), 
                SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(claims)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            //生成最后的JWT字符串
            var encodedJwt = tokenHandler.WriteToken(token);  
            return encodedJwt;
        }

        public static TokenModel GetToken(string encodedJwt)
        {
            TokenModel model = new TokenModel();
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadToken(encodedJwt);
           
            return null;
        }
    }
}
