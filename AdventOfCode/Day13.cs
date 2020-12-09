using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    internal class Day13
    {
        public void DoIt()
        {
            var node = new Node();
            var visited = new Dictionary<Coord, int>();

            var searchQ = new Queue<Node>();
            searchQ.Enqueue(node);
            visited.Add(node.Coord, 0);

            int lastReport = 0;
            while (true)
            {
                node = searchQ.Dequeue();

                if (node.NumSteps != lastReport)
                    Console.WriteLine((lastReport = node.NumSteps) + " visited " + visited.Count + " queue size " + searchQ.Count);

                
                if (node.NumSteps == 50)
                {
                    var map = CreateMap(visited, node);

                    Tools.PostResult(visited.Count);
                    return;
                }

                foreach (var child in node.CreateChildren())
                {
                    if (child.Coord.X == 31 && child.Coord.Y == 39)
                    {
                        var map = CreateMap(visited, child);

                        Tools.PostResult(child.NumSteps);
                        return;
                    }

                    if (visited.ContainsKey(child.Coord))
                        continue;

                    visited.Add(child.Coord, child.NumSteps);
                    searchQ.Enqueue(child);
                }
            }
        }

        private static string CreateMap(Dictionary<Coord, int> visited, Node node)
        {
            var maxy = visited.Keys.Select(c => c.Y).Max() + 3;
            var maxx = visited.Keys.Select(c => c.X).Max() + 3;
            StringBuilder sb = new StringBuilder();
            for (int y = -1; y < maxy; y++)
            {
                for (int x = -1; x < maxx; x++)
                {
                    var coord = new Coord(x, y);
                    int dist;
                    bool b = visited.TryGetValue(coord, out dist);

                    if (IsWall(coord))
                        sb.Append("#");
                    else if (x == node.Coord.X && y == node.Coord.Y)
                        sb.Append("+");
                    else if (b)
                        //sb.Append("O");
                        sb.Append((char) ((int) '0' + (dist%10)));
                    else
                        sb.Append(".");
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        class Node
        {
            public readonly Coord Coord;
            public readonly int NumSteps;

            public readonly Node Prev;

            public Node()
            {
                Coord = new Coord(1,1);
                NumSteps = 0;
            }

            private Node(Node other, int deltaX, int deltaY)
            {
                Prev = other;
                Coord = new Coord(Coord.X + deltaX, Coord.Y+deltaY);
                NumSteps = other.NumSteps + 1;
            }

            private Node(Node other, Coord newCoord)
            {
                Prev = other;
                Coord = newCoord;
                NumSteps = other.NumSteps + 1;
            }

            public IEnumerable<Node> CreateChildren()
            {
                var pos = Coord.MoveX(-1);
                if (!IsWall(pos))
                    yield return new Node(this, pos);
                pos = Coord.MoveX(1);
                if (!IsWall(pos))
                    yield return new Node(this, pos);
                pos = Coord.MoveY(-1);
                if (!IsWall(pos))
                    yield return new Node(this, pos);
                pos = Coord.MoveY(1);
                if (!IsWall(pos))
                    yield return new Node(this, pos);
            }
        }

        private static bool IsWall(Coord coord)
        {
            int x = coord.X;
            int y = coord.Y;
            if (x < 0 || y < 0)
                return true;

            var val = x*x + 3*x + 2*x*y + y + y*y + 1364;

            int numBits = 0;
            int mask = 1;
            do
            {
                if ((val & mask) != 0)
                    numBits++;
                mask <<= 1;
            } while (mask <= val);

            return (numBits & 1) == 1;
        }
    }
}