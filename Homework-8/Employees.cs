using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_8
{
    public class Employees
    {       
        //Имя сотрудника
        public string Name { get; set; }

        //Заработная плата сотрудника
        public int Money { get; set; }

        //Левая ветка
        public Employees LeftBranch { get; set; }

        //Правая ветка
        public Employees RightBranch { get; set; }
    }
}
