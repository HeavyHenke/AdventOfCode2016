using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

    internal class Day19
    {
        public void DoIt()
        {
            const int numElfs = 3012210;

            int[] presentsByElf = new int[numElfs];
            for (int i = 0; i < presentsByElf.Length; i++)
                presentsByElf[i] = 1;

            int numElfsWithPresents = numElfs;

            int elfNumsTurn = 0;
            while (numElfsWithPresents > 1)
            {
                if (elfNumsTurn >= presentsByElf.Length)
                    elfNumsTurn = 0;

                while (presentsByElf[elfNumsTurn] == 0)
                {
                    elfNumsTurn++;
                    if (elfNumsTurn >= presentsByElf.Length)
                        elfNumsTurn = 0;
                }

                // Find next elf to left
                int leftElve = elfNumsTurn;
                do
                {
                    leftElve++;
                    if (leftElve >= presentsByElf.Length)
                        leftElve = 0;
                } while (presentsByElf[leftElve] == 0);

                presentsByElf[elfNumsTurn] += presentsByElf[leftElve];
                presentsByElf[leftElve] = 0;
                numElfsWithPresents--;
                elfNumsTurn++;
            }

            for (int i = 0; i < presentsByElf.Length; i++)
            {
                if (presentsByElf[i] > 0)
                {
                    PostResult(i + 1);
                    return;
                }
            }
        }


        public void DoIt2()
        {
            const int numElfs = 3012210;
            var sw = new Stopwatch();
            sw.Start();

            int[] presentsByElf = new int[numElfs];
            for (int i = 0; i < presentsByElf.Length; i++)
                presentsByElf[i] = 1;

            int numElfsWithPresents = numElfs;

            int elfNumsTurn = 0;
            while (numElfsWithPresents > 1)
            {
                if (elfNumsTurn >= presentsByElf.Length)
                    elfNumsTurn = 0;

                while (presentsByElf[elfNumsTurn] == 0)
                {
                    elfNumsTurn++;
                    if (elfNumsTurn >= presentsByElf.Length)
                        elfNumsTurn = 0;
                }

                // Find elf to steal from
                int elfToStealFrom = numElfsWithPresents / 2;

                int elfNumToStealFrom = elfNumsTurn;
                while (elfToStealFrom > 0)
                {
                    elfNumToStealFrom++;
                    if (elfNumToStealFrom == numElfs)
                        elfNumToStealFrom = 0;
                    while (presentsByElf[elfNumToStealFrom] == 0)
                    {
                        elfNumToStealFrom++;
                        if (elfNumToStealFrom == numElfs)
                            elfNumToStealFrom = 0;
                    }

                    elfToStealFrom--;
                }


                presentsByElf[elfNumsTurn] += presentsByElf[elfNumToStealFrom];
                presentsByElf[elfNumToStealFrom] = 0;
                numElfsWithPresents--;
                elfNumsTurn++;

                if (numElfsWithPresents%1000 == 0)
                {
                    var prognos = sw.ElapsedMilliseconds*(numElfsWithPresents/1000.0);
                    Console.WriteLine($"Num elves with presents {numElfsWithPresents}, estimated ready in {prognos/(1000.0*60)} minutes");
                    sw.Restart();
                }
            }

            for (int i = 0; i < presentsByElf.Length; i++)
            {
                if (presentsByElf[i] > 0)
                {
                    PostResult(i + 1);
                    return;
                }
            }
        }

        public void DoIt2b()
        {
            const int numElfs = 3012210;
            var sw = new Stopwatch();
            sw.Start();

            var presentsByElf = new LinkedList<Tuple<int,int>>();

            for (int i = 0; i < numElfs; i++)
            {
                presentsByElf.AddLast(Tuple.Create(i, 1));
            }

            int numElfsWithPresents = numElfs;

            LinkedListNode<Tuple<int, int>> elfsTurn = presentsByElf.First;
            int elfIx = 0;
            while (numElfsWithPresents > 1)
            {
                // Find elf to steal from
                int distToElfToStealFrom = numElfsWithPresents / 2;
                LinkedListNode<Tuple<int, int>> elfToStealFrom;

                if (distToElfToStealFrom + elfIx >= numElfsWithPresents)
                {
                    distToElfToStealFrom -= numElfsWithPresents - elfIx;
                    elfToStealFrom = presentsByElf.First;
                    elfIx--;
                }
                else
                {
                    elfToStealFrom = elfsTurn;
                }

                while (distToElfToStealFrom-- > 0)
                {
                    elfToStealFrom = elfToStealFrom.Next ?? presentsByElf.First;
                }


                elfsTurn.Value = Tuple.Create(elfsTurn.Value.Item1, elfsTurn.Value.Item2 + elfToStealFrom.Value.Item2);
                presentsByElf.Remove(elfToStealFrom);

                numElfsWithPresents--;


                if (numElfsWithPresents % 1000 == 0)
                {
                    var prognos = sw.ElapsedMilliseconds * (numElfsWithPresents / 1000.0);
                    Console.WriteLine($"Num elves with presents {numElfsWithPresents}, estimated ready in {prognos / (1000.0 * 60)} minutes");
                    sw.Restart();
                }

                elfsTurn = elfsTurn.Next ?? presentsByElf.First;
                elfIx++;
                if (elfIx == numElfsWithPresents)
                    elfIx = 0;
            }

            PostResult(presentsByElf.First.Value.Item1 + 1);
        }
    }

}
