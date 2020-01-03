using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.AOP
{
    /// <summary>
    /// 自定义log拦截器
    /// </summary>
    public class CoreLogAOP : IInterceptor
    {
        /// <summary>
        /// 实例化IInterceptor唯一方法 
        /// </summary>
        /// <param name="invocation">被拦截方法的所有信息</param>
        public void Intercept(IInvocation invocation)
        {
            string strRqu = "执行时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")
                + ";执行方法：" + invocation.TargetType.Name + "." + invocation.Method.Name
                + "(" + string.Join(",", invocation.Method.GetParameters().Select(x => x ?.Name).ToArray())+")";
            // +";参数值：" + string.Join(",", invocation.Arguments.Select(x=>(x ?? "").ToString()).ToArray());

            try
            {
                invocation.Proceed();
            }catch(Exception ex)
            {
                strRqu += $";【执行错误，错误原因：{ex.Message},错误行：{ex.StackTrace}】";
            }
           // strRqu += $";执行完毕，返回结果：{invocation.ReturnValue}";

            #region 输出到当前项目日志
            var path = Directory.GetCurrentDirectory() + @"\Log";
            string fileName = path + $@"\Log-{DateTime.Now.ToString("yyyyMMdd")}.log";
            StreamWriter sw = File.AppendText(fileName);
            sw.WriteLine(strRqu);
            sw.Close();
            #endregion
        }
    }
}
