using IBatisNet.DataMapper.TypeHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBatisNetWinformDemo.Entities
{
    /// <summary>
    /// SampleTypeHandlerCallBack
    /// </summary>
    public class SampleTypeTypeHandlerCallBack : ITypeHandlerCallback
    {
        public object GetResult(IResultGetter getter)
        {
            try
            {
                string retVal = "非正式";
                string getVal = getter.Value.ToString();
                if (getVal.StartsWith("pro"))
                {
                    retVal = "正式";
                }
                return retVal;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "";
            }
        }

        public object NullValue
        {
            get { return ""; }
        }

        public void SetParameter(IParameterSetter setter, object parameter)
        {
            throw new NotImplementedException();
        }

        public object ValueOf(string s)
        {
            throw new NotImplementedException();
        }
    }
}
