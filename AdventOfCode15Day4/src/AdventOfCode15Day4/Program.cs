using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode15Day4
{
    public class Program
    {
        public void Main(string[] args)
        {
            Calculate("00000");
            Calculate("000000");
            Console.ReadKey();

        }

        public static void Calculate(string startsWith)
        {
            var i = 0;
            var found = false;
            string md5 = "";
            while (!found)
            {
                i++;
                md5 = CreateMD5("ckczppom" + i);
                found = md5.StartsWith(startsWith);

            }
            Console.WriteLine(md5 + " / " + i);
            
        }
        /// <summary>
        /// Copied from: http://stackoverflow.com/a/24031467
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
