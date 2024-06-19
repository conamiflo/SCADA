using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace ReportManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }

        public static void Menu()
        {
            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. All alarms that occurred within a specific time period");
                Console.WriteLine("2. All alarms of a specific priority");
                Console.WriteLine("3. All tag values that have arrived at the service within a specific time period ");
                Console.WriteLine("4. The latest value of all AI (Analog Input) tags ");
                Console.WriteLine("5. The latest value of all DI (Digital Input) tags ");
                Console.WriteLine("6. All values of a tag with a specific identifier ");
                Console.WriteLine("7. Quit");
                Console.Write("Choose an option (1-7): ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        getAlarmsInTimePeriod();
                        break;
                    case "2":
                        getAlarmsByPriority();
                        break;
                    case "3":
                        getTagValuesInTimePeriod();
                        return;
                    case "4":
                        getLatestAITags();
                        break;
                    case "5":
                        getLatestDITags();
                        break;
                    case "6":
                        getTagByIdentifier();
                        break;
                    case "7":
                        Console.WriteLine("Exiting program. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please choose again.");
                        break;
                }
                Console.WriteLine();
            }
        }
        static bool GetBooleanFromUser(string message, string value)
        {
            Console.Write(message);
            string input = Console.ReadLine().Trim().ToLower();

            return input == value;
        }

        static int GetNumberFromUser(string message ,int lowerBound, int higherBound)
        {
            int number;
            bool isValidInput;

            do
            {
                Console.Write($"{message} between {lowerBound} and {higherBound}: ");
                string input = Console.ReadLine();
                isValidInput = int.TryParse(input, out number) && number >= lowerBound && number <= higherBound;

                if (!isValidInput)
                {
                    Console.WriteLine($"Invalid input. {message} between {lowerBound} and {higherBound} or quit by entering q: ");
                    if (Console.ReadLine().ToLower().Equals("q"))
                    {
                        throw new Exception();
                    }
                }
            }
            while (!isValidInput);

            return number;
        }

        static DateTime GetUserDate(string message)
        {
            DateTime date;
            string input;
            bool isValid;
            string choice = "";
            do
            {
                Console.Write("\n"+message);
                input = Console.ReadLine();
                isValid = DateTime.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

                if (!isValid)
                {
                    Console.Write("Invalid date format. Please try again or quit by entering q: ");
                    choice = Console.ReadLine();
                }
                else if(date > DateTime.Now)
                {
                    Console.Write("Date must be in the past. Please try again or quit by entering q: ");
                    choice = Console.ReadLine();
                }

                if (choice.ToLower().Equals("q"))
                {
                    throw new Exception("Canceled getting all alarms that occurred within a specific time period");
                }
            } while (!isValid);

            return date;
        }

        public static void getAlarmsInTimePeriod()
        {
            DateTime startDate;
            DateTime endDate;
            bool sortByPriority;
            try
            {
                startDate = GetUserDate("Please enter the start date of the period where the alarms occurred (format: yyyy-MM-dd): ");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            
            try
            {
                while (true)
                {
                    endDate = GetUserDate("Please enter the end date of the period where the alarms occurred (format: yyyy-MM-dd): ");
                    if (endDate < startDate)
                    {
                        Console.WriteLine("Start date must be before the end date. Please try again entering the end date or quit by entering q: ");
                        if (Console.ReadLine().ToLower().Equals("q"))
                        {
                            Console.WriteLine("Canceled getting all alarms that occurred within a specific time period");
                            return;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            sortByPriority = GetBooleanFromUser("Choose to sort by priority or time (p or t): ", "p");

            //stavti kod koji ce pozvati servis i ispisati sve alarme izmedju perioda start i end date i sortiran po sortByPriority ako je true ili time ako je false

        }

        public static void getAlarmsByPriority()
        {
            int priority;
            try
            {
                priority = GetNumberFromUser("Input alarm priority which is a number", 1, 3);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Quiting getting all alarms of a specific priority.");
                return;
            }
            //kod koji ce dobaviti alarme prioriteta priority i ispisati ih sortirane po vremenu
        }

        public static void getTagValuesInTimePeriod()
        {
            DateTime startDate;
            DateTime endDate;
            try
            {
                startDate = GetUserDate("Please enter the start date of the period where the tags arrived at the service (format: yyyy-MM-dd): ");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            try
            {
                while (true)
                {
                    endDate = GetUserDate("Please enter the end date of the period where the tags arrived at the service (format: yyyy-MM-dd): ");
                    if (endDate < startDate)
                    {
                        Console.WriteLine("Start date must be before the end date. Please try again entering the end date or quit by entering q: ");
                        if (Console.ReadLine().ToLower().Equals("q"))
                        {
                            Console.WriteLine("Canceled getting all tag values that have arrived at the service within a specific time period");
                            return;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                //kod za dobavljanje tagova u odredjenom periodu sortirane po vremenu

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }

        public static void getLatestAITags()
        {
            //kod koji dobavlja sve poslednje vrednosti AI tagova sortirane po vremenu
        }

        public static void getLatestDITags()
        {
            //kod koji dobavlja sve poslednje vrednosti DI tagova sortirane po vremenu
        }

        public static void getTagByIdentifier()
        {
            Console.Write($"Enter a tag identifier: ");
            string tagName = Console.ReadLine();

            //nabaviti sve vrednosti tagova sa identifierom tagName i sortirati po vrednosti (Mozda napraviti da je dovoljno da je tagName pod string TagName-a od taga)
        }

    }
}
