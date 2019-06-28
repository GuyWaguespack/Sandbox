using System;
using System.Security.Cryptography;
using System.Xml;
using System.IO;
using System.Text;

namespace Sandbox.Crypto
{
    class Program
    {
        static void Main(string[] args)
        {
            string pubPriv = "/Users/guy/temp/SyncoDeCosto.pubPriv";
            string pubOnly = "/Users/guy/temp/SyncoDeCosto.pubOnly";

            string plainText = "Hello World";

            // Generate Keys
            RSA rsa = RSA.Create();
            ToXmlFile(rsa, true, pubPriv);
            ToXmlFile(rsa, false, pubOnly);

            Console.WriteLine(rsa.KeyExchangeAlgorithm);
            Console.WriteLine(rsa.KeySize);
            Console.WriteLine(rsa.SignatureAlgorithm);


            // Encrypt Text
            RSA publicKey = FromXmlFile(pubOnly);
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] encryptedBytes = publicKey.Encrypt(plainBytes, RSAEncryptionPadding.OaepSHA512);
            string encryptedText = Convert.ToBase64String(encryptedBytes);
            Console.WriteLine(encryptedText);

            // Decrypt Text
            RSA privateKey = FromXmlFile(pubPriv);
            byte[] tempBytes = Convert.FromBase64String(encryptedText);
            byte[] decryptedBytes = privateKey.Decrypt(tempBytes, RSAEncryptionPadding.OaepSHA512);
            string decryptedString = Encoding.UTF8.GetString(decryptedBytes);
            Console.WriteLine(decryptedString);

        }

        // https://dejanstojanovic.net/aspnet/2018/june/loading-rsa-key-pair-from-pem-files-in-net-core-with-c/
        public static RSA FromXmlFile(string xmlFilePath)
        {
            RSAParameters parameters = new RSAParameters();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(File.ReadAllText(xmlFilePath));

            if (xmlDoc.DocumentElement.Name.Equals("RSAKeyValue"))
            {
                foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "Modulus":
                            if (!string.IsNullOrEmpty(node.InnerText))
                                parameters.Modulus = Convert.FromBase64String(node.InnerText);
                            break;

                        case "Exponent":
                            if (!string.IsNullOrEmpty(node.InnerText))
                                parameters.Exponent = Convert.FromBase64String(node.InnerText);
                            break;

                        case "P":
                            if (!string.IsNullOrEmpty(node.InnerText))
                                parameters.P = Convert.FromBase64String(node.InnerText);
                            break;

                        case "Q":
                            if (!string.IsNullOrEmpty(node.InnerText))
                                parameters.Q = Convert.FromBase64String(node.InnerText);
                            break;

                        case "DP":
                            if (!string.IsNullOrEmpty(node.InnerText))
                                parameters.DP = Convert.FromBase64String(node.InnerText);
                            break;

                        case "DQ":
                            if (!string.IsNullOrEmpty(node.InnerText))
                                parameters.DQ = Convert.FromBase64String(node.InnerText);
                            break;

                        case "InverseQ":
                            if (!string.IsNullOrEmpty(node.InnerText))
                                parameters.InverseQ = Convert.FromBase64String(node.InnerText);
                            break;

                        case "D":
                            if (!string.IsNullOrEmpty(node.InnerText))
                                parameters.D = Convert.FromBase64String(node.InnerText);
                            break;

                    }
                }
            }
            else
            {
                throw new Exception("Invalid XML RSA key.");
            }

            return RSA.Create(parameters);
        }


        public static void ToXmlFile(RSA rsa, bool includePrivateParameters, string xmlFilePath)
        {
            RSAParameters parameters = rsa.ExportParameters(includePrivateParameters);

            StringBuilder sb = new StringBuilder();
            sb.Append("<RSAKeyValue>");

            if (parameters.Modulus != null)
                sb.Append($"<Modulus>{ Convert.ToBase64String(parameters.Modulus) }</Modulus>");

            if (parameters.Exponent != null)
                sb.Append($"<Exponent>{ Convert.ToBase64String(parameters.Exponent) }</Exponent>");

            if (parameters.P != null)
                sb.Append($"<P>{ Convert.ToBase64String(parameters.P) }</P>");

            if (parameters.Q != null)
                sb.Append($"<Q>{ Convert.ToBase64String(parameters.Q) }</Q>");

            if (parameters.DP != null)
                sb.Append($"<DP>{ Convert.ToBase64String(parameters.DP) }</DP>");

            if (parameters.DQ != null)
                sb.Append($"<DQ>{ Convert.ToBase64String(parameters.DQ) }</DQ>");

            if (parameters.InverseQ != null)
                sb.Append($"<InverseQ>{ Convert.ToBase64String(parameters.InverseQ) }</InverseQ>");

            if (parameters.D != null)
                sb.Append($"<D>{ Convert.ToBase64String(parameters.D) }</D>");

            sb.Append("</RSAKeyValue>");

            File.WriteAllText(xmlFilePath, sb.ToString());
        }
    }
}
