using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Advent_of_Code_2023
{
    public class Day_9
    {
        public static void Start() {
            string filePath = "resources/9HistoricalData.txt";
            List<string> inputLines = new List<string>(File.ReadAllLines(filePath));
            // running and displaying
            List<List<long>> sequences = ParseInput(inputLines);
            Console.WriteLine(PartOne(sequences));
            Console.WriteLine(PartTwo(sequences));  
        }

        private static long PartOne(List<List<long>> sequences) {

            long finalAnswer = 0;
            foreach (List<long> sequence in sequences) {
                List<List<long>> subsequences = new List<List<long>>();
                subsequences.Add(sequence);
                List<long> pastSubsequence = sequence;

                while (pastSubsequence.Any(n => n != 0))
                {
                    List<long> currentSubsequence = new List<long>();
                    for (int n = 0; n < pastSubsequence.Count - 1; n++)
                    {
                        currentSubsequence.Add(pastSubsequence[n + 1] - pastSubsequence[n]);
                    }
                    subsequences.Add(currentSubsequence);
                    pastSubsequence = currentSubsequence;
                }

                subsequences.Last().Add(0);
                for (int i = subsequences.Count - 2; i >= 0; i--)
                {
                    subsequences[i].Add(subsequences[i].Last() + subsequences[i + 1].Last());
                }

                Print(subsequences);
                long nextMainNumber = subsequences[0].Last();
                finalAnswer += nextMainNumber;
            }
            return finalAnswer;
        }

         private static long PartTwo(List<List<long>> sequences) {

            long finalAnswer = 0;
            foreach (List<long> sequence in sequences) {
                List<List<long>> subsequences = new List<List<long>>();
                subsequences.Add(sequence);
                List<long> pastSubsequence = sequence;

                while (pastSubsequence.Any(n => n != 0))
                {
                    List<long> currentSubsequence = new List<long>();
                    for (int n = 0; n < pastSubsequence.Count - 1; n++)
                    {
                        currentSubsequence.Add(pastSubsequence[n + 1] - pastSubsequence[n]);
                    }
                    subsequences.Add(currentSubsequence);
                    pastSubsequence = currentSubsequence;
                }

                subsequences.Last().Insert(0,0);
                for (int i = subsequences.Count - 2; i >= 0; i--)
                {
                    subsequences[i].Insert(0, subsequences[i][0] - subsequences[i + 1][0]);
                }

                Print(subsequences);
                long nextMainNumber = subsequences[0][0];
                finalAnswer += nextMainNumber;
            }
            return finalAnswer;
        }

        private static List<List<long>> ParseInput(List<string> input) {
            List<List<long>> sequences = new List<List<long>>();
            foreach (string line in input)
            {
                string[] nums = line.Split(' ');
                List<long> sequence = new List<long>();
                for (int i = 0; i < nums.Length; i++)
                {
                    sequence.Add(long.Parse(nums[i]));
                }
                sequences.Add(sequence);
            }
            return sequences;
        }

        private static void Print(List<List<long>> subsequences) {
            for (int i = 0; i < subsequences.Count; i++)
            {
                subsequences[i].ForEach(s => Console.Write(s + " "));
                Console.WriteLine();
            }
        }
    }
}