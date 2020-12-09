using System;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day8
    {
        public void DoIt()
        {
            var screen = new bool[6, 50];
            var rectCmd = new Regex(@"rect (\d+)x(\d+)");
            var rotateRowCmd = new Regex(@"rotate row y=(\d+) by (\d+)");
            var rotateColCmd = new Regex(@"rotate column x=(\d+) by (\d+)");

            for (int y = 0; y < 6; y++)
                for (int x = 0; x < 50; x++)
                    screen[y, x] = false;

            foreach (var command in _input.Split('\n'))
            {
                //Print(screen);

                var rect = rectCmd.Match(command);
                if (rect.Success)
                {
                    int a = int.Parse(rect.Groups[1].Value);
                    int b = int.Parse(rect.Groups[2].Value);
                    for (int x = 0; x < a; x++)
                        for (int y = 0; y < b; y++)
                            screen[y, x] = true;
                    continue;
                }

                var rotCol = rotateColCmd.Match(command);
                if (rotCol.Success)
                {
                    int col = int.Parse(rotCol.Groups[1].Value);
                    int times = int.Parse(rotCol.Groups[2].Value);
                    for(int i = 0; i < times; i++)
                        RotateCol(screen, col);
                    continue;
                }

                var rotRow = rotateRowCmd.Match(command);
                if(rotRow.Success == false)
                    throw new Exception("knas");

                int row = int.Parse(rotRow.Groups[1].Value);
                int tms = int.Parse(rotRow.Groups[2].Value);
                for (int i = 0; i < tms; i++)
                    RotateRow(screen, row);
            }

            Print(screen);
            Console.ReadKey();
        }

        private void RotateCol(bool[,] screen, int col)
        {
            var last = screen[5, col];
            for (int r = 5; r > 0; r--)
                screen[r, col] = screen[r - 1, col];
            screen[0, col] = last;
        }

        private void RotateRow(bool[,] screen, int row)
        {
            var last = screen[row, 49];
            for (int c = 49; c > 0; c--)
            {
                screen[row, c] = screen[row, c - 1];
            }
            screen[row, 0] = last;
        }

        private string _inputTest = @"rect 3x2
rotate column x=1 by 1
rotate row y=0 by 4
rotate column x=1 by 1";


        private string _input = @"rect 1x1
rotate row y=0 by 5
rect 1x1
rotate row y=0 by 6
rect 1x1
rotate row y=0 by 5
rect 1x1
rotate row y=0 by 2
rect 1x1
rotate row y=0 by 5
rect 2x1
rotate row y=0 by 2
rect 1x1
rotate row y=0 by 4
rect 1x1
rotate row y=0 by 3
rect 2x1
rotate row y=0 by 7
rect 3x1
rotate row y=0 by 3
rect 1x1
rotate row y=0 by 3
rect 1x2
rotate row y=1 by 13
rotate column x=0 by 1
rect 2x1
rotate row y=0 by 5
rotate column x=0 by 1
rect 3x1
rotate row y=0 by 18
rotate column x=13 by 1
rotate column x=7 by 2
rotate column x=2 by 3
rotate column x=0 by 1
rect 17x1
rotate row y=3 by 13
rotate row y=1 by 37
rotate row y=0 by 11
rotate column x=7 by 1
rotate column x=6 by 1
rotate column x=4 by 1
rotate column x=0 by 1
rect 10x1
rotate row y=2 by 37
rotate column x=19 by 2
rotate column x=9 by 2
rotate row y=3 by 5
rotate row y=2 by 1
rotate row y=1 by 4
rotate row y=0 by 4
rect 1x4
rotate column x=25 by 3
rotate row y=3 by 5
rotate row y=2 by 2
rotate row y=1 by 1
rotate row y=0 by 1
rect 1x5
rotate row y=2 by 10
rotate column x=39 by 1
rotate column x=35 by 1
rotate column x=29 by 1
rotate column x=19 by 1
rotate column x=7 by 2
rotate row y=4 by 22
rotate row y=3 by 5
rotate row y=1 by 21
rotate row y=0 by 10
rotate column x=2 by 2
rotate column x=0 by 2
rect 4x2
rotate column x=46 by 2
rotate column x=44 by 2
rotate column x=42 by 1
rotate column x=41 by 1
rotate column x=40 by 2
rotate column x=38 by 2
rotate column x=37 by 3
rotate column x=35 by 1
rotate column x=33 by 2
rotate column x=32 by 1
rotate column x=31 by 2
rotate column x=30 by 1
rotate column x=28 by 1
rotate column x=27 by 3
rotate column x=26 by 1
rotate column x=23 by 2
rotate column x=22 by 1
rotate column x=21 by 1
rotate column x=20 by 1
rotate column x=19 by 1
rotate column x=18 by 2
rotate column x=16 by 2
rotate column x=15 by 1
rotate column x=13 by 1
rotate column x=12 by 1
rotate column x=11 by 1
rotate column x=10 by 1
rotate column x=7 by 1
rotate column x=6 by 1
rotate column x=5 by 1
rotate column x=3 by 2
rotate column x=2 by 1
rotate column x=1 by 1
rotate column x=0 by 1
rect 49x1
rotate row y=2 by 34
rotate column x=44 by 1
rotate column x=40 by 2
rotate column x=39 by 1
rotate column x=35 by 4
rotate column x=34 by 1
rotate column x=30 by 4
rotate column x=29 by 1
rotate column x=24 by 1
rotate column x=15 by 4
rotate column x=14 by 1
rotate column x=13 by 3
rotate column x=10 by 4
rotate column x=9 by 1
rotate column x=5 by 4
rotate column x=4 by 3
rotate row y=5 by 20
rotate row y=4 by 20
rotate row y=3 by 48
rotate row y=2 by 20
rotate row y=1 by 41
rotate column x=47 by 5
rotate column x=46 by 5
rotate column x=45 by 4
rotate column x=43 by 5
rotate column x=41 by 5
rotate column x=33 by 1
rotate column x=32 by 3
rotate column x=23 by 5
rotate column x=22 by 1
rotate column x=21 by 2
rotate column x=18 by 2
rotate column x=17 by 3
rotate column x=16 by 2
rotate column x=13 by 5
rotate column x=12 by 5
rotate column x=11 by 5
rotate column x=3 by 5
rotate column x=2 by 5
rotate column x=1 by 5";

        public void Print(bool[,] screen)
        {
            int numOn = 0;
            for (int y = 0; y < 6; y++)
            {
                for (int x = 0; x < 50; x++)
                {
                    Console.Write(screen[y, x]? '#':' ');
                    if (screen[y, x])
                        numOn++;
                }
                Console.WriteLine();
            }
            Console.WriteLine("Total " + numOn);
            Console.WriteLine();
        }
    }
}