using System.Collections.Generic;

namespace AdventOfCode
{
    internal class Day12
    {
        public void DoItA()
        {
            Dictionary<string, int> registers = new Dictionary<string, int> { { "a", 0 }, { "b", 0 }, { "c", 0 }, { "d", 0 } };

            var commandLines = _input.SplitLines();

            for (int ip = 0; ip < commandLines.Length; ip++)
            {
                var parts = commandLines[ip].Split(' ');
                int val;
                switch (parts[0])
                {
                    case "cpy":
                        if (int.TryParse(parts[1], out val))
                            registers[parts[2]] = val;
                        else
                            registers[parts[2]] = registers[parts[1]];
                        break;
                    case "inc":
                        registers[parts[1]]++;
                        break;
                    case "dec":
                        registers[parts[1]]--;
                        break;
                    case "jnz":
                        if (int.TryParse(parts[1], out val))
                        {
                            if (val != 0)
                                ip += int.Parse(parts[2]) - 1;
                        }
                        else if (registers[parts[1]] != 0)
                        {
                            ip += int.Parse(parts[2]) - 1;
                        }
                        break;
                }
            }

            Tools.PostResult(registers["a"]);
        }

        public void DoItB()
        {
            Dictionary<string, int> registers = new Dictionary<string, int> { { "a", 0 }, { "b", 0 }, { "c", 1 }, { "d", 0 } };

            var commandLines = _input.SplitLines();

            for (int ip = 0; ip < commandLines.Length; ip++)
            {
                var parts = commandLines[ip].Split(' ');
                int val;
                switch (parts[0])
                {
                    case "cpy":
                        if (int.TryParse(parts[1], out val))
                            registers[parts[2]] = val;
                        else
                            registers[parts[2]] = registers[parts[1]];
                        break;
                    case "inc":
                        registers[parts[1]]++;
                        break;
                    case "dec":
                        registers[parts[1]]--;
                        break;
                    case "jnz":
                        if (int.TryParse(parts[1], out val))
                        {
                            if (val != 0)
                                ip += int.Parse(parts[2]) - 1;
                        }
                        else if (registers[parts[1]] != 0)
                        {
                            ip += int.Parse(parts[2]) - 1;
                        }
                        break;
                }
            }

            Tools.PostResult(registers["a"]); // too high 9227663
        }

        private string _testInput = @"cpy 41 a
inc a
inc a
dec a
jnz a 2
dec a";

        private string _input = @"cpy 1 a
cpy 1 b
cpy 26 d
jnz c 2
jnz 1 5
cpy 7 c
inc d
dec c
jnz c -2
cpy a c
inc a
dec b
jnz b -2
cpy c b
dec d
jnz d -6
cpy 18 c
cpy 11 d
inc a
dec d
jnz d -2
dec c
jnz c -5";
    }
}