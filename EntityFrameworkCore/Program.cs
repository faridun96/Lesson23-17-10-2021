using System;
using System.Linq;
using EntityFrameworkCore.Models;
using EntityFrameworkCore.Context;

namespace EntityFrameworkCore
{
    class Program
    {
        static void Create()
        {
            try
            {
                using (var context = new FaridunDBContext())
                {
                    System.Console.Write("Введите логин: ");
                    string log = Console.ReadLine();
                    var list = context.Customers.ToList();
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].Login == log)
                        {
                            System.Console.WriteLine("Такой логин уже существует!");
                            return;
                        }
                    }
                    System.Console.Write("Введите пароль: ");
                    string pas = Console.ReadLine();
                    Console.Write("Введите имя: ");
                    string name = Console.ReadLine();
                    Console.Write("Введите фамилию: ");
                    string lastName = Console.ReadLine();
                    Console.Write("Введите возраст: ");
                    int age = int.Parse(Console.ReadLine());
                    Customers customers = new Customers();
                    customers.Name = name;
                    customers.LastName = lastName;
                    customers.Age = age;
                    customers.Password = pas;
                    customers.Login = log;
                    context.Customers.Add(customers);
                    int a = context.SaveChanges();
                    if (a > 0)
                    {
                        System.Console.WriteLine("Успешно добавлен");
                    }
                    else System.Console.WriteLine("Прозошла ошибка во время добавления");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            finally
            {
                CWCRK();
            }
        }
        static void Update()
        {
            try
            {
                if (!Select(""))
                {
                    System.Console.WriteLine("Нечего изменять, увы...");
                }
                else
                {
                    using (var context = new FaridunDBContext())
                    {
                        Console.Write("Введите id: ");
                        int id = int.Parse(Console.ReadLine());
                        Customers customer = context.Customers.Find(id);
                        if (customer != null)
                        {
                            Console.Write("Введите новое имя: ");
                            customer.Name = Console.ReadLine();
                            Console.Write("Ввведите новую фамилию: ");
                            customer.LastName = Console.ReadLine();
                            System.Console.Write("Введите новый возраст: ");
                            customer.Age = int.Parse(Console.ReadLine());
                            if (context.SaveChanges() > 0) Console.WriteLine("Успешно");
                            else System.Console.WriteLine("Произошла ошибка во время изменения");
                        }
                        else System.Console.WriteLine("Клиент с таким id не существует");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            finally
            {
                CWCRK();
            }
        }
        static void Delete()
        {
            try
            {
                if (!Select(""))
                {
                    System.Console.WriteLine("Некого удалять");
                }
                else
                {
                    using (var context = new FaridunDBContext())
                    {
                        Console.Write("Введите id клиента которого хотите удалить: ");
                        int id = int.Parse(Console.ReadLine());
                        Customers customer = context.Customers.Find(id);
                        if (customer != null)
                        {
                            System.Console.Write($"Вы точно хотите удалить клиента {customer.Name}?(Y/N):");
                            if (Console.ReadLine().ToLower() == "y") context.Customers.Remove(customer);
                            if (context.SaveChanges() > 0) Console.WriteLine("successfull");
                            else System.Console.WriteLine("Customer not deleted");
                        }
                        else
                        {
                            System.Console.WriteLine("Клиент с таким id не существует");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            finally
            {
                CWCRK();
            }
        }
        static void CWCRK()
        {
            System.Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
        static bool Select(string a = "1")
        {
            try
            {
                using (var context = new FaridunDBContext())
                {
                    var customers = context.Customers.ToList();
                    foreach (var items in customers)
                    {
                        System.Console.WriteLine($"Id = {items.Id} | Name = {items.Name} | LastName = {items.LastName} | Age = {items.Age} | Login {items.Login} | Password = {items.Password}");
                    }
                    if (customers.Count == 0)
                    {
                        System.Console.WriteLine("В данный момент список пуст.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (a == "1")
                    CWCRK();
            }
            return true;
        }
        static void Main(string[] args)
        {
            string choice = "1";
            while (choice != "5")
            {
                Console.Clear();
                Console.Write("1. Create\n2. Update\n3. Delete\n4. Select\n5. Exit\nChoice: ");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": Create(); break;
                    case "2": Update(); break;
                    case "3": Delete(); break;
                    case "4": Select(); break;
                }
            }
        }
    }
}