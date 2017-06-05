using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatternConsoleApplication
{
    public class Employee
    {
        #region 工厂模式
        public class EmployeeAddress
        {
            public string Address1 { get; set; }
            public string Address2 { get; set; }
        }
        public string Name { get; set; }

        public EmployeeAddress AddressCollection { get; set; }

        #endregion

        #region 观察者模式

        private EmployeeChangeName _employeeChangeName;
        public event EmployeeChangeName EmployeeChangeNameEvent
        {
            add { _employeeChangeName += value; }
            remove
            {
                if (_employeeChangeName != null)
                {
                    _employeeChangeName -= value;
                }
            }
        }

        public void ChangeName(string name)
        {
            if (_employeeChangeName != null)
            {
                this.Name = name;
                this._employeeChangeName(name);
            }
        }
        #endregion


    }
}
