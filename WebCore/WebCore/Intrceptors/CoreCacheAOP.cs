using Castle.DynamicProxy;
using Common.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.Intrceptors
{
    /// <summary>
    /// 缓存拦截器
    /// </summary>
    public class CoreCacheAOP : IInterceptor
    {
        private readonly ICache _cache;
        public CoreCacheAOP(ICache cache)
        {
            this._cache = cache;
        }
        public void Intercept(IInvocation invocation)
        {
            string key = CustomCacheKey(invocation);
            if (!string.IsNullOrEmpty(key))
            {
               var  value =  _cache.Get(key);
                if (value != null)
                {
                    invocation.ReturnValue = value;
                    return;
                }
                invocation.Proceed();
                _cache.Set(key, invocation.ReturnValue);
            }
        }

        //自定义缓存键
        private string CustomCacheKey(IInvocation invocation)
        {
            var typeName = invocation.TargetType.Name;
            var methodName = invocation.Method.Name;
            var methodArguments = invocation.Arguments.Select(GetArgumentValue).Take(3).ToList();//获取参数列表，我最多需要三个即可

            string key = $"{typeName}:{methodName}:";
            foreach (var param in methodArguments)
            {
                key += $"{param}:";
            }

            return key.TrimEnd(':');
        }
        //object 转 string
        private string GetArgumentValue(object arg)
        {
            if (arg is int || arg is long || arg is string)
                return arg.ToString();

            if (arg is DateTime)
                return ((DateTime)arg).ToString("yyyyMMddHHmmss");

            return "";
        }
    }
}
