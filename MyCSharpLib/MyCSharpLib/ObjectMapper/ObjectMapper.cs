using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MyCSharpLib
{
    /// <summary>
    /// 使用场景：
    /// 1.经常有不同对象，但是有相同的属性需要相互赋值的时候，可以参考以下的方法。
    /// </summary>
    public class ObjectMapper<TIn,TOut>
    {
        //第一种：直接赋值属性

        //第二种：使用反射
        public TOut ReflectionObject(TIn objTIn)
        {
            TOut objOut = Activator.CreateInstance<TOut>();
            var inType = objTIn.GetType();
            foreach (var itemOut in objOut.GetType().GetProperties())
            {
                var itemIn = inType.GetProperty(itemOut.Name);
                if(itemIn != null)
                {
                    itemOut.SetValue(objOut,itemIn.GetValue(objTIn));
                }
            }
            return objOut;
        }

        //第三种：使用序列化和反序列化
        public TOut SerializeObject(TIn objTIn)
        {
            return JsonConvert.DeserializeObject<TOut>(JsonConvert.SerializeObject(objTIn));
        }
        
    }
}
