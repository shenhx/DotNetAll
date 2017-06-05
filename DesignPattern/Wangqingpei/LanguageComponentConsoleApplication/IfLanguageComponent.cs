using System;

namespace LanguageComponentConsoleApplication
{
    [Serializable]
    public class IfLanguageComponent<T> : LanguageComponent where T : class,new ()
    {
        public LanguageComponent If { get; set; }
        public LanguageComponent Else { get; set; }
        private T parameter;
        public Func<T,bool> Expression { get; private set; }
        
        public IfLanguageComponent(LanguageComponent ifLanguage, LanguageComponent elseLanguage)
        {
            this.If = ifLanguage;
            this.Else = elseLanguage;
        }

        public void SetIfExpression(Func<T,bool> expression,T parameter)
        {
            this.Expression = expression;
            this.parameter = parameter;
        }

        public override void Run(object parameter, LanguageComponentManager trackMark)
        {
            trackMark.Index = 0;
            if(this.Expression(parameter as T))
            {
                this.If.Run(parameter, trackMark);
            }
            else if(Else != null)
            {
                this.Else.Run(parameter,trackMark);
            }
        }

        public override void Run(LanguageComponentManager trachMark)
        {
            this.Run(parameter, trachMark);
        }
    }
}