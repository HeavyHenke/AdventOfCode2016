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

        private readonly List<Tuple<int, string>> _fiveInARows = new List<Tuple<int, string>>();
        private int _fiveInARowsFoundToIx = -1;

        private string _salt = "yjdafjpo";

        private readonly Regex _pentuple = new Regex(@"(\w)\1\1\1\1", RegexOptions.Compiled);

        private readonly List<string> _computedHashes = new List<string>();

        public void DoIt()
        {
            var tripple = new Regex(@"(\w)\1\1", RegexOptions.Compiled);
            int i = 0;
            int numFound = 0;
            while (true)
            {
                var hash = GetMd5(i);
                var m = tripple.Match(hash);
                if (m.Success)
                {
                    var found5 = FindNext10005InARowsMatch(i + 1, m.Captures[0].Value[0]);
                    if (found5)
                    {
                        numFound++;
                        Console.WriteLine($"Found {numFound} at ix {i}");
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

        private bool FindNext10005InARowsMatch(int startIx, char charToFind)
        {
            int i = startIx;
            string toFind = new string(charToFind, 5);

            while (true)
            {
                var nextPentuple = Next5InARow(i);
                if (nextPentuple.Item1 >= startIx + 1000)
                    return false;
                if (nextPentuple.Item2.Contains(toFind))
                    return true;
                i = nextPentuple.Item1 + 1;
            }
        }

        private Tuple<int, string> Next5InARow(int startAt)
        {
            if (startAt <= _fiveInARowsFoundToIx)
            {
                for (int index = 0; index < _fiveInARows.Count; index++)
                {
                    var fiveInARow = _fiveInARows[index];
                    if (fiveInARow.Item1 >= startAt)
                        return fiveInARow;
                }
            }

            int i = _fiveInARowsFoundToIx + 1;
            while (true)
            {
                var hash = GetMd5(i);
                if (_pentuple.IsMatch(hash))
                {
                    var tpl = Tuple.Create(i, hash);
                    _fiveInARows.Add(tpl);
                    _fiveInARowsFoundToIx = i;
                    return tpl;
                }

                i++;
            }
        }


        private string GetMd5(int ix)
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