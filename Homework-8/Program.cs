using System;
using System.Xml.Linq;

namespace Homework_8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Employee userEmployee = null;
            DictionaryEmployees dictionary = new DictionaryEmployees();


            while (true)
            {
                while (true)
                {
                    Console.WriteLine("Программа работает в двух режимах.\nВведите 1 для ручного ввода сотрудников.\nВведите 0 для автоматического заполнения.");
                    var check = int.TryParse(Console.ReadLine(), out int mode);

                    if (check && mode == 0)
                    {
                        //Получение данных от пользователя и построение бинарного дерева (автоматический ввод)
                        userEmployee = GetConsoleUserDataAuto(userEmployee);
                        break;
                    }
                    else if (check && mode == 1)
                    {
                        //Получение данных от пользователя и построение бинарного дерева (ручной ввод)
                        userEmployee = GetConsoleUserData(userEmployee);
                        break;
                    }
                }

                //Сортировка и вывод в консоль по возростанию сотрудников + зп
                Console.WriteLine("-- Сортировка --");
                SortingEmployees(userEmployee);
                Console.WriteLine();

                Console.WriteLine("-- Поиск --");
                while (true)
                {
                    //Поиск сотрудника по з/п
                    Console.WriteLine("Введите зарплату сотрудника для поиска: ");
                    var result = int.TryParse(Console.ReadLine(), out int inputMoney);

                    SearchEmployee(userEmployee, inputMoney);

                    //Продолжить поиск или вернуться в начало програмы
                    if (SearchOrReturn() == 0) break;
                }

                userEmployee = null;
                Console.Clear();
            }  
         
            
            

            //---------------------------------- Методы ----------------------------------//

            int SearchOrReturn()
            {
                var inputMoney = 0;

                while (true)
                {
                    //Продолжить поиск или вернуться в начало програмы
                    Console.WriteLine("Введите цифру 1 если хотите продолжить поиск или 0 если хотите начать с начала: ");
                    Console.WriteLine();
                    var rezalt = int.TryParse(Console.ReadLine(), out inputMoney);

                    if (rezalt && inputMoney == 1 || inputMoney == 0) break;
                
                }
                return inputMoney;
            }

            //Метод для получения из консоли Имени и зарплаты сотрудника в ручном режиме
            Employee GetConsoleUserData(Employee userEmployee)
            {
                Console.WriteLine("-- Добавление сотрудников --\nДля перехода к сортировке введите пустое значение в строке (имя сотрудника)\n");
                
                while (true)
                {
                    Console.Write("Введите имя сотрудника: ");
                    var name = Console.ReadLine();

                    //Если пользователь ввёл пустое значение в запросе Имени, выходим из метода
                    if (name == string.Empty) break;

                    Console.Write("Введите зарплату сотрудника: ");
                    var rezalt = int.TryParse(Console.ReadLine(), out int money);
                    Console.WriteLine();

                    if (rezalt)
                    {
                        if (userEmployee == null)
                        {
                            userEmployee = new Employee()
                            {
                                Salary = money,
                                Name = name
                            };
                        }
                        else
                        {
                            AddNewEmployee(userEmployee, new Employee
                            {
                                Salary = money,
                                Name = name
                            });
                        }

                    }
                }
                return userEmployee;
            }

            //Метод для получения из консоли Имени и зарплаты сотрудника в ручном режиме
            Employee GetConsoleUserDataAuto(Employee еmployee)
            {
                Console.WriteLine("-- Добавление сотрудников авто --");

                foreach (var item in dictionary.employeesDict)
                {
                    Console.WriteLine("Введите имя сотрудника: " + item.Key);
                    Console.WriteLine("Введите зарплату сотрудника: " + item.Value);
                    Console.WriteLine();

                    if (еmployee == null)
                    {
                        еmployee = new Employee()
                        {
                            Salary = item.Value,
                            Name = item.Key
                        };
                    }
                    else
                    {
                        AddNewEmployee(еmployee, new Employee
                        {
                            Salary = item.Value,
                            Name = item.Key
                        });
                    }
                }
                                
                return еmployee;
            }

            //Метод добавления новых объектов и построения бинарного дерева
            void AddNewEmployee(Employee rootNode, Employee childNode)
            {
                if (childNode.Salary < rootNode.Salary)
                {
                    //Идёс в левую ветку
                    if (rootNode.LeftBranch != null)
                    {
                        AddNewEmployee(rootNode.LeftBranch, childNode);
                    }
                    else
                    {
                        rootNode.LeftBranch = childNode;
                    }
                }
                else
                {
                    //Идём в правую ветку
                    if (rootNode.RightBranch != null)
                    {
                        AddNewEmployee(rootNode.RightBranch, childNode);
                    }
                    else
                    {
                        rootNode.RightBranch = childNode;
                    }
                }
            }

            //Метод сортировки сотрудников по возростанию по зп
            void SortingEmployees(Employee userEmployee)
            {
                if (userEmployee.LeftBranch != null)
                {
                    SortingEmployees(userEmployee.LeftBranch);
                }
                Console.WriteLine($"{userEmployee.Name} - {userEmployee.Salary}");

                if (userEmployee.RightBranch != null)
                {
                    SortingEmployees(userEmployee.RightBranch);
                }
            }

            //Метод поиска сотрудника по зп
            void SearchEmployee(Employee userEmployee, int inputMoney)
            {
                //Поиск
                if (userEmployee.Salary == inputMoney)
                {
                    Console.Write("Найден сотрудник - " + userEmployee.Name);
                    Console.WriteLine();
                }
                else if (userEmployee.Salary < inputMoney)
                {
                    if (userEmployee.RightBranch != null)
                    {
                        SearchEmployee(userEmployee.RightBranch, inputMoney);
                    }
                    else
                    {
                        Console.Write("Сотрудник с зарплатой " + inputMoney + " не найден.");
                        Console.WriteLine();
                    }
                }
                else if (userEmployee.Salary > inputMoney)
                {
                    if (userEmployee.LeftBranch != null)
                    {
                        SearchEmployee(userEmployee.LeftBranch, inputMoney);
                    }
                    else
                    {
                        Console.Write("Сотрудник с зарплатой " + inputMoney + " не найден.");
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}