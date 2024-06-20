using System;
using System.Collections.Generic;
using System.IO;
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

        private const string KEY_STORE_NAME = "MyKeyStore";
        private const string EXPORT_FOLDER = @"..\public_key\";
        private const string PUBLIC_KEY_FILE = @"rsaPublicKey.txt";

        public double GenerateRandomValue()
        {
            return new Random().NextDouble() * (HighLimit - LowLimit) + LowLimit;
        }

        public void CreateKeys(bool useMachineKeyStore)
        {
            CspParameters csp = new CspParameters
            {
                KeyContainerName = KEY_STORE_NAME
            };
            if (useMachineKeyStore)
            {
                csp.Flags = CspProviderFlags.UseMachineKeyStore;
            }
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp))
            {
                rsa.PersistKeyInCsp = true;
                ExportPublicKey(rsa);
            }
        }

        private void ExportPublicKey(RSACryptoServiceProvider rsa)
        {
            if (!Directory.Exists(EXPORT_FOLDER))
            {
                Directory.CreateDirectory(EXPORT_FOLDER);
            }
            using (StreamWriter writer = new StreamWriter(Path.Combine(EXPORT_FOLDER, PUBLIC_KEY_FILE)))
            {
                writer.WriteLine(rsa.ToXmlString(false));
            }
        }

        public byte[] SignMessage(string message, out byte[] hashValue)
        {
            using (SHA256 sha = SHA256.Create())
            {
                hashValue = sha.ComputeHash(Encoding.UTF8.GetBytes(message));
                CspParameters csp = new CspParameters
                {
                    KeyContainerName = KEY_STORE_NAME
                };
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp))
                {
                    var formatter = new RSAPKCS1SignatureFormatter(rsa);
                    formatter.SetHashAlgorithm("SHA256");
                    return formatter.CreateSignature(hashValue);
                }
            }
        }

        public string CreateSignedMessage()
        {
            double value = GenerateRandomValue();
            string message = $"{DriverAddress}:{value}";
            byte[] hash;
            byte[] signature = SignMessage(message, out hash);
            return Convert.ToBase64String(signature) + ":" + message;
        }
    }


    //public double GenerateRandomValue()
    //{
    //    return new Random().NextDouble() * (HighLimit - LowLimit) + LowLimit;
    //}

    //public byte[] SignMessage(string message, out byte[] hashValue)
    //{
    //    using (SHA256 sha = SHA256.Create())
    //    {
    //        hashValue = sha.ComputeHash(Encoding.UTF8.GetBytes(message));
    //        CspParameters csp = new CspParameters
    //        {
    //            KeyContainerName = "KeyContainer"
    //        };
    //        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp))
    //        {
    //            var formatter = new RSAPKCS1SignatureFormatter(rsa);
    //            formatter.SetHashAlgorithm("SHA256");
    //            return formatter.CreateSignature(hashValue);
    //        }
    //    }
    //}
}

