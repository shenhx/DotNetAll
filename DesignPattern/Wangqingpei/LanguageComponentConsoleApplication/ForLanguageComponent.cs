using System;
using System.Collections;
using System.Collections.Generic;

namespace LanguageComponentConsoleApplication
{
    [Serializable]
    public class ForLanguageComponent<T> : LanguageComponent where T : class,IList
    {
        public LanguageComponent ItemExpression { get; set; }

        private T Parameter { get; set; }

        private int CurrentIndex { get; set; }

        public ForLanguageComponent(LanguageComponent itemExpression,T parameter)
        {
            this.ItemExpression = itemExpression;
            this.Parameter = parameter;
        }

        public override void Run(object parameter, LanguageComponentManager trackMark)
        {
            trackMark.Index = 1;
            var pList = (parameter as T);
            for(int index = CurrentIndex; index< pList.Count; index++)
            {
                this.CurrentIndex = index;
                if(ItemExpression!= null)
                {
                    ItemExpression.Run(pList[index], trackMark);
                }
            }
        }

        public override void Run(LanguageComponentManager trachMark)
        {
            this.Run(Parameter, trachMark);
        }
    }
}