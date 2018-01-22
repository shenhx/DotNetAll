using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    [ClassMapper(typeof(ClassTarget))]
    public class ClassSrc : ISrc
    {
        private string _name = "";
        public string Names { get; set; }
        public string Names2 { get; set; }
    }
}
