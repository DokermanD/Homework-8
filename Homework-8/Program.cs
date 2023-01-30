using System;
using System.Xml.Linq;

namespace Homework_8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Employees userEmployee = null;

            while (true)
            {
                //Получение данных от пользователя и построение бинарного дерева
                userEmployee = GetConsoleUserData(userEmployee);

                //Сортировка и вывод в консоль по возростанию сотрудников + зп
                Console.WriteLine("-- Сортировка --");
                SortingEmployees(userEmployee);
                Console.WriteLine();

                Console.WriteLine("-- Поиск --");
                while (true)
                {
                    //Поиск сотрудника по з/п
                    Console.WriteLine("Введите зарплату сотрудника для поиска: ");
                    var rezalt = int.TryParse(Console.ReadLine(), out int inputMoney);

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

            //Метод для получения из консоли Имени и зарплаты сотрудника
            Employees GetConsoleUserData(Employees userEmployee)
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
                            userEmployee = new Employees()
                            {
                                Money = money,
                                Name = name
                            };
                        }
                        else
                        {
                            AddNewEmployee(userEmployee, new Employees
                            {
                                Money = money,
                                Name = name
                            });
                        }

                    }
                }
                return userEmployee;
            }

            //Метод добавления новых объектов и построения бинарного дерева
            void AddNewEmployee(Employees userEmployee, Employees addUser)
            {
                if (addUser.Money < userEmployee.Money)
                {
                    //Идёс в левую ветку
                    if (userEmployee.LeftBranch != null)
                    {
                        AddNewEmployee(userEmployee.LeftBranch, addUser);
                    }
                    else
                    {
                        userEmployee.LeftBranch = addUser;
                    }
                }
                else
                {
                    //Идём в правую ветку
                    if (userEmployee.RightBranch != null)
                    {
                        AddNewEmployee(userEmployee.RightBranch, addUser);
                    }
                    else
                    {
                        userEmployee.RightBranch = addUser;
                    }
                }
            }

            //Метод сортировки сотрудников по возростанию по зп
            void SortingEmployees(Employees userEmployee)
            {
                if (userEmployee.LeftBranch != null)
                {
                    SortingEmployees(userEmployee.LeftBranch);
                }
                Console.WriteLine($"{userEmployee.Name} - {userEmployee.Money}");

                if (userEmployee.RightBranch != null)
                {
                    SortingEmployees(userEmployee.RightBranch);
                }
            }

            //Метод поиска сотрудника по зп
            void SearchEmployee(Employees userEmployee, int inputMoney)
            {
                //Поиск
                if (userEmployee.Money == inputMoney)
                {
                    Console.Write("Найден сотрудник - " + userEmployee.Name);
                    Console.WriteLine();
                }
                else if (userEmployee.Money < inputMoney)
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
                else if (userEmployee.Money > inputMoney)
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