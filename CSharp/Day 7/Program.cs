using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            var grammar = ParseBags(lines);

            var containingSet = new HashSet<string>();
            var currentSet = new HashSet<string> {"shiny gold"};
            var numAdded = 0;
            do
            {
                var newContainingSet = new HashSet<string>();
                foreach (var (container, contained) in grammar)
                {
                    foreach (var label in currentSet)
                    {
                        if (contained.Select(c => c.Item1).Contains(label))
                        {
                            newContainingSet.Add(container);
                        }
                    }
                }

                numAdded = newContainingSet.Count;
                containingSet.UnionWith(newContainingSet);
                currentSet = newContainingSet;
            } while (numAdded > 0);

            foreach (var label in containingSet)
            {
                writer.WriteLine(label);
            }
            writer.WriteLine($"num possible outer bags: {containingSet.Count}");

            var numBags = GetNumBags(ref grammar, "shiny gold");
            writer.WriteLine($"total number of bags: {numBags - 1}");

        }

        static int GetNumBags(ref Dictionary<string, List<(string, int)>> grammar, string label)
        {
            int count = 1;
            var innerBags = grammar[label];
            foreach (var (bagLabel, innerCount) in innerBags)
            {
                count += GetNumBags(ref grammar, bagLabel) * innerCount;
            }

            return count;
        }

        static Dictionary<string, List<(string, int)>> ParseBags(IList<string> lines)
        {
            var bagGrammar = new Dictionary<string, List<(string, int)>>();
            foreach (var line in lines)
            {
                var oneLine = ParseLine(line);
                bagGrammar.Add(oneLine.container, oneLine.Item2);
            }

            return bagGrammar;
        }

        static (string container, List<(string, int)>) ParseLine(string line)
        {
            var sides = line.Split("contain", StringSplitOptions.TrimEntries);
            var container = sides[0].Split("bag", StringSplitOptions.TrimEntries)[0];
            var insideStrs = sides[1].Split(new[] {','}, StringSplitOptions.TrimEntries);
            var insides = new List<(string, int)>(insideStrs.Length);
            foreach (var inside in insideStrs)
            {
                var cntStr = inside.Split()[0];
                int count;
                if (!int.TryParse(cntStr, out count))
                    continue;
                var bagLabel = inside.Substring(cntStr.Length).Split("bag", StringSplitOptions.TrimEntries)[0];
                insides.Add((bagLabel, count));
            }

            return (container, insides);
        }
    }
}