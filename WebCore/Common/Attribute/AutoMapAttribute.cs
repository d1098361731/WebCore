using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Attribute
{
    /// <summary>
    /// 双向映射
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class AutoMapAttribute : System.Attribute
    {
        public Type Type { get; set; }

        internal virtual AutoMapDirection Direciton
        {
            get { return AutoMapDirection.From | AutoMapDirection.To; }
        }


        public AutoMapAttribute(Type type)
        {
            Type = type;
        }
    }

    /// <summary>
    /// 反向映射
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class AutoMapFormAttribute : System.Attribute
    {
        public Type Type { get; set; }

        internal virtual AutoMapDirection Direciton
        {
            get { return AutoMapDirection.From; }
        }

        public AutoMapFormAttribute( Type type)
        {
            Type = type;
        }

    }

    /// <summary>
    ///  正向映射
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class AutoMapToAttribute : System.Attribute
    {
        public Type Type { get; set; }

        internal virtual AutoMapDirection Direciton
        {
            get { return AutoMapDirection.From | AutoMapDirection.To; }
        }


        public AutoMapToAttribute( Type type)
        {
            Type = type;
        }
    }


    public enum AutoMapDirection
    {
        From = 1,
        To = 2,
    }
}
