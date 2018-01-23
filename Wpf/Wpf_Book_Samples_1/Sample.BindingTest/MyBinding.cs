using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Sample.BindingTest
{
    public class MyBinding
    {
        //检索绑定
        public void SearchBinding<T>(DependencyObject target,DependencyProperty property)
        {
            BindingExpression expression = BindingOperations.GetBindingExpression(target,property);
            T sourceObject = (T)expression.ResolvedSource; //获取到数据源对象，这样可以根据需求进行具体操作

        }
    }
}
