using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {

        static void Main(string[] args)
        {
            byte[] iv = new byte[8];
            byte[] salt = new byte[8];

            string msg = "Hello world Hello world Hello world Hello world Hello world Hello world Hello world Hello world Hello world Hello world ";
            Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes("password", salt, 1);



            TripleDES td = TripleDES.Create();
            td.Key = rfc.GetBytes(16);
            td.IV = iv;
            MemoryStream ms = new MemoryStream();
            CryptoStream encrypt = new CryptoStream(ms, td.CreateEncryptor(), CryptoStreamMode.Write);
            byte[] bmsg = new System.Text.UTF8Encoding(false).GetBytes(msg);
            //byte[] bmsg = File.ReadAllBytes("test.txt"); 
            encrypt.Write(bmsg, 0, bmsg.Length);
            encrypt.FlushFinalBlock();
            encrypt.Close();
            rfc.Reset();
            Console.WriteLine(String.Join(" ", ms.ToArray()));


            byte[] by = ms.ToArray();
            TripleDES td2 = TripleDES.Create();
            td2.IV = iv;
            td2.Key = rfc.GetBytes(16);
            MemoryStream ms2 = new MemoryStream();
            CryptoStream encrypt2 = new CryptoStream(ms2, td2.CreateDecryptor(), CryptoStreamMode.Write);

            encrypt2.Write(by, 0, by.Length);
            encrypt2.FlushFinalBlock();
            encrypt2.Close();
            Console.WriteLine(String.Join(" ", new System.Text.UTF8Encoding(false).GetString(ms2.ToArray())));

        }
    }
}
