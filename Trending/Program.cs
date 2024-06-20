using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Trending.CoreService;

namespace Trending
{
    public class Callback : ITrendingServiceCallback
    {
        //TODO lockovanje??
        public void addTagValue(string tagName, double value)
        {
            Program.tags[tagName] = value;
            Program.printTable();
        }

        public void removeTag(string tagName)
        {
            Program.tags.Remove(tagName);
            Program.printTable();
        }

        public void initTagTable(Dictionary<string, double> tagsVals)
        {
           Program.tags = tagsVals;
           Program.printTable();

        }

    }

    public class Program
    {
        public static TrendingServiceClient trendingServiceClient;
        public static Dictionary<string,double> tags = new Dictionary<string,double>();
        static void Main(string[] args)
        {

            InstanceContext ic = new InstanceContext(new Callback());
            trendingServiceClient = new TrendingServiceClient(ic);

            trendingServiceClient.SubscribeToTrending();
            Console.ReadKey();
            trendingServiceClient.Close();
        }

        public static void printTable()
        {
            Console.Clear();
            foreach (KeyValuePair<string, double> kvp in tags)
            {
                Console.WriteLine("Tag: {0}, Value: {1}", kvp.Key, kvp.Value);
            }
        }
    }
}
