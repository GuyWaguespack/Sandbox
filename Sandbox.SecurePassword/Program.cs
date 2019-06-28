using System;
using System.Security;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.IO;


namespace Sandbox.SecurePassword
{
    class Program
    {
        private static string defaultKey = "GuyWaguespack042GuyWaguespack042";
        private static byte[] baseKey { get { return Encoding.Default.GetBytes(defaultKey); } }

        static void Main(string[] args)
        {
            //Console.Write(">> Enter String To Secure : ");
            //String str = Console.ReadLine();

            using (RijndaelManaged myRijndael = new RijndaelManaged())
            {
                Console.WriteLine($">> User   : [{Environment.UserName}@{Environment.UserDomainName}]");
                Console.WriteLine($">> Host   : [{Environment.MachineName}]");

                //myRijndael.GenerateKey();
                //Console.WriteLine($">> Key        : [{ToString(myRijndael.Key)}]");
                //Console.WriteLine($">> Key Length : {myRijndael.KeySize}");

                //myRijndael.Key = ToBytes(Environment.UserName + "@" + Environment.UserDomainName + Environment.MachineName);
                myRijndael.Key = baseKey;
                Console.WriteLine($">> Key    : [{ToString(myRijndael.Key)}]");
                myRijndael.GenerateIV();
                Console.WriteLine($">> Vector : [{ToString(myRijndael.IV)}]");

            }
        }

        static byte[] ToBytes(string str)
        {
            return Encoding.Default.GetBytes(str);
        }

        static string ToString(byte[] bytes)
        {
            return Encoding.Default.GetString(bytes);
        }

        static SecureString EncodeString(string str)
        {
            SecureString ss = new SecureString();

            for (int i = 0; i < str.Length; i++)
                ss.AppendChar(str[i]);

            return ss;
        }

        static string DecodeString(SecureString ss)
        {
            var bstr = Marshal.SecureStringToBSTR(ss);
            string str = Marshal.PtrToStringAuto(bstr);
            return str;
        }
    }
}
