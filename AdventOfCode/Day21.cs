using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    internal class Day21
    {
        public string DoItA()
        {
            List<char> password = "abcdefgh".ToList();

            var swapPosReg = new Regex(@"swap position (?<pos1>\d+) with position (?<pos2>\d+)", RegexOptions.Compiled);
            var swapLetterReg = new Regex(@"swap letter (?<pos1>\w) with letter (?<pos2>\w)", RegexOptions.Compiled);
            var reverseReg = new Regex(@"reverse positions (?<pos1>\d+) through (?<pos2>\d+)", RegexOptions.Compiled);
            var rotReg = new Regex(@"rotate (left|right) (?<num>\d+) steps?", RegexOptions.Compiled);
            var twoValueRegEx = new Regex(@"\D*(?<pos1>\d+)\D+(?<pos2>\d+)", RegexOptions.Compiled);

            foreach (var row in _data.SplitLines())
            {
                if (row.StartsWith("swap position"))
                {
                    var m = swapPosReg.Match(row);
                    var ix1 = int.Parse(m.Groups["pos1"].Value);
                    var ix2 = int.Parse(m.Groups["pos2"].Value);
                    char c = password[ix1];
                    password[ix1] = password[ix2];
                    password[ix2] = c;
                }

                else if (row.StartsWith("swap letter"))
                {
                    var m = swapLetterReg.Match(row);
                    var c1 = m.Groups["pos2"].Value[0];
                    var c2 = m.Groups["pos1"].Value[0];
                    for (int i = 0; i < password.Count; i++)
                    {
                        if (password[i] == c1)
                        {
                            password[i] = c2;
                        }
                        else if (password[i] == c2)
                            password[i] = c1;
                    }
                }

                else if (row.StartsWith("reverse"))
                {
                    var m = reverseReg.Match(row);
                    var start = int.Parse(m.Groups["pos1"].Value);
                    var stop = int.Parse(m.Groups["pos2"].Value);
                    var rev = password.Skip(start).Take(stop - start + 1).Reverse().ToList();
                    int revIx = 0;
                    for (int i = start; i <= stop; i++)
                    {
                        password[i] = rev[revIx++];
                    }
                }

                else if (row.StartsWith("rotate left"))
                {
                    var m = rotReg.Match(row);
                    var num = int.Parse(m.Groups["num"].Value);
                    while (num > 0)
                    {
                        var chr = password[0];
                        password.RemoveAt(0);
                        password.Add(chr);
                        num--;
                    }
                }

                else if (row.StartsWith("rotate right"))
                {
                    var m = rotReg.Match(row);
                    var num = int.Parse(m.Groups["num"].Value);
                    while (num > 0)
                    {
                        var chr = password[password.Count-1];
                        password.RemoveAt(password.Count - 1);
                        password.Insert(0, chr);
                        num--;
                    }
                }

                else if (row.StartsWith("move position"))
                {
                    var m = twoValueRegEx.Match(row);
                    var num1 = int.Parse(m.Groups["pos1"].Value);
                    var num2 = int.Parse(m.Groups["pos2"].Value);
                    var chr = password[num1];
                    password.RemoveAt(num1);
                    password.Insert(num2, chr);
                }

                else if (row.StartsWith("rotate based"))
                {
                    var chr = row[row.Length - 1];
                    var ix = password.IndexOf(chr);
                    var numRot = 1 + ix + (ix >= 4 ? 1 : 0);
                    while (numRot > 0)
                    {
                        chr = password[password.Count - 1];
                        password.RemoveAt(password.Count - 1);
                        password.Insert(0, chr);
                        numRot--;
                    }
                }
            }

            var pwd = new string(password.ToArray());
            Tools.PostResult(pwd);
            return pwd;
        }

        public string DoItB()
        {
            List<char> password = "fbgdceah".ToList();

            var swapPosReg = new Regex(@"swap position (?<pos1>\d+) with position (?<pos2>\d+)", RegexOptions.Compiled);
            var swapLetterReg = new Regex(@"swap letter (?<pos1>\w) with letter (?<pos2>\w)", RegexOptions.Compiled);
            var reverseReg = new Regex(@"reverse positions (?<pos1>\d+) through (?<pos2>\d+)", RegexOptions.Compiled);
            var rotReg = new Regex(@"rotate (left|right) (?<num>\d+) steps?", RegexOptions.Compiled);
            var twoValueRegEx = new Regex(@"\D*(?<pos1>\d+)\D+(?<pos2>\d+)", RegexOptions.Compiled);

            foreach (var row in _data.SplitLines().Reverse())
            {
                if (row.StartsWith("swap position"))
                {
                    var m = swapPosReg.Match(row);
                    var ix1 = int.Parse(m.Groups["pos1"].Value);
                    var ix2 = int.Parse(m.Groups["pos2"].Value);
                    char c = password[ix1];
                    password[ix1] = password[ix2];
                    password[ix2] = c;
                }

                else if (row.StartsWith("swap letter"))
                {
                    var m = swapLetterReg.Match(row);
                    var c1 = m.Groups["pos2"].Value[0];
                    var c2 = m.Groups["pos1"].Value[0];
                    for (int i = 0; i < password.Count; i++)
                    {
                        if (password[i] == c1)
                        {
                            password[i] = c2;
                        }
                        else if (password[i] == c2)
                            password[i] = c1;
                    }
                }

                else if (row.StartsWith("reverse"))
                {
                    var m = reverseReg.Match(row);
                    var start = int.Parse(m.Groups["pos1"].Value);
                    var stop = int.Parse(m.Groups["pos2"].Value);
                    var rev = password.Skip(start).Take(stop - start + 1).Reverse().ToList();
                    int revIx = 0;
                    for (int i = start; i <= stop; i++)
                    {
                        password[i] = rev[revIx++];
                    }
                }

                else if (row.StartsWith("rotate left"))
                {
                    var m = rotReg.Match(row);
                    var num = int.Parse(m.Groups["num"].Value);
                    while (num > 0)
                    {
                        var chr = password[password.Count - 1];
                        password.RemoveAt(password.Count - 1);
                        password.Insert(0, chr);
                        num--;
                    }
                }

                else if (row.StartsWith("rotate right"))
                {
                    var m = rotReg.Match(row);
                    var num = int.Parse(m.Groups["num"].Value);
                    while (num > 0)
                    {
                        var chr = password[0];
                        password.RemoveAt(0);
                        password.Add(chr);
                        num--;
                    }
                }

                else if (row.StartsWith("move position"))
                {
                    var m = twoValueRegEx.Match(row);
                    var num2 = int.Parse(m.Groups["pos1"].Value);
                    var num1 = int.Parse(m.Groups["pos2"].Value);
                    var chr = password[num1];
                    password.RemoveAt(num1);
                    password.Insert(num2, chr);
                }

                else if (row.StartsWith("rotate based"))
                {
                    var chr = row[row.Length - 1];
                    
                    var rotNum = 0;
                    while (true)
                    {
                        var rotChr = password[0];
                        password.RemoveAt(0);
                        password.Add(rotChr);
                        rotNum++;

                        var ix = password.IndexOf(chr);
                        var toRotate = 1 + ix + (ix >= 4 ? 1 : 0);
                        if(rotNum == toRotate)
                            break;

                        if(rotNum > password.Count * 10)
                            throw new Exception("knas vid: " + row);
                    }
                }
            }

            var pwd = new string(password.ToArray());
            Tools.PostResult(pwd);
            return pwd;
        }

        private const string _testData = @"swap position 4 with position 0
swap letter d with letter b
reverse positions 0 through 4
rotate left 1 step
move position 1 to position 4
move position 3 to position 0
rotate based on position of letter b
rotate based on position of letter d";

        public const string _data = @"swap position 7 with position 1
swap letter e with letter d
swap position 7 with position 6
move position 4 to position 0
move position 1 to position 4
move position 6 to position 5
rotate right 1 step
swap letter e with letter b
reverse positions 3 through 7
swap position 2 with position 6
reverse positions 2 through 4
reverse positions 1 through 4
reverse positions 5 through 7
rotate left 2 steps
swap letter g with letter f
rotate based on position of letter a
swap letter b with letter h
swap position 0 with position 3
move position 4 to position 7
rotate based on position of letter g
swap letter f with letter e
move position 1 to position 5
swap letter d with letter e
move position 5 to position 2
move position 6 to position 5
rotate right 6 steps
rotate left 4 steps
reverse positions 0 through 3
swap letter g with letter c
swap letter f with letter e
reverse positions 6 through 7
move position 6 to position 1
rotate left 2 steps
rotate left 5 steps
swap position 3 with position 6
reverse positions 1 through 5
rotate right 6 steps
swap letter a with letter b
reverse positions 3 through 4
rotate based on position of letter f
swap position 2 with position 6
reverse positions 5 through 6
swap letter h with letter e
reverse positions 0 through 4
rotate based on position of letter g
rotate based on position of letter d
rotate based on position of letter b
swap position 5 with position 1
rotate based on position of letter f
move position 1 to position 5
rotate right 0 steps
rotate based on position of letter e
move position 0 to position 1
swap position 7 with position 2
rotate left 3 steps
reverse positions 0 through 1
rotate right 7 steps
rotate right 5 steps
swap position 2 with position 0
swap letter g with letter a
rotate left 0 steps
rotate based on position of letter f
swap position 5 with position 1
rotate right 0 steps
rotate left 5 steps
swap letter e with letter a
swap position 5 with position 4
reverse positions 2 through 5
swap letter e with letter a
swap position 3 with position 7
reverse positions 0 through 2
swap letter a with letter b
swap position 7 with position 1
move position 1 to position 6
rotate right 1 step
reverse positions 2 through 6
rotate based on position of letter b
move position 1 to position 0
swap position 7 with position 3
move position 6 to position 5
rotate right 4 steps
reverse positions 2 through 7
reverse positions 3 through 4
reverse positions 4 through 5
rotate based on position of letter f
reverse positions 0 through 5
reverse positions 3 through 4
move position 1 to position 2
rotate left 4 steps
swap position 7 with position 6
rotate right 1 step
move position 5 to position 2
rotate right 5 steps
swap position 7 with position 4
swap letter a with letter e
rotate based on position of letter e
swap position 7 with position 1
swap position 7 with position 3
move position 7 to position 1
swap position 7 with position 4";

    }
}