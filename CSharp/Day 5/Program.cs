using System;
using System.IO;

namespace Day_5
{
    class Program
    {
        static void Main(string[] args)
        {
            var part2 = args.Length >= 1;
            const string inputFileName = "../../../Input.txt";
            const string exampleInput = "../../../example.txt";
            const string part1OutputFileName = "../../../Output.txt";
            //var lines = File.ReadAllLines(exampleInput);
            var lines = File.ReadAllLines(inputFileName);

            using var writer = new StreamWriter(part1OutputFileName);

            var maxID = 0u;
            var takenIDs = new bool[1024];
            foreach (var line in lines)
            {
                var id = getUniqueID(line);
                if (maxID < id)
                {
                    maxID = id;
                }
                writer.WriteLine(id);
                takenIDs[id] = true;
            }
            writer.WriteLine($"Max is {maxID}");
            for (int i = 1; i < 1023; i++)
            {
                if (!takenIDs[i] && takenIDs[i - 1] && takenIDs[i + 1])
                {
                    writer.WriteLine($"My seat id is {i}");
                }
            }
        }

        static uint getUniqueID(string line)
        {
            string asBinary = line.Replace("F", "0").Replace("B", "1").Replace("L", "0").Replace("R", "1");
            uint uniqueID = Convert.ToUInt32(asBinary, 2);
            return uniqueID;
        }
        
    }
}