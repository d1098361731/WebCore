using CoreServices.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServices;

namespace CoreServices.Users
{
    public interface IUserService:IService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
         Task<UserOutPut> LoginAsync(LoginInput input);

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<UserOutPut> TokenLoginAsync(string token);
    }
}
