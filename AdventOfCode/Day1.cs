using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace AdventOfCode
{
    class Day1
    {
        public static void DoIt()
        {
            string input =
                //    "R2, L3";
                //    "R2, R2, R2";
                //"R5, L5, R5, R3";
                "L1, R3, R1, L5, L2, L5, R4, L2, R2, R2, L2, R1, L5, R3, L4, L1, L2, R3, R5, L2, R5, L1, R2, L5, R4, R2, R2, L1, L1, R1, L3, L1, R1, L3, R5, R3, R3, L4, R4, L2, L4, R1, R1, L193, R2, L1, R54, R1, L1, R71, L4, R3, R191, R3, R2, L4, R3, R2, L2, L4, L5, R4, R1, L2, L2, L3, L2, L1, R4, R1, R5, R3, L5, R3, R4, L2, R3, L1, L3, L3, L5, L1, L3, L3, L1, R3, L3, L2, R1, L3, L1, R5, R4, R3, R2, R3, L1, L2, R4, L3, R1, L1, L1, R5, R2, R4, R5, L1, L1, R1, L2, L4, R3, L1, L3, R5, R4, R3, R3, L2, R2, L1, R4, R2, L3, L4, L2, R2, R2, L4, R3, R5, L2, R2, R4, R5, L2, L3, L2, R5, L4, L2, R3, L5, R2, L1, R1, R3, R3, L5, L2, L2, R5";

            HashSet<Tuple<int, int>> visited = new HashSet<Tuple<int, int>>();

            var directions = input.Split(',').Select(i => i.Trim());
            Direction heading = Direction.North;
            int x = 0, y = 0;
            foreach (var d in directions)
            {
                if (d[0] == 'L')
                    heading++;
                else if (d[0] == 'R')
                    heading--;

                if (heading == 0) heading = (Direction) 4;
                if ((int)heading == 5) heading = (Direction)1;

                int distance = int.Parse(d.Substring(1));
                for (int i = 0; i < distance; i++)
                {
                    switch (heading)
                    {
                        case Direction.North:
                            y += 1;
                            break;
                        case Direction.West:
                            x -= 1;
                            break;
                        case Direction.South:
                            y -= 1;
                            break;
                        case Direction.East:
                            x += 1;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    if (visited.Add(Tuple.Create(x, y)) == false)
                    {
                        Clipboard.SetText((Math.Abs(x) + Math.Abs(y)).ToString(), TextDataFormat.Text);
                        Console.WriteLine($"x {x}, y {y} total {Math.Abs(x) + Math.Abs(y)}");
                        Console.ReadKey();
                        return;
                    }
                }

                Debug.WriteLine($"input {d} x {x}, y {y} total {x + y}");
            }


            Console.WriteLine($"x {x}, y {y} total {Math.Abs(x)+Math.Abs(y)}");
            Console.ReadKey();
        }

        enum Direction
        {
            North = 1, West = 2, South = 3, East = 4
        }
    }
}