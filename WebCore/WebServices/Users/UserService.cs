using Common.Cache;
using Common.CoreToken;
using Common.CoreToken.Model;
using Core.Model;
using CoreRepository.EF;
using CoreServices.Users.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using WebServices;

namespace CoreServices.Users
{
    public  class UserService : BaseService,IUserService
    {
        private readonly ICache _cache;
        private readonly IEFRepository<User, Guid> _userRepository;
        public UserService(ICache cache, IEFRepository<User, Guid> userRepository)
        {
            this._cache = cache;
            this._userRepository = userRepository;
        }
        public async Task<UserOutPut> LoginAsync(LoginInput input)
        {
            var model = await _userRepository.FirstOrDefaultAsync(x => x.Name == input.Name && x.Password == input.Password);
            if (model != null)
            {
                var outPut = Mapper.Map<UserOutPut>(model);
                var token = new TokenModel
                {
                    Sub = "Admin",
                    Uname = model.Name,
                    Uid = new Random().Next(),
                    UNickname = model.Name,
                };
                //获取Token
                string TokenStr = TokenHelper.IssueJWT(token);
                outPut.Token = TokenStr;
                this._cache.Set<UserOutPut>(TokenStr, outPut);
                return outPut;
            }
            return null;
        }
        public async Task<UserOutPut> TokenLoginAsync(string token)
        {

            //var output = await Task.Run<UserOutPut>(() =>
            //{
            //    var model = this._cache.Get<UserOutPut>(token);
            //    if (model == null)
            //        model = new UserOutPut();
            //    return model;
            //});

            var output  = await  Task.Run<UserOutPut>(() => this._cache.Get<UserOutPut>(token));
            return output;
        }
    }
}
