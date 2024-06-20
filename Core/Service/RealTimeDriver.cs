using Core.Service.IService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Core.Service
{
    public class RealTimeDriver : IRealTimeDriver
    {
        public static string ContainerName { get; private set; } = "MyKeyStore";
        public Dictionary<string, double> RTUs = new Dictionary<string, double>();
        private const string EXPORT_FOLDER = @"..\public_key\";
        private const string PUBLIC_KEY_FILE = @"rsaPublicKey.txt";

        public RealTimeDriver()
        {
            ImportPublicKey();
        }

        private void ImportPublicKey()
        {
            string path = Path.Combine(EXPORT_FOLDER, PUBLIC_KEY_FILE);
            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string publicKeyText = reader.ReadToEnd();
                    CspParameters csp = new CspParameters
                    {
                        KeyContainerName = ContainerName
                    };
                    using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp))
                    {
                        rsa.FromXmlString(publicKeyText);
                        rsa.PersistKeyInCsp = true;
                    }
                }
            }
            else
            {
                throw new FileNotFoundException("Public key file not found.");
            }
        }

        public void SendMessage(string signedMessage)
        {
            string[] parts = signedMessage.Split(new[] { ':' }, 2);
            byte[] signature = Convert.FromBase64String(parts[0]);
            string message = parts[1];
            byte[] hash = ComputeMessageHash(message);

            if (VerifySignedMessage(hash, signature))
            {
                string[] messageParts = message.Split(':');
                string address = messageParts[0];
                double value = double.Parse(messageParts[1]);

                RTUs[address] = value;
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

        private bool VerifySignedMessage(byte[] hash, byte[] signature)
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

        public void SubscribeRealTimeUnit(string signedMessage)
        {
            SendMessage(signedMessage);
        }

        public double GetRealTimeUnitValue(string IOAddress)
        {
            if (RTUs.ContainsKey(IOAddress))
            {
                return RTUs[IOAddress];
            }
            return Double.NegativeInfinity;
        }
    }



    //public static string ContainerName { get; private set; } = "KeyContainer";
    //public Dictionary<string,double> RTUs = new Dictionary<string,double>();

    //public void SendMessage(string message, byte[] signature)
    //{
    //    byte[] hash = ComputeMessageHash(message);

    //    if (VerifySignedMessage(hash, signature))
    //    {
    //        string[] parts = message.Split(':');
    //        string adress = parts[0];
    //        double value = double.Parse(parts[1]);

    //        RTUs[adress] = value;
    //    }
    //    else
    //    {
    //        Console.WriteLine($"Received invalid message: {message}");
    //    }
    //}

    //private static byte[] ComputeMessageHash(string value)
    //{
    //    using (SHA256 sha = SHA256.Create())
    //    {
    //        return sha.ComputeHash(Encoding.UTF8.GetBytes(value));
    //    }
    //}

    //private static bool VerifySignedMessage(byte[] hash, byte[] signature)
    //{
    //    CspParameters csp = new CspParameters
    //    {
    //        KeyContainerName = ContainerName
    //    };
    //    using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp))
    //    {
    //        var deformatter = new RSAPKCS1SignatureDeformatter(rsa);
    //        deformatter.SetHashAlgorithm("SHA256");
    //        return deformatter.VerifySignature(hash, signature);
    //    }
    //}

    //public void SubscribeRealTimeUnit(string message, byte[] signature)
    //{
    //    byte[] hash = ComputeMessageHash(message);

    //    if (VerifySignedMessage(hash, signature))
    //    {
    //        string[] parts = message.Split(':');
    //        string address = parts[0];
    //        double value = double.Parse(parts[1]);

    //        //napraviti provere da li vec postoji u xmlu itd i upisati ga u xml

    //        RTUs[address] = value;
    //    }
    //    else
    //    {
    //        Console.WriteLine($"Received invalid message: {message}");
    //    }
    //}

    //public double GetRealTimeUnitValue(string IOAdress)
    //{
    //    if (RTUs.ContainsKey(IOAdress))
    //    {
    //        return RTUs[IOAdress];  
    //    }
    //    return Double.NegativeInfinity;
    //}
}
