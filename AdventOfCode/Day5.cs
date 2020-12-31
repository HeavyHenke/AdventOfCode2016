using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace AdventOfCode
{
    public class Day5
    {
        public void DoItA()
        {
            string input = "uqwqemis";
            int addon = 0;

            char[] pwd = "        ".ToCharArray();


            var md5 = MD5.Create();
            string hex = "0123456789abcdef";
            int pwdIx = 0;

            while (pwd.Count(d => d == ' ') > 0)
            {
                string test = input + addon;
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(test));
                if (hash[0] == 0 && hash[1] == 0 && (hash[2] & 0xF0) == 0)
                {
                    pwd[pwdIx++] = hash.ToHex()[5];
                    Console.WriteLine(new string(pwd) + " " + addon);
                }

                addon++;
            }

            PostResult(new string(pwd));
        }

        public void DoItB()
        {
            string input = "uqwqemis";
            int addon = 0;

            char[] pwd = "        ".ToCharArray();


            var md5 = MD5.Create();
            string hex = "0123456789abcdef";

            while (pwd.Count(d => d == ' ') > 0)
            {
                string test = input + addon;
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(test));
                if (hash[0] == 0 && hash[1] == 0 && (hash[2] & 0xF0) == 0)
                {
                    if (hash[2] <= 7)
                    {
                        if (pwd[hash[2]] == ' ')
                        {
                            pwd[hash[2]] = hex[hash[3] >> 4];
                        }
                    }
                    Console.WriteLine(new string(pwd) + " " + addon);
                }

                addon++;
            }

            PostResult(new string(pwd));
        }

        private void PostResult(object res)
        {
            var str = res.ToString();
            Clipboard.SetText(str);
            Console.WriteLine(str);
            Console.ReadKey();
        }
    }
}