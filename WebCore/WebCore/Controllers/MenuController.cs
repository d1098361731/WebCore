using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreServices.Menus.Dtos;
using CoreServices.Users;
using CoreServices.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebCore.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            this._menuService = menuService;
        }
        /// <summary>
        /// 根据条件获取到菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("get_menus")]
        public  async  Task<List<MenuOutPut>>  GetMenusAsync()
        {
            return await _menuService.GetMenusAsync();
        }
    }
}