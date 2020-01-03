using Common.Cache;
using Common.CoreToken;
using Common.CoreToken.Model;
using Core.Model;
using CoreRepository.EF;
using CoreServices.Menus.Dtos;
using CoreServices.Users.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServices;

namespace CoreServices.Users
{
    public  class MenuService : BaseService, IMenuService
    {
        private readonly ICache _cache;
        private readonly IEFRepository<Menu, Guid> _menuRepository;
        public MenuService(ICache cache, IEFRepository<Menu, Guid> menuRepository)
        {
            this._cache = cache;
            this._menuRepository = menuRepository;
        }
        public async Task<List<MenuOutPut>> GetMenusAsync()
        {
            //缓存中获取，如果缓存没有则数据库获取再存入缓存
            var list = _cache.Get<List<MenuOutPut>>("menu");
            if (list==null || list.Count == 0)
            {
                var  query = _menuRepository.GetAll();
                if (query != null)
                {
                    var  models  = await query.ToListAsync();
                    list = Mapper.Map<List<MenuOutPut>>(models);
                    _cache.Set<List<MenuOutPut>>("menu", list);
                }    
            } 
            return list;
        }
        
    }
}
