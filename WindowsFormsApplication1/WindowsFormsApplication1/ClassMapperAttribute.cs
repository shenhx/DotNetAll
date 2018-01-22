using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =false,Inherited =false)]
    public class ClassMapperAttribute : System.Attribute
    {
        public ClassMapperAttribute(Type targetClassType)
        {
            this.TargetClassType = targetClassType;
        }

        public Type TargetClassType { get; set; }
    }
}
