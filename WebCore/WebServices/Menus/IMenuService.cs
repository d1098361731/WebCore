using CoreServices.Menus.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServices;

namespace CoreServices.Users
{
    public interface IMenuService : IService
    {
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<MenuOutPut>> GetMenusAsync();

    }
}
