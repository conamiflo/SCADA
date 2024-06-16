using DatabaseManager.CoreService;
using System;
using System.Collections.Generic;
using System.Linq;
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
            bool continueProgram = true;

            while (continueProgram)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("3. Quit");
                Console.Write("Choose an option (1-3): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("You chose Login.");
                        break;
                    case "2":
                        Console.WriteLine("You chose Register.");
                        bool succesfulRegistration = Register(client);
                        if (succesfulRegistration)
                        {
                            Console.WriteLine("You have succesfully registered, next step is login.");
                        }
                        break;
                    case "3":
                        Console.WriteLine("Exiting program. Goodbye!");
                        continueProgram = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please choose again.");
                        break;
                }

                Console.WriteLine();
            }
        }

        public static bool Register(UserServiceClient client)
        {
            Console.WriteLine("Welcome to registration! Please enter your username:");
            string username = Console.ReadLine();

            Console.WriteLine("Enter your password:");
            string password = Console.ReadLine();


            return client.Registration(username,password);
        }
    }
}
