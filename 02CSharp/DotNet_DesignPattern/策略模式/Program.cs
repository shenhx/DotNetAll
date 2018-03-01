using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 介绍
意图：定义一组算法，将每个算法都封装起来，并且使他们之间可以互换。
主要解决：针对一组算法，将每个算法封装到具体类中，从而使得他们可以互换，并且算法不影响客户端的情况下发生变化。
何时使用： 
如何解决：环境（Context）角色，上下文角色，屏蔽高层模块策略、算法的直接访问，持有一个strategy类的引用。 抽象策略角色 具体策略角色
关键代码：
应用实例：
优点： 策略模式提供了管理相关的算法族的办法。 策略模式提供可以替换继承关系的办法。 可以避免使用多重条件转移的语句。
缺点：客户端必须知道所有的策略类，并自行决定使用哪一个策略类。（可利用规则引擎动态设计策略规则） 策略模式会造成很多的策略类
使用场景： 多个类只是在算法或行为上稍有不同的场景。算法需要自由切换 。需要屏蔽算法规则的场景。
注意事项：
     */
namespace 策略模式
{
    class Program
    {
        static void Main(string[] args)
        {
            new Context(new ConcreteStrategyB()).ContextInterface();
        }
    }

    public abstract class Strategy { public abstract void StrategyInterface(); }

    public class ConcreteStrategy : Strategy
    {
        public override void StrategyInterface()
        {
            throw new NotImplementedException();
        }
    }

    public class ConcreteStrategyB : Strategy
    {
        public override void StrategyInterface()
        {
            throw new NotImplementedException();
        }
    }

    public class Context
    {
        private Strategy m_strategy = null;

        public Context(Strategy strategy)
        {
            this.m_strategy = strategy;
        }

        public void ContextInterface()
        {
            this.m_strategy.StrategyInterface();
        }
}
