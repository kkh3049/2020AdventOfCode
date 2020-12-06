using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace Day_4
{
    class Program
    {
        private static readonly string[] NecessaryIds = new[] {"byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"};

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

            var documents = extractDocuments(lines);
            var numValid = CountValidDocuments(documents, part2 ? DocumentHasValidData : DocumentIsValid);
            using var writer = new StreamWriter(outputFile);
            writer.WriteLine(numValid);
        }

        private static int CountValidDocuments(List<Dictionary<string, string>> documents,
            Func<Dictionary<string, string>, bool> documentValidChecker)
        {
            var numValid = 0;
            foreach (var document in documents)
            {
                if (documentValidChecker(document))
                {
                    ++numValid;
                }
                else
                {
                    Console.WriteLine("Invalid Document");
                    foreach (var key in document.Keys)
                    {
                        Console.WriteLine($"{key}:{document[key]}");
                    }
                }
            }

            return numValid;
        }

        private static bool DocumentIsValid(Dictionary<string, string> document)
        {
            foreach (var reqItem in NecessaryIds)
            {
                if (!document.ContainsKey(reqItem))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool DocumentHasValidData(Dictionary<string, string> document)
        {
            if (!ExtractAndValidateNumerical(document, "byr", 4, 1920, 2002)) return false;
            if (!ExtractAndValidateNumerical(document, "iyr", 4, 2010, 2020)) return false;
            if (!ExtractAndValidateNumerical(document, "eyr", 4, 2020, 2030)) return false;
            if (!document.TryGetValue("hgt", out var heightStr) || heightStr.Length < 4) return false;
            var heightDigitsLength = heightStr.Length - 2;
            var heightUnits = heightStr.Substring(heightDigitsLength);
            switch (heightUnits)
            {
                case "cm":
                    if (!ValidateNumerical(0, 150, 193, heightStr.Substring(0, heightDigitsLength))) return false;
                    break;
                case "in":
                    if (!ValidateNumerical(0, 59, 76, heightStr.Substring(0, heightDigitsLength))) return false;
                    break;
                default:
                    return false;
            }

            if (!document.TryGetValue("hcl", out var hairColor) || hairColor.Length != 7) return false;
            if (!Regex.IsMatch(hairColor, "#[a-f0-9]{6}")) return false;
            if (!document.TryGetValue("ecl", out var eyeColor) || eyeColor.Length != 3) return false;
            switch (eyeColor)
            {
                case "amb":
                case "blu":
                case "brn":
                case "gry":
                case "grn":
                case "hzl":
                case "oth":
                    break;
                default:
                    return false;
            }

            return ExtractAndValidateNumerical(document, "pid", 9, 0, 999999999);
        }


        private static bool ExtractAndValidateNumerical(Dictionary<string, string> document, string key, int length,
            int minVal, int maxVal)
        {
            if (!document.TryGetValue(key, out var birthYear))
            {
                return false;
            }

            return ValidateNumerical(length, minVal, maxVal, birthYear);
        }

        private static bool ValidateNumerical(int length, int minVal, int maxVal, string birthYear)
        {
            if (length != 0 && birthYear.Length != length)
            {
                return false;
            }

            if (!int.TryParse(birthYear, out var value))
            {
                return false;
            }

            if (value < minVal || value > maxVal)
            {
                return false;
            }

            return true;
        }

        static List<Dictionary<string, string>> extractDocuments(IList<string> lines)
        {
            var documents = new List<Dictionary<string, string>>();
            var document = new Dictionary<string, string>();
            foreach (var line in lines)
            {
                if (line == "")
                {
                    documents.Add(document);
                    document = new Dictionary<string, string>();
                }
                else
                {
                    var items = line.Split(new[] {' '}, StringSplitOptions.TrimEntries);
                    foreach (var item in items)
                    {
                        var keyValue = item.Split(new[] {':'});
                        Debug.Assert(keyValue.Length == 2);
                        document.Add(keyValue[0], keyValue[1]);
                    }
                }
            }

            if (lines[lines.Count - 1] != "")
            {
                documents.Add(document);
            }

            return documents;
        }
    }
}