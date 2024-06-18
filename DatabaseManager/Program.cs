﻿using DatabaseManager.CoreService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DatabaseManager
{
    class Program
    {

        static void Main(string[] args)
        {
            UserServiceClient client = new CoreService.UserServiceClient();
            UnsignedMenu(client);
        }

        public static string Register(UserServiceClient client)
        {
            Console.Write("Welcome to registration!\nPlease enter your username:");
            string username = Console.ReadLine();

            Console.Write("\nEnter your password:");
            string password = Console.ReadLine();

            return client.Login(username,password);
        }

        public static string Login(UserServiceClient client)
        {
            Console.Write("Welcome to Login!\nPlease enter your username: ");
            string username = Console.ReadLine();

            Console.Write("\nEnter your password: ");
            string password = Console.ReadLine();


            return client.Registration(username, password);
        }

        public static void UnsignedMenu(UserServiceClient client)
        {
            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("3. Quit");
                Console.Write("Choose an option (1-3): ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        string token = Login(client);
                        if (token != null)
                        {
                            Console.WriteLine("Login sucessfull!");
                            SignedInMenu(client, token);
                        }
                        break;
                    case "2":
                        Console.WriteLine(Register(client));

                        break;
                    case "3":
                        Console.WriteLine("Exiting program. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please choose again.");
                        break;
                }

                Console.WriteLine();
            }
        }

        public static void SignedInMenu(UserServiceClient client, string token)
        {
            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1.");
                Console.WriteLine("2.");
                Console.WriteLine("3. Sign out");
                Console.Write("Choose an option (1-3): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        break;
                    case "2":
                        break;
                    case "3":
                        Console.WriteLine("Signing out. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please choose again.");
                        break;
                }

                Console.WriteLine();
            }
        }

    }
}
