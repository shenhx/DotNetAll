using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 介绍
意图：定义一个操作中的算法的框架，而将一些步骤延迟到子类中。使得子类可以不改变一个算法的结构即可重定义该算法的某些特定步骤。
主要解决：
何时使用： 
如何解决：有两个模式，公共操作封装到抽象模板角色中，个性化实现延迟到具体模板角色中。
关键代码：
应用实例：
优点： 封装不变的部分，扩展可变部分。 提取公共部分的代码，便于维护。 行为由父类控制，子类实现，符合开闭原则。
缺点：
使用场景： 多个子类有公公方法，并且逻辑基本相同时候。 可以把重要的、复杂的、核心算法设计为模板方法。常用于重构代码。
注意事项：
     */
namespace 模板方法模式
{
    class Program
    {
        static void Main(string[] args)
        {
        }


    }

    interface A
    {
        void SayHello();
    }

    abstract class B : A
    {
        public void SayHello()
        {
            throw new NotImplementedException();
        }
    }

    class C : B, A
    {
        //比较简单
    }
}
