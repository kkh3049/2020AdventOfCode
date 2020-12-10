using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Day_8
{
    class Program
    {
        static void Main(string[] args)
        {
            var part2 = args.Length >= 1;
            const string inputFileName = "../../../Input.txt";
            const string exampleInput = "../../../example.txt";
            const string part1OutputFileName = "../../../Output.txt";
            const string part2OutputFileName = "../../../OutputPart2.txt";
            var outputFile = part2 ? part2OutputFileName : part1OutputFileName;
            //var lines = File.ReadAllLines(exampleInput);
            var lines = File.ReadAllLines(inputFileName);
            using var writer = new StreamWriter(outputFile);

            var instructions = ExtractInstructions(lines);
            var programState = new ProgramState {ProgramCounter = 0, Accumulator = 0, TimesExecuted = new int[instructions.Length]};
            while (programState.TimesExecuted[programState.ProgramCounter] < 1)
            {
                programState = instructions[programState.ProgramCounter].Execute(programState);
            }
            writer.WriteLine(programState.Accumulator);
        }

        static Instruction[] ExtractInstructions(IList<string> lines)
        {
            var instructions = new Instruction[lines.Count];
            for (var index = 0; index < lines.Count; index++)
            {
                var line = lines[index];
                instructions[index] = ExtractInstruction(line);
            }

            return instructions;
        }

        static Instruction ExtractInstruction(string line)
        {
            var parts = line.Split();
            Debug.Assert(parts.Length == 2);
            Command cmd = (Command) Enum.Parse(typeof(Command), parts[0], true);
            var value = int.Parse(parts[1]);
            return new Instruction(cmd, value);
        }

        private record ProgramState
        {
            public int ProgramCounter;
            public int Accumulator;
            public int[] TimesExecuted;
        }

        private record Instruction
        {
            public Command Command;
            public int Value;

            public Instruction(Command command, int value)
            {
                Command = command;
                Value = value;
            }

            public ProgramState Execute(ProgramState state)
            {
                var numTimesRun = state.TimesExecuted;
                numTimesRun[state.ProgramCounter] += 1;
                switch (Command)
                {
                    case Command.Nop:
                        return state with {ProgramCounter = state.ProgramCounter + 1, TimesExecuted = numTimesRun};
                    case Command.Acc:
                        return state with {Accumulator = state.Accumulator + Value, ProgramCounter =
                            state.ProgramCounter + 1, TimesExecuted = numTimesRun};
                    case Command.Jmp:
                        return state with {ProgramCounter = state.ProgramCounter + Value, TimesExecuted = numTimesRun};
                    default:
                        throw new Exception($"Unhandled Switch Case {Command}");
                }
            }
        }

        private enum Command
        {
            Nop = 0,
            Acc,
            Jmp
        }
    }
}