using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_8
{
    public class Employee
    {       
        //Имя сотрудника
        public string Name { get; set; }

        //Заработная плата сотрудника
        public int Salary { get; set; }

        //Левая ветка
        public Employee LeftBranch { get; set; }

        //Правая ветка
        public Employee RightBranch { get; set; }
    }
}
