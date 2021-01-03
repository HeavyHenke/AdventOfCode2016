using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace AdventOfCode
{
    internal sealed class Day11
    {
        public void DoItA()
        {
            var visited = new HashSet<Node>();
            var searchQ = new Queue<Node>();
            searchQ.Enqueue(new Node());

            int lastReportedSteps = 0;
            Stopwatch swTot = new Stopwatch();
            Stopwatch sw = new Stopwatch();
            swTot.Start();
            sw.Start();

            while (true)
            {
                var node = searchQ.Dequeue();
                visited.Add(node);

                if (node.NumSteps != lastReportedSteps)
                {
                    Console.WriteLine($"Step {node.NumSteps} nodes {searchQ.Count} visited {visited.Count} it took {sw.Elapsed.TotalSeconds} s");
                    lastReportedSteps = node.NumSteps;
                    sw.Restart();
                }

                foreach (var child in node.CreateChildren())
                {
                    if (child.IsSolved())
                    {
                        swTot.Stop();
                        Console.WriteLine("Total time " + swTot.Elapsed);
                        Tools.PostResult(child.NumSteps);
                        return;
                    }

                    if (visited.Add(child) == false)
                        continue;

                    searchQ.Enqueue(child);
                }
            }
        }

        public void DoItB()
        {
            var visited = new HashSet<ulong>();
            var searchQ = new Queue<Node3>();
            searchQ.Enqueue(new Node3());

            int lastReportedSteps = 0;
            Stopwatch swTot = new Stopwatch();
            Stopwatch sw = new Stopwatch();
            swTot.Start();
            sw.Start();

            while (true)
            {
                var node = searchQ.Dequeue();
                visited.Add(node.GetRepresentation());

                if (node.NumSteps != lastReportedSteps)
                {
                    Console.WriteLine($"Step {node.NumSteps} nodes {searchQ.Count} visited {visited.Count} it took {sw.Elapsed.TotalSeconds} s");
                    lastReportedSteps = node.NumSteps;
                    sw.Restart();
                }

                foreach (var child in node.CreateChildren())
                {
                    if (child.IsSolved())
                    {
                        swTot.Stop();
                        Console.WriteLine("Total time " + swTot.Elapsed);
                        Tools.PostResult(child.NumSteps-2);
                        return;
                    }

                    var rep = child.GetRepresentation();
                    if(visited.Add(rep) == false)
                        continue;

                    searchQ.Enqueue(child);
                }
            }
        }

        /* Initial unoptimized solution */
        class Node
        {
            private readonly List<List<string>> _floorStuff;
            private readonly int _elevatorFloor;
            public readonly int NumSteps;
            private static int _numItems;

            public Node()
            {
                NumSteps = 0;
                _elevatorFloor = 0;
                _floorStuff = new List<List<string>>();
                for (int i = 0; i <= 4; i++)
                    _floorStuff.Add(new List<string>());

                //_floorStuff[0].Add("hydrogen microchip");
                //_floorStuff[0].Add("lithium microchip");
                //_floorStuff[1].Add("hydrogen generator");
                //_floorStuff[2].Add("lithium generator");
                //_numItems = 4;

                _floorStuff[0].Add("thulium generator");
                _floorStuff[0].Add("thulium microchip");
                _floorStuff[0].Add("plutonium generator");
                _floorStuff[0].Add("strontium generator");

                _floorStuff[1].Add("plutonium microchip");
                _floorStuff[1].Add("strontium microchip");

                _floorStuff[2].Add("promethium generator");
                _floorStuff[2].Add("promethium microchip");
                _floorStuff[2].Add("ruthenium generator");
                _floorStuff[2].Add("ruthenium microchip");

                _numItems = 10;
            }

            private Node(Node other, int direction, string toMove)
            {
                NumSteps = other.NumSteps + 1;
                _elevatorFloor = other._elevatorFloor + direction;
                _floorStuff = new List<List<string>>(4);
                for (int i = 0; i <= 3; i++)
                    _floorStuff.Add(new List<string>(other._floorStuff[i]));

                Move(toMove, other._elevatorFloor, other._elevatorFloor + direction);
            }

            private Node(Node other, int direction, string toMove1, string toMove2)
                : this(other, direction, toMove1)
            {
                Move(toMove2, other._elevatorFloor, other._elevatorFloor + direction);
            }

            private void Move(string toMove, int fromFloor, int toFloor)
            {
                _floorStuff[fromFloor].Remove(toMove);
                _floorStuff[toFloor].Add(toMove);
            }

            private bool Equals(Node other)
            {
                if (_elevatorFloor != other._elevatorFloor)
                    return false;

                for (int floor = 0; floor < _floorStuff.Count; floor++)
                {
                    if (_floorStuff.Count != other._floorStuff.Count)
                        return false;
                    foreach (var stuff in _floorStuff[floor])
                    {
                        if (!other._floorStuff[floor].Contains(stuff))
                            return false;
                    }
                }

                return true;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != GetType()) return false;
                return Equals((Node)obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = 0;
                    foreach (var floor in _floorStuff)
                    {
                        int floorHash = 0;
                        foreach (var thing in floor)
                        {
                            floorHash ^= thing.GetHashCode();
                        }
                        hashCode = (hashCode * 397) ^ floorHash;
                    }
                    hashCode = (hashCode * 397) ^ _elevatorFloor;
                    return hashCode;
                }
            }

            public static bool operator ==(Node left, Node right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(Node left, Node right)
            {
                return !Equals(left, right);
            }

            private bool IsValid(int floor1, int floor2)
            {
                if (_floorStuff[floor1].Count < _floorStuff[floor2].Count)
                    return IsFloorValid(floor1) && IsFloorValid(floor2);
                return IsFloorValid(floor2) && IsFloorValid(floor1);
            }

            private bool IsFloorValid(int floorNum)
            {
                if (_floorStuff[floorNum].Count < 2)
                    return true;

                var generators =
                    _floorStuff[floorNum].Select(GeneratorName)
                        .Where(m => m != null)
                        .ToList();

                if (generators.Count == 0)
                    return true;

                var chips =
                    _floorStuff[floorNum].Select(ChipName)
                        .Where(m => m != null)
                        .ToList();

                int numMissmatches = chips.Count(c => !generators.Contains(c));
                if (numMissmatches > 0)
                    return false;
                return true;
            }

            private static string GeneratorName(string stuff)
            {
                var splt = stuff.Split(' ');
                if (splt[1] == "generator")
                    return splt[0];
                return null;
            }

            private static string ChipName(string stuff)
            {
                if (stuff.Contains("microchip") == false)
                    return null;
                return stuff.Split(' ')[0];
            }

            public bool IsSolved()
            {
                if (_elevatorFloor != 3)
                    return false;
                return _floorStuff[3].Count == _numItems;
            }

            public IEnumerable<Node> CreateChildren()
            {
                for (int i = 0; i < _floorStuff[_elevatorFloor].Count; i++)
                {
                    // Elevetor going up
                    Node child;
                    if (_elevatorFloor < 3)
                    {
                        child = new Node(this, 1, _floorStuff[_elevatorFloor][i]);
                        if (child.IsValid(_elevatorFloor, _elevatorFloor + 1))
                            yield return child;

                        for (int j = i + 1; j < _floorStuff[_elevatorFloor].Count; j++)
                        {
                            child = new Node(this, 1, _floorStuff[_elevatorFloor][i], _floorStuff[_elevatorFloor][j]);
                            if (child.IsValid(_elevatorFloor, _elevatorFloor + 1))
                                yield return child;
                        }
                    }

                    // Going down
                    if (_elevatorFloor >= 1)
                    {
                        child = new Node(this, -1, _floorStuff[_elevatorFloor][i]);
                        if (child.IsValid(_elevatorFloor, _elevatorFloor - 1))
                            yield return child;

                        for (int j = i + 1; j < _floorStuff[_elevatorFloor].Count; j++)
                        {
                            child = new Node(this, -1, _floorStuff[_elevatorFloor][i], _floorStuff[_elevatorFloor][j]);
                            if (child.IsValid(_elevatorFloor, _elevatorFloor - 1))
                                yield return child;
                        }
                    }
                }
            }

            public override string ToString()
            {
                var sb = new StringBuilder();
                for (int i = 0; i < _floorStuff.Count; i++)
                {
                    if (_elevatorFloor == i)
                        sb.Append("E ");
                    sb.AppendLine(string.Join(" ", _floorStuff[i]));
                }

                return sb.ToString();
            }
        }
        

        sealed class Node3
        {
            private readonly ushort[] _floors;
            private readonly int _elevatorFloor;
            public readonly int NumSteps;

            private const ushort thulium = 1;
            private const ushort plutonium = 2;
            private const ushort strontium = 4;
            private const ushort promethium = 8;
            private const ushort ruthenium = 16;
            private const ushort elerium = 32;
            private const ushort dilithium = 64;

            private const ushort solution = 0x7F7F;

            public Node3()
            {
                NumSteps = 0;
                _elevatorFloor = 0;
                _floors = new ushort[4];

                _floors[0] = AddChip(_floors[0], thulium);
                _floors[0] = AddGenerator(_floors[0], thulium);
                _floors[0] = AddGenerator(_floors[0], plutonium);
                _floors[0] = AddGenerator(_floors[0], strontium);

                _floors[1] = AddChip(_floors[1], plutonium);
                _floors[1] = AddChip(_floors[1], strontium);


                _floors[2] = AddChip(_floors[2], promethium);
                _floors[2] = AddChip(_floors[2], ruthenium);
                _floors[2] = AddGenerator(_floors[2], promethium);
                _floors[2] = AddGenerator(_floors[2], ruthenium);

                _floors[0] = AddChip(_floors[0], elerium);
                _floors[0] = AddChip(_floors[0], dilithium);
                _floors[0] = AddGenerator(_floors[0], elerium);
                _floors[0] = AddGenerator(_floors[0], dilithium);
            }

            private Node3(Node3 other, short direction, ushort toMove)
            {
                NumSteps = other.NumSteps + 1;
                _elevatorFloor = (ushort)(other._elevatorFloor + direction);
                _floors = new ushort[4];
                other._floors.CopyTo(_floors, 0);

                Move(toMove, other._elevatorFloor, other._elevatorFloor + direction);
            }

            private Node3(Node3 other, short direction, ushort toMove1, ushort toMove2)
                : this(other, direction, toMove1)
            {
                Move(toMove2, other._elevatorFloor, other._elevatorFloor + direction);
            }

            public ulong GetRepresentation()
            {
                ulong val = (ulong)_floors[0] | ((ulong)_floors[1] << 16) | ((ulong)_floors[2] << 24) | ((ulong)_elevatorFloor << 48);
                return val;
            }

            private void Move(ushort toMove, int fromFloor, int toFloor)
            {
                _floors[fromFloor] &= (ushort)~toMove;
                _floors[toFloor] |= toMove;
            }


            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private bool IsValid(int floor1, int floor2)
            {
                return IsValid(_floors[floor1]) && IsValid(_floors[floor2]);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool IsSolved()
            {
                if (_elevatorFloor != 3)
                    return false;
                return _floors[3] == solution;
            }

            public IEnumerable<Node3> CreateChildren()
            {
                ushort i = 0;
                var floor = _floors[_elevatorFloor];
                while (floor > i)
                {
                    if (i == 0)
                        i = 1;
                    else
                        i <<= 1;
                    if ((floor & i) == 0)
                        continue;

                    // Elevetor going up
                    Node3 child;
                    if (_elevatorFloor < 3)
                    {
                        child = new Node3(this, 1, i);
                        if (child.IsValid(_elevatorFloor, _elevatorFloor + 1))
                            yield return child;

                        ushort j = i;
                        while (floor > j)
                        {
                            j <<= 1;
                            if ((floor & j) == 0)
                                continue;

                            child = new Node3(this, 1, i, j);
                            if (child.IsValid(_elevatorFloor, _elevatorFloor + 1))
                                yield return child;
                        }
                    }

                    // Going down
                    if (_elevatorFloor >= 1)
                    {
                        child = new Node3(this, -1, i);
                        if (child.IsValid(_elevatorFloor, _elevatorFloor - 1))
                            yield return child;

                        ushort j = i;
                        while (floor > j)
                        {
                            j <<= 1;
                            if ((floor & j) == 0)
                                continue;

                            child = new Node3(this, -1, i, j);
                            if (child.IsValid(_elevatorFloor, _elevatorFloor - 1))
                                yield return child;
                        }
                    }
                }
            }

            public override string ToString()
            {
                var sb = new StringBuilder();
                for (int i = 0; i < _floors.Length; i++)
                {
                    if (_elevatorFloor == i)
                        sb.Append("E ");
                    sb.AppendLine(string.Join(" ", _floors[i]));
                }

                return sb.ToString();
            }

            private static ushort AddGenerator(ushort state, ushort name)
            {
                state |= name;
                return state;
            }

            private static ushort AddChip(ushort state, ushort name)
            {
                state |= (ushort)(name << 8);
                return state;
            }

            private static bool IsValid(ushort state)
            {
                var generator = (state & 0xFF);
                if (generator == 0)
                    return true;

                var chip = (state >> 8);

                var test = (chip ^ generator) & chip;
                return test == 0;
            }
        }
    }
}