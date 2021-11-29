using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Command
    {
        private string _cmd;
        private readonly string _op1Str;
        private readonly string _op2Str;
        private readonly Func<Dictionary<string, int>, int> _op1;
        private readonly Func<Dictionary<string, int>, int> _op2;

        public Command(string cmdText)
        {
            var parts = cmdText.Split(' ');
            _cmd = parts[0];

            _op1Str = parts[1];
            _op1 = CreateOperandGetter(parts[1]);
            if (parts.Length > 2)
            {
                _op2Str = parts[2];
                _op2 = CreateOperandGetter(parts[2]);
            }
        }

        private Func<Dictionary<string, int>, int> CreateOperandGetter(string op)
        {
            if (char.IsLetter(op[0]))
                return d => d[op];

            var op1Val = int.Parse(op);
            return _ => op1Val;
        }

        public void Execute(Dictionary<string, int> registers, ComputerProgram computerProgram, ref int ip)
        {
            switch (_cmd)
            {
                case "cpy":
                    registers[_op2Str] = _op1(registers);
                    ip++;
                    break;
                case "inc":
                    registers[_op1Str]++;
                    ip++;
                    break;
                case "dec":
                    registers[_op1Str]--;
                    ip++;
                    break;
                case "jnz":
                    if (_op1(registers) != 0)
                        ip += _op2(registers);
                    else
                        ip++;
                    break;
                case "add":
                    registers[_op1Str] += _op2(registers);
                    ip++;
                    break;
                case "mul":
                    registers[_op1Str] *= _op2(registers);
                    ip++;
                    break;
                case "tgl":
                    int arg = _op1(registers);

                    var instructionIx = ip + arg;
                    if (instructionIx < 0 || instructionIx >= computerProgram.Commands.Count)
                    {
                        ip++;
                        break;
                    }

                    var foundInstruction = computerProgram.Commands[instructionIx];
                    if (foundInstruction._op2Str == null)
                    {
                        if (foundInstruction._cmd == "inc")
                            foundInstruction._cmd = "dec";
                        else
                            foundInstruction._cmd = "inc";
                    }
                    else
                    {
                        if (foundInstruction._cmd == "jnz")
                        {
                            foundInstruction._cmd = "cpy";
                        }
                        else
                        {
                            foundInstruction._cmd = "jnz";
                        }
                    }

                    computerProgram.OptimizedCommand = Optimize(computerProgram.Commands);
                    ip++;
                    break;
            }
        }

        public override string ToString()
        {
            if (_op2Str == null)
                return _cmd + " " + _op1Str;
            return _cmd + " " + _op1Str + " " + _op2Str;
        }

        public static List<Command> Optimize(List<Command> inp)
        {
            var ret = new List<Command>(inp);

            // Multiplications
            for (int i = 0; i < ret.Count - 7; i++)
            {
                if (ret[i].ToString() == "cpy a d" &&
                    ret[i + 1].ToString() == "cpy 0 a" &&
                    ret[i + 2].ToString() == "cpy b c" &&
                    ret[i + 3].ToString() == "inc a" &&
                    ret[i + 4].ToString() == "dec c" &&
                    ret[i + 5].ToString() == "jnz c -2" &&
                    ret[i + 6].ToString() == "dec d" &&
                    ret[i + 7].ToString() == "jnz d -5")
                {
                    ret[i] = new Command("mul a b");
                    ret[i + 1] = new Command("cpy 0 c");
                    ret[i + 2] = new Command("cpy 0 d");
                    ret[i + 3] = new Command("jnz 1 5");
                }
            }

            return ret;
        }
    }

    class ComputerProgram
    {
        public List<Command> Commands;
        public List<Command> OptimizedCommand;
    }
    
    public class Day23
    {
        public void DoItA()
        {
            Dictionary<string, int> registers = new Dictionary<string, int> { { "a", 0 }, { "b", 0 }, { "c", 1 }, { "d", 0 } };
            registers["a"] = 12;

            var commands = Data.SplitLines().Select(c => new Command(c)).ToList();

            var executionCounter = new int[commands.Count];

            var computerProgram = new ComputerProgram();
            computerProgram.Commands = commands;
            computerProgram.OptimizedCommand = Command.Optimize(commands);
            
            for (int ip = 0; ip < computerProgram.OptimizedCommand.Count;)
            {
                executionCounter[ip]++;
                computerProgram.OptimizedCommand[ip].Execute(registers, computerProgram, ref ip);
            }

            //Tools.PostResult(registers["a"]);
            Console.WriteLine(registers["a"]);
        }

        private const string Test = @"cpy 2 a
tgl a
tgl a
tgl a
cpy 1 a
dec a
dec a";

        private const string Data = @"cpy a b
dec b
cpy a d
cpy 0 a
cpy b c
inc a
dec c
jnz c -2
dec d
jnz d -5
dec b
cpy b c
cpy c d
dec d
inc c
jnz d -2
tgl c
cpy -16 c
jnz 1 c
cpy 81 c
jnz 73 d
inc a
inc d
jnz d -2
inc c
jnz c -5";
    }
}