using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AopConsoleApp
{
    /// <summary>
    /// IDataObject 代理；实现对 IDataObject 操作的拦截；
    /// </summary>
    public class DataObjectProxy : IDataObject
    {
        private IDataObject _realObject;
        public DataObjectProxy(IDataObject realObject)
        {
            _realObject = realObject;
        }

        /// <summary>
        /// 拦截对 Compute 方法的调用；
        /// </summary>
        /// <returns></returns>
        public int Compute()
        {
            DoSomethingBeforeCompute();

            int result = _realObject.Compute();

            DoSomethingAfterCompute(result);

            return result;
        }

        private void DoSomethingAfterCompute(int result)
        {
            Console.WriteLine("After Compute " + _realObject.ToString() + ". Result=" + result);
        }

        private void DoSomethingBeforeCompute()
        {
            Console.WriteLine("Before Compute " + _realObject.ToString());
        }
    }
}
