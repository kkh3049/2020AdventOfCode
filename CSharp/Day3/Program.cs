using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;

namespace Day3
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
            var lines = File.ReadAllLines(inputFileName);
            var slopes = part2 ? new[] {(1, 1), (3, 1), (5, 1), (7, 1), (1, 2)} : new [] {(3, 1)};
            using var writer = new StreamWriter(outputFile);
            BigInteger multiplier = 1;
            foreach (var (x, y) in slopes)
            {
                var numTrees = CountTrees(lines, x, y);
                writer.WriteLine($"({x}, {y}) => {numTrees}");
                multiplier *= numTrees;
            }

            writer.WriteLine(multiplier);
        }

        static int CountTrees(IList<string> lines, int deltaX, int deltaY)
        {
            int numTrees = 0;
            int x = 0;
            int y = 0;
            while (y < lines.Count)
            {
                if (lines[y][x % lines[y].Length] == '#')
                {
                    numTrees++;
                }

                x += deltaX;
                y += deltaY;
            }

            return numTrees;
        }

    }
}
