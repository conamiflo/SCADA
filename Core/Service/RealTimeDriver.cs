using Core.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Core.Service
{
    public class RealTimeDriver : IRealTimeDriver
    {
        public static string ContainerName { get; private set; } = "KeyContainer";
        public Dictionary<string,double> RTUs = new Dictionary<string,double>();

        public void SendMessage(string message, byte[] signature)
        {
            byte[] hash = ComputeMessageHash(message);

            if (VerifySignedMessage(hash, signature))
            {
                string[] parts = message.Split(':');
                string adress = parts[0];
                double value = double.Parse(parts[1]);

                RTUs[adress] = value;
            }
            else
            {
                Console.WriteLine($"Received invalid message: {message}");
            }
        }

        private static byte[] ComputeMessageHash(string value)
        {
            using (SHA256 sha = SHA256.Create())
            {
                return sha.ComputeHash(Encoding.UTF8.GetBytes(value));
            }
        }

        private static bool VerifySignedMessage(byte[] hash, byte[] signature)
        {
            CspParameters csp = new CspParameters
            {
                KeyContainerName = ContainerName
            };
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp))
            {
                var deformatter = new RSAPKCS1SignatureDeformatter(rsa);
                deformatter.SetHashAlgorithm("SHA256");
                return deformatter.VerifySignature(hash, signature);
            }
        }

        public void SubscribeRealTimeUnit(string message, byte[] signature)
        {
            byte[] hash = ComputeMessageHash(message);

            if (VerifySignedMessage(hash, signature))
            {
                string[] parts = message.Split(':');
                string address = parts[0];
                double value = double.Parse(parts[1]);

                //napraviti provere da li vec postoji u xmlu itd i upisati ga u xml

                RTUs[address] = value;
            }
            else
            {
                Console.WriteLine($"Received invalid message: {message}");
            }
        }

        public double GetRealTimeUnitValue(string IOAdress)
        {
            if (RTUs.ContainsKey(IOAdress))
            {
                return RTUs[IOAdress];  
            }
            return Double.NegativeInfinity;
        }
    }
}