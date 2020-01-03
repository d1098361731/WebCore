using AutoMapper;
using System;

namespace WebServices
{
    public class BaseService : IService
    {

        /// <summary>
        /// 数据映射
        /// </summary>
        protected  IMapper Mapper {get;set;}
        public BaseService()
        {
            this.Mapper  = AutoMapper.Mapper.Instance;
        }
    }
}
