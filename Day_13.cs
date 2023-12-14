using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Advent_of_Code_2023
{
    public class Day_13
    {
        public static void Start() {
            string filePath = "resources/13MirrorPatterns.txt";
            List<string> lines = File.ReadAllLines(filePath).ToList();
            List<char[,]> patternList = ParseInput(lines);
            Print(patternList);
            // this day is not complete and does not produce the correct answer
            Console.WriteLine("Day Incomplete, Answer Incorrect... " + PartOne(patternList));
            Console.ReadLine();
        }

        private static int PartOne(List<char[,]> patternList) {

            int finalAnwser = 0;

            foreach (char[,] pattern in patternList)
            {
                int height = pattern.GetLength(0);
                int width = pattern.GetLength(1);
                // check vertical
                for (int col = 0; col < width - 1; col++)
                {
                    bool yes = false;
                    for (int i = 0; i < width; i++)
                    {
                        int left = col - i;
                        int right = col + 1 + i;
                        if (0 <= left && right < width) {
                            for (int row = 0; row < height; row++)
                            {
                                if (pattern[row, left] == pattern[row, right]) {
                                    yes = true;
                                }
                            }
                        }
                    }
                    if (yes) {
                        finalAnwser += col + 1;
                    }
                }
                // check horizontal
                for (int row = 0; row < height - 1; row++)
                {
                    for (int i = 0; i < height; i++)
                    {
                        int up = row - i;
                        int down = row + 1 + i;
                        if (0 <= up && down < height) {
                            for (int col = 0; col < width; col++)
                            {
                                if (pattern[up, col] == pattern[down, col]) {
                                    
                                }
                            }
                        }
                    }
                    finalAnwser += 100 * (row + 1);
                }
            }
            return finalAnwser;
        }

        private static List<char[,]> ParseInput(List<string> input) {
            List<char[,]> patternList = new List<char[,]>();

            List<List<string>> patterns = new List<List<string>>();
            List<string> currentPattern = new List<string>();
            foreach (string line in input)
            {
                if (string.IsNullOrWhiteSpace(line)) { 
                    if (currentPattern.Any()) {
                        patterns.Add(currentPattern);
                        currentPattern = new List<string>(); 
                    }
                } else {
                    currentPattern.Add(line); 
                }
            }
            if (currentPattern.Any()) { 
                patterns.Add(currentPattern);
            }

            foreach (var pattern in patterns)
            {
                int numRows = pattern.Count;
                int numCols = pattern[0].Length;

                char[,] patternArray = new char[numRows, numCols];

                for (int i = 0; i < numRows; i++)
                {
                    char[] line = pattern[i].ToCharArray();
                    for (int j = 0; j < numCols; j++)
                    {
                        patternArray[i, j] = line[j];
                    }
                }
                patternList.Add(patternArray);
            }
            return patternList;
        }

        private static void Print(List<char[,]> patternList) {
            foreach (char[,] pattern in patternList)
            {
                for (int i = 0; i < pattern.GetLength(0); i++)
                {
                    for (int j = 0; j < pattern.GetLength(1); j++)
                    {
                        Console.Write(pattern[i,j] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
    }
}
