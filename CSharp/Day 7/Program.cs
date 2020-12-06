using System;

namespace Day_7
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
        }
    }
}