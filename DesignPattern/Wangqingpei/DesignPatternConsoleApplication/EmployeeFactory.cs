using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatternConsoleApplication
{
    public class EmployeeFactory
    {
        public class EmployeeCreatePrameterContex
        {
            public string AddressString { get; set; }
        }

        public static Employee CreatEmployee(string name, string addressString,
            Func<EmployeeFactory.EmployeeCreatePrameterContex, Employee.EmployeeAddress> addressFactory)
        {
            EmployeeCreatePrameterContex contex = new EmployeeCreatePrameterContex(){AddressString = addressString};
            return new Employee()
            {
                Name = name,
                AddressCollection = addressFactory(contex)
            };
        }
    }
}
