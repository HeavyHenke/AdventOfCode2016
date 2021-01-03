using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    internal class Day18
    {
        public void DoItA()
        {
            var input = "^.^^^..^^...^.^..^^^^^.....^...^^^..^^^^.^^.^^^^^^^^.^^.^^^^...^^...^^^^.^.^..^^..^..^.^^.^.^.......".ToList();
            input.Insert(0, '.');
            input.Add('.');
            int numSafe = 0;

            var prevRow = input;
            for (int row = 0; row < 40; row++)
            {
                numSafe += prevRow.Count(c => c == '.') - 2;

                List<char> nextRow = new List<char>();
                nextRow.Add('.');
                for (int col = 1; col < input.Count - 1; col++)
                {
                    if (prevRow[col - 1] == '^' && prevRow[col] == '^' && prevRow[col + 1] == '.')
                        nextRow.Add('^');
                    else if (prevRow[col - 1] == '.' && prevRow[col] == '^' && prevRow[col + 1] == '^')
                        nextRow.Add('^');
                    else if (prevRow[col - 1] == '^' && prevRow[col] == '.' && prevRow[col + 1] == '.')
                        nextRow.Add('^');
                    else if (prevRow[col - 1] == '.' && prevRow[col] == '.' && prevRow[col + 1] == '^')
                        nextRow.Add('^');
                    else
                        nextRow.Add('.');
                }

                nextRow.Add('.');

                prevRow = nextRow;
            }

            Tools.PostResult(numSafe);
        }

        public void DoItB()
        {
            var input = "^.^^^..^^...^.^..^^^^^.....^...^^^..^^^^.^^.^^^^^^^^.^^.^^^^...^^...^^^^.^.^..^^..^..^.^^.^.^.......".ToList();
            input.Insert(0, '.');
            input.Add('.');
            int numSafe = 0;

            var prevRow = input;
            for (int row = 0; row < 400000; row++)
            {
                numSafe += prevRow.Count(c => c == '.') - 2;

                List<char> nextRow = new List<char>(prevRow.Count);
                nextRow.Add('.');
                for (int col = 1; col < input.Count - 1; col++)
                {
                    if (prevRow[col - 1] == '^' && prevRow[col] == '^' && prevRow[col + 1] == '.')
                        nextRow.Add('^');
                    else if(prevRow[col - 1] == '.' && prevRow[col] == '^' && prevRow[col + 1] == '^')
                        nextRow.Add('^');
                    else if(prevRow[col - 1] == '^' && prevRow[col] == '.' && prevRow[col + 1] == '.')
                        nextRow.Add('^');
                    else if (prevRow[col - 1] == '.' && prevRow[col] == '.' && prevRow[col + 1] == '^')
                        nextRow.Add('^');
                    else
                        nextRow.Add('.');
                }

                nextRow.Add('.');

                prevRow = nextRow;
            }

            Tools.PostResult(numSafe);
        }
    }
}