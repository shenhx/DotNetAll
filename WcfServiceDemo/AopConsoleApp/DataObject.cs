using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopConsoleApp
{
    /// <summary>
    /// 数据对象；
    /// </summary>
    public class DataObject : IDataObject
    {
        private int _parameter1;
        private int _parameter2;

        /// <summary>
        /// 私有构造函数，封装DataObject的实例化过程；
        /// </summary>
        private DataObject()
        {
        }

        /// <summary>
        /// 创建数据对象；
        /// </summary>
        public static IDataObject CreateDataObject(int p1, int p2)
        {
            //创建真实实例；
            DataObject realObject = new DataObject();
            realObject._parameter1 = p1;
            realObject._parameter2 = p2;

            //返回代理；
            return new DataObjectProxy(realObject);
        }

        /// <summary>
        /// 创建数据对象；
        /// </summary>
        public static IDataObject CreateDataObjec2(int p1, int p2)
        {
            //创建真实实例；
            DataObject realObject = new DataObject();
            realObject._parameter1 = p1;
            realObject._parameter2 = p2;

            //创建真实代理；
            AopProxy<IDataObject> proxy = new AopProxy<IDataObject>(realObject);

            //返回透明代理；
            return (IDataObject)proxy.GetTransparentProxy();
        }

        /// <summary>
        /// “计算”的实现；
        /// </summary>
        /// <returns></returns>
        public int Compute()
        {
            Console.WriteLine("compute...");
            return -1;
        }
    }
}
