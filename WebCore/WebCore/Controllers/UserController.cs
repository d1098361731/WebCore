using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Cache;
using Common.CoreToken;
using Common.CoreToken.Model;
using CoreServices.Users;
using CoreServices.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebCore.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Authorize]
    public class UserController : ControllerBase
    {
        private  readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            this._userService = userService;
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="input">登录类型</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/user/login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserOutPut>> LoginAsync (LoginInput input)
        {
            var outPut = await  this._userService.LoginAsync(input);
            if (outPut!=null)
                return Ok(outPut);
            return StatusCode(403,"登录错误！");
        }

        [HttpGet]
        [Route("api/user/tokenLogin")]
        [AllowAnonymous]
        public async Task<ActionResult<UserOutPut>> TokenLoginAsync(string tokenStr)
        {
            var outPut = await this._userService.TokenLoginAsync(tokenStr);
            if (outPut != null)
                return Ok(outPut);
            return StatusCode(403, "登录错误！");
        }
        
    }
}