using AlarmDisplay.CoreService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AlarmDisplay
{
    public class Callback : CoreService.IAlarmServiceCallback
    {
        public void AlarmTriggered(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class Program
    {
        public static AlarmServiceClient alarmServiceClient;
        static void Main(string[] args)
        {
            InstanceContext ic = new InstanceContext(new Callback());
            alarmServiceClient = new AlarmServiceClient(ic);

            alarmServiceClient.SubscribeToAlarmDisplay();
            Console.ReadKey();
            alarmServiceClient.Close();
        }
    }
}
