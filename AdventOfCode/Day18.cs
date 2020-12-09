using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    internal class Day18
    {
        public void DoIt()
        {
            var input = "..^^^^^.^^.^^^.^...^..^^.^.^..^^^^^^^^^^..^...^^.^..^^^^..^^^^...^.^.^^^^^^^^....^..^^^^^^.^^^.^^^.^^.".ToList();
            //var input = "..^^.^.^^^^.".ToList();
            int numSafe = 0;

            var prevRow = input;
            for (int row = 0; row < 400000; row++)
            {
                numSafe += prevRow.Count(c => c == '.') - 2;

                List<char> nextRow = new List<char>();
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

            // 1938 too low
            // 1973 too low
            Tools.PostResult(numSafe);
        }
    }
}