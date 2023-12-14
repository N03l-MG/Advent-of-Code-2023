using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Advent_of_Code_2023
{
    public class Day_12
    {
        public record SpringRecord(char[] springs, int[] nums);

        public static void Start() {
            string filePath = "resources/12SpringRecord.txt";
            List<string> lines = File.ReadAllLines(filePath).ToList();
            List<SpringRecord> parsedRecords = ParseInput(lines);

            Console.WriteLine(PartOne(parsedRecords));
            Console.ReadLine();
        }

        private static int PartOne(List<SpringRecord> recordList) {
            int finalAnswer = 0;
            foreach (SpringRecord record in recordList)
            {
                List<char> springs = record.springs.ToList();
                List<int> brokenGroups = record.nums.ToList();

                int confirmedBroken = springs.Where(c => c == '#').Count();
                int unknown = springs.Where(c => c == '?').Count();
                int unknownBroken = brokenGroups.Sum() - confirmedBroken;
                int unknownNormal = unknown - unknownBroken;
                string springs_ = new string(springs.ToArray());

                finalAnswer += CountArrangementsRecursive(springs_, brokenGroups, unknownBroken, unknownNormal);
            }
            return finalAnswer;
        }

        private static int CountArrangementsRecursive(string springs, List<int> groups, int unknownBroken, int unknownNormal) {
            int arrangements = 0;

            
            if (unknownBroken == 0 && unknownNormal == 0) {
                List<int> contiguousGroups = springs.Split('.').Where(x => x != "").Select(x => x.Length).ToList();
                if (contiguousGroups.Count == groups.Count) {
                    bool isValid = true;
                    for (int i = 0; i < contiguousGroups.Count; i++)
                    {
                        if (contiguousGroups[i] != groups[i]) {
                            isValid = false;
                            break;
                        }
                    }
                    if (isValid) {
                        Console.WriteLine(springs);
                        arrangements++;
                    }
                }
            } else {
                int indexOfUnknown = springs.IndexOf('?');
                if (indexOfUnknown >= 0) {
                    if (unknownBroken > 0) {
                    arrangements += CountArrangementsRecursive(springs.Substring(0, indexOfUnknown) + '#' + springs
                    .Substring(indexOfUnknown + 1), groups, unknownBroken - 1, unknownNormal);
                    }
                    if (unknownNormal > 0) {
                        arrangements += CountArrangementsRecursive(springs.Substring(0, indexOfUnknown) + '.' + springs
                        .Substring(indexOfUnknown + 1), groups, unknownBroken, unknownNormal - 1);
                    }
                }
            }
            return arrangements;
        }

        private static List<SpringRecord> ParseInput(List<string> inputLines) {
            List<SpringRecord> records = new List<SpringRecord>();
            foreach (string line in inputLines)
            {
                string[] lineChars = line.Split(' ');
                char[] springChars = lineChars[0].ToCharArray();
                int[] springNums = lineChars[1].Split(',').Select(c => int.Parse(c)).ToArray();
                records.Add(new SpringRecord(springChars, springNums));
            }
            return records;
        }
    }
}
