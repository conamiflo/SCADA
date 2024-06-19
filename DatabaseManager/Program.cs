using DatabaseManager.CoreService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Model.Tag;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using Core.Model;
using System.Xml.Linq;
using Core.Service;

namespace DatabaseManager
{
    class Program
    {
        public static TagServiceClient tagServiceClient = new CoreService.TagServiceClient();

        static void Main(string[] args)
        {
            UserServiceClient client = new CoreService.UserServiceClient();
            //AnalogInput analogInput = new AnalogInput();
            //analogInput.TagName = "aasdasdsdf";
            //analogInput.Description = "asdf";
            //analogInput.Units = "asdf";
            //analogInput.HighLimit = 1.0;
            //analogInput.LowLimit = 1.0;
            //analogInput.IsOn = true;
            //analogInput.IOAddress = "asdf";
            //analogInput.Driver = "Driveeeer";

            //Alarm alarm = new Alarm
            //{
            //    Id = 1,
            //    Threshold = 80.0,
            //    Type = AlarmType.HIGH,
            //    Priority = 3,
            //    Unit = "C"
            //};
            //Alarm alarm2 = new Alarm
            //{
            //    Id = 12,
            //    Threshold = 80.0,
            //    Type = AlarmType.HIGH,
            //    Priority = 3,
            //    Unit = "C"
            //};
            //analogInput.Alarms.Add(alarm);
            //analogInput.Alarms.Add(alarm2);

            //tagServiceClient.AddAnalogInput(analogInput);

            //DigitalInput digitalInput = new DigitalInput();
            //digitalInput.TagName = "aaaa";
            //digitalInput.Description = "asdf";
            //digitalInput.IsOn = true;
            //digitalInput.ScanTime = 1;
            //digitalInput.IOAddress = "asdf";
            //digitalInput.Driver = "Driveeeer";

            //tagServiceClient.AddDigitalInput(digitalInput);

            UnsignedMenu(client);
        }

        public static string Register(UserServiceClient client)
        {
            Console.Write("Welcome to registration!\n\nPlease enter your username:");
            string username = Console.ReadLine();

            Console.Write("\nEnter your password:");
            string password = Console.ReadLine();

            string message = "Registration failed";
            try
            {
                return client.Registration(username,password);
            }
            catch (ArgumentException ex)
            {
                message = $"Registration failed: {ex.Message}";
            }
            catch (Exception ex)
            {
                message = $"{ex.Message}";
            }
            return message;
        }

        public static string Login(UserServiceClient client)
        {
            Console.Write("Welcome to Login!\n\nPlease enter your username: ");
            string username = Console.ReadLine();

            Console.Write("\nEnter your password: ");
            string password = Console.ReadLine();

            string message = "Login failed!";
            try
            {
                return client.Login(username, password);
            }
            catch (Exception ex)
            {
                message = $"{ex.Message}";
            }
            Console.WriteLine(message);
            return null;
        }

