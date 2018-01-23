using System;
using System.Windows;

namespace Sample.RouteEventTest
{
    /// <summary>
    /// 需要引用PresentationCore.dll、Windowsbase.dll
    /// </summary>
    public class MyRouteEvent : UIElement
    {
        //第一步：定义路由事件
        public static readonly RoutedEvent SayHelloEvent;

        //第二步：注册SayHello事件
        static MyRouteEvent()
        {
            MyRouteEvent.SayHelloEvent = EventManager.RegisterRoutedEvent("SayHello",RoutingStrategy.Bubble,typeof(RoutedEventHandler),typeof(MyRouteEvent));
        }

        //第三步：按传统方式封装事件
        public event RoutedEventHandler SayHello
        {
            add { base.AddHandler(MyRouteEvent.SayHelloEvent, value); }
            remove { base.RemoveHandler(MyRouteEvent.SayHelloEvent, value); }
        }

        //备注：共享路由事件参考MainWindow.cs

        
    }
}
