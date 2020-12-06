using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace Day_6
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

            var groupAnswerSets = ExtractGroupAnswerSets(lines, part2);
            var sum = 0;
            foreach (var group in groupAnswerSets)
            {
                writer.Write(String.Join(' ', group));
                var groupCount = @group.Count;
                writer.WriteLine($": {groupCount}");
                sum += groupCount;
            }

            writer.WriteLine($"Sum: {sum}");
        }

        static List<HashSet<char>> ExtractGroupAnswerSets(IList<string> lines, bool allAnswered)
        {
            var groupAnswerSets = new List<HashSet<char>>();
            var groupAnswers = new HashSet<char>();
            foreach (var line in lines)
            {
                if (line == "")
                {
                    groupAnswerSets.Add(groupAnswers);
                    groupAnswers = new HashSet<char>();
                }
                else
                {
                    foreach (var answer in line)
                    {
                        groupAnswers.Add(answer);
                    }
                }
            }

            if (lines[lines.Count - 1] != "")
            {
                groupAnswerSets.Add(groupAnswers);
            }

            return groupAnswerSets;
        }
    }
}