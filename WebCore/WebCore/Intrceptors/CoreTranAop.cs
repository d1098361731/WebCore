using Castle.DynamicProxy;
using Common.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebEf;

namespace WebCore.Intrceptors
{
    public class CoreTranAOP : IInterceptor
    {
        private readonly EFDbContext _db;
        public CoreTranAOP(EFDbContext db)
        {
            this._db = db;
        }
        public void Intercept(IInvocation invocation)
        {
            var method = invocation.MethodInvocationTarget ?? invocation.Method;
            //获取当前方法的特性
            var unitWorkAttribute = method.GetCustomAttributes(typeof(UnitWorkAttribute), true)
                .FirstOrDefault(x => x.GetType() == typeof(UnitWorkAttribute)) as UnitWorkAttribute;
            invocation.Proceed();
            if (unitWorkAttribute == null || unitWorkAttribute.Tran == true)
                _db.SaveChanges();
        }
    }
}
