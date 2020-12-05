using System;
using System.IO;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFileName = "../../../Input.txt";
            var outputFileName = "../../../Output.txt";
            var lines = File.ReadAllLines(inputFileName);
            var vals = Array.ConvertAll(lines, int.Parse);
            var numVals = vals.Length;
            using (var writer = new StreamWriter(outputFileName))
            {
                for (int i = 0; i < numVals; i++)
                {
                    for (int j = i + 1; j < numVals; j++)
                    {
                        if (vals[i] + vals[j] == 2020)
                        {
                            writer.WriteLine((vals[i] * vals[j]).ToString());
                        }
                    }
                }
            }
        }
    }
}