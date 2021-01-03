using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    internal class Day14
    {
        //private readonly MD5 _md5 = MD5.Create();
        private readonly HashAlgorithm _md52 = new MD5Managed();
        private string _salt = "yjdafjpo";
        private readonly List<string> _computedHashes = new List<string>();

        public void DoItA()
        {
            var tripple = new Regex(@"(\w)\1\1", RegexOptions.Compiled);
            int i = 0;
            int numFound = 0;
            while (true)
            {
                var hash = GetMd5A(i);
                var m = tripple.Match(hash);
                if (m.Success)
                {
                    for (int j = i + 1; j < i + 1001; j++)
                    {
                        var toFind = new string(m.Groups[0].Value[0], 5);
                        if(GetMd5A(j).Contains(toFind))
                        {
                            numFound++;
                            Console.WriteLine($"Found {numFound} at ix {i}");
                            break;
                        }
                    }

                    if (numFound == 64)
                    {
                        Tools.PostResult(i);
                        return;
                    }
                }

                i++;
            }
        }

        public void DoItB()
        {
            var tripple = new Regex(@"(\w)\1\1", RegexOptions.Compiled);
            int i = 0;
            int numFound = 0;
            while (true)
            {
                var hash = GetMd5B(i);
                var m = tripple.Match(hash);
                if (m.Success)
                {
                    for (int j = i + 1; j < i + 1001; j++)
                    {
                        var toFind = new string(m.Groups[0].Value[0], 5);
                        if (GetMd5B(j).Contains(toFind))
                        {
                            numFound++;
                            Console.WriteLine($"Found {numFound} at ix {i}");
                            break;
                        }
                    }

                    if (numFound == 64)
                    {
                        Tools.PostResult(i);
                        return;
                    }
                }

                i++;
            }
        }


        private string GetMd5A(int ix)
        {
            if (ix < _computedHashes.Count)
                return _computedHashes[ix];
            if (ix != _computedHashes.Count)
                throw new Exception("knas");

            string val = _salt + ix;
            var hash = _md52.ComputeHash(Encoding.ASCII.GetBytes(val));
            var hex = hash.ToHex();
            _computedHashes.Add(hex);
            return hex;
        }

        private string GetMd5B(int ix)
        {
            if (ix < _computedHashes.Count)
                return _computedHashes[ix];

            if (ix != _computedHashes.Count)
                throw new Exception("knas");

            string val = _salt + ix;
            var hash = _md52.ComputeHash(Encoding.ASCII.GetBytes(val));
            for (int i = 0; i < 2016; i++)
                hash = _md52.ComputeHash(Encoding.ASCII.GetBytes(hash.ToHex()));

            var hex = hash.ToHex();
            _computedHashes.Add(hex);
            return hex;
        }
    }
}