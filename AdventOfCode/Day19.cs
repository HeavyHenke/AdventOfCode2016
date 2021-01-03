using System.Collections.Generic;
using static AdventOfCode.Tools;

namespace AdventOfCode
{

    internal class Day19
    {
        public void DoItA()
        {
            const int numElves = 3005290;

            int[] presentsByElf = new int[numElves];
            for (int i = 0; i < presentsByElf.Length; i++)
                presentsByElf[i] = 1;

            int numElvesWithPresents = numElves;

            int elfNumsTurn = 0;
            while (numElvesWithPresents > 1)
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
                int leftElf = elfNumsTurn;
                do
                {
                    leftElf++;
                    if (leftElf >= presentsByElf.Length)
                        leftElf = 0;
                } while (presentsByElf[leftElf] == 0);

                presentsByElf[elfNumsTurn] += presentsByElf[leftElf];
                presentsByElf[leftElf] = 0;
                numElvesWithPresents--;
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

        public void DoItB()
        {
            const int numElves = 3005290;

            var elves = new LinkedList<int>();
            LinkedListNode<int> opposite = null;

            for (int i = 0; i < numElves; i++)
            {
                elves.AddLast(i + 1);
                if (i == numElves / 2)
                    opposite = elves.Last;
            }

            while (elves.Count > 1)
            {
                var next = opposite.Next ?? elves.First;
                elves.Remove(opposite);

                if ((elves.Count % 2) == 0)
                    opposite = next.Next ?? elves.First;
                else
                    opposite = next;
            }

            PostResult(elves.First.Value);
        }

    }

}
