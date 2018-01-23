using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Sample.CommandTest
{
    public class MyCommands
    {
        private static RoutedUICommand _orderSplit;

        static MyCommands()
        {
            //无法实现组合按钮命令
            //InputGestureCollection inputs = new InputGestureCollection();
            //inputs.Add(new KeyGesture(Key.O,ModifierKeys.Control,"Ctrl + O"));
            //inputs.Add(new KeyGesture(Key.P,ModifierKeys.Control,"Ctrl + P"));
            _orderSplit = new RoutedUICommand("OrderSplit", "OrderSplit",typeof(MyCommands));
        }

        /// <summary>
        /// 医嘱拆分命令
        /// </summary>
        public static RoutedUICommand OrderSplit
        {
            get { return _orderSplit; }
        }
    }
}
