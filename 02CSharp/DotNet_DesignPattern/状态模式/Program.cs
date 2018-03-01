using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 介绍
意图：又称为状态对象模式，该模式允许一个对象在其内部状态改变时改变其行为。
主要解决：
何时使用： 
如何解决：包含抽象状态角色、具体状态角色和环境角色（定义客户端需要的接口并负责具体状态的切换）。核心是封装，状态的变更引起行为的变动，从外部看来像改变了其类型。
关键代码：
应用实例：
优点： 结构清晰、封装性非常好
缺点：子类太多，不容易管理。
使用场景： 
注意事项：
     */
namespace 状态模式
{
    class Program
    {
        static void Main(string[] args)
        {
            Context context = new Context();
            context.Handle1();
            context.Handle2();
        }
    }

    abstract class StateBase
    {
        protected Context context;
        public void SetContext(Context context)
        {
            this.context = context;
        }
        /// <summary>
        /// 抽象行为
        /// </summary>
        public abstract void Handle();
    }

    class Context
    {
        //定义状态
        public static StateBase state1 = new State1();
        public static StateBase state2 = new State2();

        private StateBase _stateBase;
        public StateBase CurState
        {
            get { return _stateBase; }
            set { _stateBase = value;
                _stateBase.SetContext(this);
            }
        }

        public void Handle1()
        {
            this._stateBase = state1;
            this.CurState.Handle();
        }
        public void Handle2()
        {
            this._stateBase = state2;
            this.CurState.Handle();
        }
    }

    class State1 : StateBase
    {
        public override void Handle()
        {
            Console.WriteLine("hello,state1");
        }
    }

    class State2 : StateBase
    {
        public override void Handle()
        {
            Console.WriteLine("hello,state2");
        }
    }
}