        public static void UnsignedMenu(UserServiceClient client)
        {
            while (true)
            {
                Console.WriteLine("\nMenu:");
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
                            Console.WriteLine("\nLogin sucessfull!");
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
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1.Create Tag");
                Console.WriteLine("2. Give sloppy toppy");
                Console.WriteLine("3. Sign out");
                Console.Write("Choose an option (1-3): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        TagCreationMenu();
                        break;
                    case "2":
                        Console.WriteLine("\n,':/\n");
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

        public static void TagCreationMenu()
        {
            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1.Create Input Analog Tag");
                Console.WriteLine("2.Create Output Analog Tag");
                Console.WriteLine("3.Create Input Digital Tag");
                Console.WriteLine("4.Create Output Digital Tag");
                Console.WriteLine("5. Back");
                Console.Write("Choose an option (1-3): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AnalogTagCreation(true);
                        break;
                    case "2":
                        AnalogTagCreation(false);
                        break;
                    case "3":
                        DigitalTagCreation(true);
                        break;
                    case "4":
                        DigitalTagCreation(false);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please choose again.");
                        break;
                }

                Console.WriteLine();
            }
        }


        public static void AnalogTagCreation( bool input)
        {

            Console.WriteLine("\nEnter the name of the tag: ");
            string name = Console.ReadLine();

            Console.WriteLine("Enter the description of the tag: ");
            string description = Console.ReadLine();

            Console.WriteLine("Enter the I/O address of the tag: ");
            string ioAddress = Console.ReadLine();

            Console.Write("Enter Driver: ");
            string driver = Console.ReadLine();

            Console.Write("Enter Low Limit: ");
            double lowLimit;
            while (!double.TryParse(Console.ReadLine(), out lowLimit))
            {
                Console.Write("Invalid input. Please enter a valid number for Low Limit: ");
            }

            Console.Write("Enter High Limit: ");
            double highLimit;
            while (!double.TryParse(Console.ReadLine(), out highLimit))
            {
                Console.Write("Invalid input. Please enter a valid number for High Limit: ");
            }

            Console.Write("Enter Units: ");
            string units = Console.ReadLine();

            if(input)
            {
                Console.Write("Enter On/Off Scan (true/false): ");
                bool onOffScan;
                while (!bool.TryParse(Console.ReadLine(), out onOffScan))
                {
                    Console.Write("Invalid input. Please enter true or false for On/Off Scan: ");
                }

                Console.Write("Enter Scan Time (in milliseconds): ");
                int scanTime;
                while (!int.TryParse(Console.ReadLine(), out scanTime))
                {
                    Console.Write("Invalid input. Please enter a valid integer for Scan Time: ");
                }



                AnalogInput tag = new AnalogInput(name,  description,  ioAddress,  scanTime,driver,null,  onOffScan,  lowLimit,  highLimit,  units);
                tagServiceClient.AddAnalogInput(tag);
            }
            else
            {
                Console.Write("Enter an initial value: ");
                int initValue;
                while (!int.TryParse(Console.ReadLine(), out initValue))
                {
                    Console.Write("Invalid input. Please enter valid int for initial value: ");
                }

                AnalogOutput tag = new AnalogOutput(name, description, ioAddress, initValue, lowLimit, highLimit, units);
                tagServiceClient.AddAnalogOutput(tag);


            }


        }

        public static void DigitalTagCreation(bool input)
        {
            Console.Write("Enter Tag Name (ID): ");
            string tagName = Console.ReadLine();

            Console.Write("Enter Description: ");
            string description = Console.ReadLine();

            Console.Write("Enter I/O Address: ");
            string ioAddress = Console.ReadLine();

            if (input)
            {
                Console.Write("Enter Driver: ");
                string driver = Console.ReadLine();


                Console.Write("Enter On/Off Scan (true/false): ");
                bool onOffScan;
                while (!bool.TryParse(Console.ReadLine(), out onOffScan))
                {
                    Console.Write("Invalid input. Please enter true or false for On/Off Scan: ");
                }

                Console.Write("Enter Scan Time (in milliseconds): ");
                int scanTime;
                while (!int.TryParse(Console.ReadLine(), out scanTime))
                {
                    Console.Write("Invalid input. Please enter a valid integer for Scan Time: ");
                }

                DigitalInput tag = new DigitalInput(tagName, description, ioAddress, driver,scanTime,onOffScan);
                tagServiceClient.AddDigitalInput(tag);

            }
            else
            {
                Console.Write("Enter an initial value: ");
                int initValue;
                while (!int.TryParse(Console.ReadLine(), out initValue))
                {
                    Console.Write("Invalid input. Please enter valid int for initial value: ");
                }

                DigitalOutput tag = new DigitalOutput(tagName, description, ioAddress, initValue);
                tagServiceClient.AddDigitalOutput(tag);


            }




        }


        void AlarmCreation()
        {
            AnalogInput tag = new AnalogInput();
            while (true)
            {

                Console.Write("Enter Alarms Tag Name ");

                string input = Console.ReadLine();
                
                tag= tagServiceClient.GetAnalogInput(input);

                if (tag == null)
                {
                    Console.Write("Non Existant Analog Input Tag Name. ");
                    continue;
                }

                break;

            }


            Console.Write("Enter Alarm Type: ");
            Console.WriteLine("1.Low");
            Console.WriteLine("2.High");
            AlarmType type;
            while (true)
            {
                string input = Console.ReadLine().ToLower();
                if (input == "1" || input == "2")
                {
                    type = (AlarmType)int.Parse(input);
                    break;
                }
                else
                {
                    Console.Write("Invalid input. Please enter '1' or '2' for Alarm Type: ");
                }
            }

            Console.Write("Enter Priority (1, 2, 3): ");
            int priority;
            while (!int.TryParse(Console.ReadLine(), out priority) || (priority < 1 || priority > 3))
            {
                Console.Write("Invalid input. Please enter 1, 2, or 3 for Priority: ");
            }

            Console.Write("Enter Threshold Value: ");
            double threshold;
            while (!double.TryParse(Console.ReadLine(), out threshold))
            {
                Console.Write("Invalid input. Please enter a valid number for Threshold Value: ");
            }

            Console.Write("Enter Size Name the alarm is tied to: ");
            string sizeName = Console.ReadLine();

            Console.Write("Enter Unit which alarm uses: ");
            string unit = Console.ReadLine();

            Alarm alarm = new Alarm(threshold, type, priority, unit);

            tag.addAlarm(alarm);

        }




    }
}
