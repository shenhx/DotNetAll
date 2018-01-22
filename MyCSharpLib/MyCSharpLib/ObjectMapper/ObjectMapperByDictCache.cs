using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class ObjectMapperByDictCache
    {
        //第四种：使用Lambda表达树
        private static Dictionary<string, object> _Dic = new Dictionary<string, object>();
        public TOut TransByCopyObject<TIn, TOut>(TIn objTIn)
        {
            string key = string.Format("trans_exp_{0}_{1}", typeof(TIn).FullName, typeof(TOut).FullName);
            if (!_Dic.Keys.Contains(key))
            {
                ParameterExpression parameterExpression = Expression.Parameter(typeof(TIn), "p"); ;
                List<MemberBinding> memberBindingList = new List<MemberBinding>();
                foreach (var item in typeof(TOut).GetProperties())
                {
                    if (!item.CanWrite)
                    {
                        continue;
                    }
                    var memberBinding = Expression.Bind(item, Expression.Property(parameterExpression, typeof(TIn).GetProperty(item.Name)));
                    memberBindingList.Add(memberBinding);
                }
                Expression<Func<TIn, TOut>> expression = Expression.Lambda<Func<TIn, TOut>>(Expression.MemberInit(Expression.New(typeof(TOut)), memberBindingList.ToArray()), new ParameterExpression[]
                {
                    parameterExpression
                });
                Func<TIn, TOut> func = expression.Compile();
                _Dic[key] = func;
            }
            //return default(TOut);
            return (_Dic[key] as Func<TIn,TOut>)(objTIn);
        }
        
    }
}
