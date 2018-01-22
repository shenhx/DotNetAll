using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class ObjectMapperGenericType<TIn, TOut> 
    {
        //第五种：利用泛型优化第四种办法
        #region 
        private static readonly Func<TIn, TOut> cache = GetFunc();

        /// <summary>
        /// todo:需要看懂lambda表达树的构造
        /// </summary>
        /// <returns></returns>
        private static Func<TIn, TOut> GetFunc()
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TIn), "p");
            List<MemberBinding> memberBindingList = new List<MemberBinding>();
            foreach (var item in typeof(TOut).GetProperties())
            {
                if (!item.CanWrite)
                {
                    continue;
                }
                MemberExpression property = Expression.Property(parameterExpression, typeof(TIn).GetProperty(item.Name));
                MemberBinding memberBinding = Expression.Bind(item, property);
                memberBindingList.Add(memberBinding);
            }
            Expression<Func<TIn, TOut>> lambdaExpression = Expression.Lambda<Func<TIn, TOut>>(
                Expression.MemberInit(Expression.New(typeof(TOut)), memberBindingList.ToArray()), 
                new ParameterExpression[]
                {
                    parameterExpression
                });
            return lambdaExpression.Compile();
        }

        public TOut TransByGenericObject(TIn objTIn)
        {
            return cache(objTIn);
        }
        #endregion
    }
}
