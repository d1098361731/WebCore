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
    [AutoMap(typeof(User))]
    public class UserOutPut
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>

        public string Name { get; set; }
   
        /// <summary>
        /// 状态
        /// </summary>

        public int State { get; set; }
        /// <summary>
        /// 地址
        /// </summary>

        public string Address { get; set; }
        /// <summary>
        /// 备注
        /// </summary>

        public string Remarks { get; set; }
        /// <summary>
        /// 公司
        /// </summary>
        public string Company { get; set; }

        public  string Token { get; set; }
    }
}
