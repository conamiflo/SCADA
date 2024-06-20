using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Driver
{
    public class RealTimeDriver : IRealTimeDriver
    {
        public static string ContainerName { get; private set; } = "KeyContainer";

        public void SendMessage(string message, byte[] signature)
        {
            byte[] hash = ComputeMessageHash(message);

            if (VerifySignedMessage(hash, signature))
            {
                Console.WriteLine($"Received valid message: {message}");
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
    }
}