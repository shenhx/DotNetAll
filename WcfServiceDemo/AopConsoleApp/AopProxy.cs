using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;

namespace AopConsoleApp
{

    /// <summary>
    /// 通用的代理； 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AopProxy<T> : RealProxy
    {
        private T _realObject;

        public AopProxy(T realObject)
            : base(typeof(T))
        {
            _realObject = realObject;
        }

        /// <summary>
        /// 拦截所有方法的调用；
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override IMessage Invoke(IMessage msg)
        {
            IMethodCallMessage callMsg = msg as IMethodCallMessage;
            //调用前拦截；
            BeforeInvoke(callMsg.MethodBase);
            try
            {
                //调用真实方法；
                object retValue = callMsg.MethodBase.Invoke(_realObject, callMsg.Args);
                return new ReturnMessage(retValue, callMsg.Args, callMsg.ArgCount - callMsg.InArgCount, callMsg.LogicalCallContext, callMsg);
            }
            catch (Exception ex)
            {
                return new ReturnMessage(ex, callMsg);
            }
            finally
            {
                //调用后处理；
                AfterInvoke(callMsg.MethodBase);
            }
        }

        private void BeforeInvoke(MethodBase method)
        {
            Console.WriteLine("Before Invoke {0}::{1}", typeof(T).FullName, method.ToString());
        }

        private void AfterInvoke(MethodBase method)
        {
            Console.WriteLine("After Invoke {0}::{1}", typeof(T).FullName, method.ToString());
        }
    }
}
