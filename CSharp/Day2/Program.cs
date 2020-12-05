using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Day2
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
            Func<string, char, int, int, bool> validityMethod = part2 ? TobogganPasswordIsValid : SledPasswordIsValid;
            var numValidPasswords = CountValidPasswords(lines, validityMethod);
            using var writer = new StreamWriter(outputFile);
            writer.WriteLine(numValidPasswords);
        }

        static int CountValidPasswords(IList<string> lines, Func<string, char, int, int, bool> passwordIsValid)
        {
            var numValidPasswords = 0;
            foreach (var line in lines)
            {
                var parsed = ParseLine(line);
                if (passwordIsValid(parsed.password, parsed.c, parsed.min, parsed.max))
                {
                    ++numValidPasswords;
                }
            }

            return numValidPasswords;
        }
        
        static (int min, int max, char c, string password) ParseLine(string line)
        {
            var items = line.Split(new[] {'-', ' ', ':'},
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            Debug.Assert(items.Length == 4);
            return (int.Parse(items[0]), int.Parse(items[1]), Char.Parse(items[2]), items[3]);
        }
        
        static bool TobogganPasswordIsValid(string password, char c, int min, int max)
        {
            int numChar = 0;
            if (password[min - 1] == c)
            {
                ++numChar;
            }

            if (password[max - 1] == c)
            {
                ++numChar;
            }
            return numChar == 1;
        }
        
        static bool SledPasswordIsValid(string password, char c, int min, int max)
        {
            int numChar = 0;
            foreach (var character in password)
            {
                if (character == c)
                {
                    ++numChar;
                }
            }

            return numChar >= min && numChar <= max;
        }
    }
}