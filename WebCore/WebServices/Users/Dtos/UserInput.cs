using Common.Attribute;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreServices.Users.Dtos
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class LoginInput
    {
        /// <summary>
        /// 用户名
        /// </summary>

        public string Name { get; set; }
        /// <summary>
        /// 密码
        /// </summary>

        public string Password { get; set; }
    }
}
