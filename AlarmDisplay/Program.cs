using System;
using System.Collections.Generic;
using System.Linq;
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
        static void Main(string[] args)
        {
        }
    }
}
