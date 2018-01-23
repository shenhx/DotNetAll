using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sample.DependencyObjectTest
{
    /// <summary>
    /// 定义依赖属性
    /// 继承自DependencyObject类
    /// </summary>
    public class MyDependencyObject : DependencyObject
    {
        //第一步：定义依赖属性(属性名称+Property)
        public static readonly DependencyProperty IsDefaultProperty;

        //第二步：注册依赖项属性
        static MyDependencyObject()
        {
            //PropertyChangedCallback:表示在依赖属性的有效属性值更改时调用的回调
            PropertyMetadata metadata = new PropertyMetadata(false);
            metadata.CoerceValueCallback = new CoerceValueCallback((d, o) => {
                //强制验证
                return true; });
            IsDefaultProperty = DependencyProperty.Register("IsDefault", typeof(bool), typeof(bool), metadata);
        }

        //第三步：添加属性包装器
        public bool IsDefault
        {
            get { return (bool)GetValue(IsDefaultProperty); }
            set { SetValue(IsDefaultProperty, value); }
        }

        ~MyDependencyObject()
        {
            //清除属性的值
            this.ClearValue(IsDefaultProperty);
        }
    }
}
