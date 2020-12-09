using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Numerics;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.XPath;
using MoreLinq;
using static AdventOfCode.Tools;
using static System.Math;

namespace AdventOfCode
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            new Day20().DoItB();
        }
    }

    [DebuggerDisplay("({X}, {Y})")]
    public class Coord
    {
        public int X { get; }
        public int Y { get; }

        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Coord MoveX(int delta)
        {
            return new Coord(X + delta, Y);
        }

        public Coord MoveY(int delta)
        {
            return new Coord(X, Y + delta);
        }

        public override int GetHashCode()
        {
            return (X & 0xFFFF) | (Y << 16);
        }

        private bool Equals(Coord other)
        {
            return X == other.X && Y == other.Y;
        }

        public static bool operator ==(Coord left, Coord right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Coord left, Coord right)
        {
            return !Equals(left, right);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Coord;
            if (other == null)
                return false;
            return Equals(other);
        }
    }

    public static class Tools
    {
        public static void PostResult(object res)
        {
            var str = res.ToString();
            Clipboard.SetText(str);
            Console.WriteLine(str);
            Console.ReadKey();
        }

        public static string[] SplitLines(this string inp)
        {
            return inp.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static int ParseInt(this string inp)
        {
            return int.Parse(inp);
        }

        public static string ToHex(this byte[] inp)
        {
            var chrArr = new char[inp.Length * 2];
            int charArrIx = 0;
            const string hex = "0123456789abcdef";
            for (int i = 0; i < inp.Length; i++)
            {
                byte b = inp[i];
                chrArr[charArrIx++] = hex[b >> 4];
                chrArr[charArrIx++] = hex[b & 0xf];
            }

            return new string(chrArr);
        }
    }
}
