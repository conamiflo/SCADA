using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeUnit
{
    public class RTU
    {
        public string DriverAddress { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }

        public RTU(string address, double lowLimit, double highLimit)
        {
            DriverAddress = address;
            LowLimit = lowLimit;
            HighLimit = highLimit;
        }

        public double GenerateRandomValue()
        {
            return new Random().NextDouble() * (HighLimit - LowLimit) + LowLimit;
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
