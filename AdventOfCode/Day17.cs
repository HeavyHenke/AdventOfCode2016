using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    internal class Day17
    {
        public void DoIt()
        {
            var pos = new Coord(0,0);
            Func<Coord, bool> isValid = c => (c.X >= 0 && c.Y >= 0 && c.X <= 3 && c.Y <= 3);
            const string input = "edjrjqaa";
            var searchQ = new Queue<Node>();
            var md5Alg = new MD5Managed();
            var goal = new Coord(3, 3);

            searchQ.Enqueue(new Node {_coord = pos});
            int longestpath = -1;
            while (searchQ.Any())
            {
                var node = searchQ.Dequeue();
                string lockStr = input + node._way;
                var hash = md5Alg.ComputeHash(Encoding.ASCII.GetBytes(lockStr)).ToHex();

                if ("bcdef".Contains(hash[0]))
                {
                    pos = node._coord.MoveY(-1);
                    if (pos == goal)
                    {
                        if(node._dist +1 > longestpath)
                            longestpath = node._dist + 1;
                    }
                    else if (isValid(pos))
                    {
                        searchQ.Enqueue(new Node {_coord = pos, _dist = node._dist + 1, _way = node._way + "U"});
                    }
                }
                if ("bcdef".Contains(hash[1]))
                {
                    pos = node._coord.MoveY(1);
                    if (pos == goal)
                    {
                        if (node._dist + 1 > longestpath)
                            longestpath = node._dist + 1;
                    }
                    else if (isValid(pos))
                    {
                        searchQ.Enqueue(new Node { _coord = pos, _dist = node._dist + 1, _way = node._way + "D" });
                    }
                }
                if ("bcdef".Contains(hash[2]))
                {
                    pos = node._coord.MoveX(-1);
                    if (pos == goal)
                    {
                        if (node._dist + 1 > longestpath)
                            longestpath = node._dist + 1;
                    }
                    else if (isValid(pos))
                    {
                        searchQ.Enqueue(new Node { _coord = pos, _dist = node._dist + 1, _way = node._way + "L" });
                    }
                }
                if ("bcdef".Contains(hash[3]))
                {
                    pos = node._coord.MoveX(1);
                    if (pos == goal)
                    {
                        if (node._dist + 1 > longestpath)
                            longestpath = node._dist + 1;
                    }
                    else if (isValid(pos))
                    {
                        searchQ.Enqueue(new Node { _coord = pos, _dist = node._dist + 1, _way = node._way + "R" });
                    }
                }
            }

            Tools.PostResult(longestpath);

        }

        private class Node
        {
            public Coord _coord;
            public int _dist;
            public string _way;
        }
    }
}