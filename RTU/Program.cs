using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTU
{
    public class Program
    {
        static void Main(string[] args)
        {
            RealTimeDriverClient client = new CoreService.RealTimeDriverClient();

            Console.Write("Enter RTU Driver Address: ");
            string driverAddress = Console.ReadLine();

            Console.Write("Enter lower limit for values: ");
            double lowerLimit = int.Parse(Console.ReadLine());

            Console.Write("Enter upper limit for values: ");
            double upperLimit = int.Parse(Console.ReadLine());

            RealTimeUnit rtu = new RealTimeUnit(driverAddress, lowerLimit, upperLimit);

            double value = rtu.GenerateRandomValue();
            string message = $"{rtu.DriverAddress}:{value}";

            byte[] signature = rtu.SignMessage(message, out byte[] h);

            client.SubscribeRealTimeUnit(message, signature);

            System.Threading.Thread.Sleep(5000);

            while (true)
            {
                value = rtu.GenerateRandomValue();
                message = $"{rtu.DriverAddress}:{value}";

                signature = rtu.SignMessage(message, out byte[] hash);

                client.SendMessage(message, signature);

                System.Threading.Thread.Sleep(5000);
            }
        }
    }
}
