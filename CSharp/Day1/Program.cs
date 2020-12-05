using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            var part2 = args.Length >= 1;
            const string inputFileName = "../../../Input.txt";
            const string part1OutputFileName = "../../../Output.txt";
            const string part2OutputFileName = "../../../OutputPart2.txt";
            var lines = File.ReadAllLines(inputFileName);
            var vals = Array.ConvertAll(lines, int.Parse);
            var outputFile = part2 ? part2OutputFileName : part1OutputFileName;
            using var writer = new StreamWriter(outputFile);
            if (!part2)
            {
                var pairs = GetPairsWithGivenSum(vals, 2020);
                var (first, second) = pairs.Single();
                writer.WriteLine(first * second);
            }
            else
            {
                var triples = GetTriplesWithGivenSum(vals, 2020);
                var (first, second, third) = triples.Single();
                writer.WriteLine(first * second * third);
            }
        }

        private static List<(int, int)> GetPairsWithGivenSum(IList<int> vals, int sum)
        {
            var pairsThatSum = new List<(int, int)>();
            var numVals = vals.Count;
            for (var i = 0; i < numVals; i++)
            {
                for (var j = i + 1; j < numVals; j++)
                {
                    if (vals[i] + vals[j] == sum)
                    {
                        pairsThatSum.Add((vals[i], vals[j]));
                    }
                }
            }

            return pairsThatSum;
        }

        private static List<(int, int, int)> GetTriplesWithGivenSum(IList<int> vals, int sum)
        {
            var triplesThatSum = new List<(int, int, int)>();
            var numVals = vals.Count;
            for (var i = 0; i < numVals; i++)
            {
                for (var j = i + 1; j < numVals; j++)
                {
                    for (int k = j + 1; k < numVals; k++)
                    {
                        if (vals[i] + vals[j] + vals[k] == sum)
                        {
                            triplesThatSum.Add((vals[i], vals[j], vals[k]));
                        }
                    }
                }
            }

            return triplesThatSum;
        }
    }
}