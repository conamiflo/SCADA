using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeUnit
{
    public class RealTimeUnit
    {
        public string DriverAddress { get; set; }
        public int LowLimit { get; set; }
        public int HighLimit { get; set; }

        public RealTimeUnit(string address, int lowLimit, int highLimit)
        {
            DriverAddress = address;
            LowLimit = lowLimit;
            HighLimit = highLimit;
        }

        public int GenerateRandomValue()
        {
            Random rand = new Random();
            return rand.Next(LowLimit, HighLimit + 1);
        }

        public byte[] SignMessage(string message, out byte[] hashValue)
        {
            using (SHA256 sha = SHA256.Create())
            {
                hashValue = sha.ComputeHash(Encoding.UTF8.GetBytes(message));
                CspParameters csp = new CspParameters
                {
                    KeyContainerName = "KeyContainer"
                };
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp))
                {
                    var formatter = new RSAPKCS1SignatureFormatter(rsa);
                    formatter.SetHashAlgorithm("SHA256");
                    return formatter.CreateSignature(hashValue);
                }
            }
        }
    }
}
