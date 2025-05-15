using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Classes.ControlEventArgs
{
    public class EmployeeEventArgs : EventArgs
    {
        public Employees Employee { get; set; }
        public EmployeeEventArgs(Employees employee) => Employee = employee;
    }
}
