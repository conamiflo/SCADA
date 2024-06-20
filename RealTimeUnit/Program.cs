using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeUnit
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter RTU Driver Address: ");
            string driverAddress = Console.ReadLine();

            Console.Write("Enter lower limit for values: ");
            int lowerLimit = int.Parse(Console.ReadLine());

            Console.Write("Enter upper limit for values: ");
            int upperLimit = int.Parse(Console.ReadLine());

            RealTimeUnit rtu = new RealTimeUnit(driverAddress, lowerLimit, upperLimit);

            // Pretpostavimo da je naziv endpointa u konfiguracionom fajlu "RealTimeDriverEndpoint"
            ChannelFactory<IRealTimeDriver> factory = new ChannelFactory<IRealTimeDriver>("RealTimeDriverEndpoint");
            IRealTimeDriver client = factory.CreateChannel();

            while (true)
            {
                int value = rtu.GenerateRandomValue();
                string message = $"{rtu.DriverAddress}:{value}:{DateTime.Now}";

                byte[] signature = rtu.SignMessage(message, out byte[] hash);

                client.SendMessage(message, signature);

                System.Threading.Thread.Sleep(5000);
            }
        }
    }
}
