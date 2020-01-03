using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Attribute
{
    /// <summary>
    /// 是否开启事务
    /// </summary>
    [AttributeUsage(AttributeTargets.Method,Inherited =true)]
    public class UnitWorkAttribute:System.Attribute  
    {
        /// <summary>
        /// 是否开启事务，默认开启
        /// </summary>
        public bool Tran { get; set; } = true;
        public UnitWorkAttribute(bool tran)
        {
            Tran = tran;
        }
    }
}
