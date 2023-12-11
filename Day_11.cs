using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Advent_of_Code_2023
{
    public class Day_11
    {
        public static void Start() {
            string filePath = "resources/11GalaxyMap.txt";
            List<string> lines = File.ReadAllLines(filePath).ToList();
            List<List<char>> galaxyMap = ParseMap(lines);
            Console.WriteLine(PartOne(galaxyMap));
        }

        public static long PartOne(List<List<char>> galaxyMap) {
            
            List<int> emptyColumnIndexes = new List<int>();
            for (int i = 0; i < galaxyMap[0].Count; i++)
            {
                if (galaxyMap.Select(x => x[i]).All(c => c == '.')) {
                    emptyColumnIndexes.Add(i);
                }
            }
            for (int i = emptyColumnIndexes.Count - 1; i >= 0; i--)
            {
                galaxyMap.ForEach(x => x.Insert(emptyColumnIndexes[i], '.'));
            }

            List<int> emptyRowIndexes = new List<int>();
            List<char> emptySpace = Enumerable.Repeat('.', galaxyMap[0].Count).ToList();
            for (int i = 0; i < galaxyMap.Count; i++)
            {
                List<char> currentRow = galaxyMap[i];
                if (currentRow.All(c => c == '.')) {
                    emptyRowIndexes.Add(i);
                }
            }
            for (int i = emptyRowIndexes.Count - 1; i >= 0; i--)
            {
                galaxyMap.Insert(emptyRowIndexes[i], emptySpace);
            }

            List<Pos> galaxyPositions = new List<Pos>();
            for (int row = 0; row < galaxyMap.Count; row++)
            {
                for (int col = 0; col < galaxyMap[row].Count; col++)
                {
                    if (galaxyMap[row][col] == '#') {
                        galaxyPositions.Add(new Pos(row, col));
                    }
                }
            }

            long finalAnswer = 0;
            for (int i = 0; i < galaxyPositions.Count; i++)
            {
                for (int j = 0; j < galaxyPositions.Count; j++)
                {
                    if (i < j) {
                        int a = Math.Abs(galaxyPositions[i].col - galaxyPositions[j].col);
                        int b = Math.Abs(galaxyPositions[i].row - galaxyPositions[j].row);
                        int distance = a + b;
                        finalAnswer += distance;
                    }
                }
            }
            return finalAnswer;
        }

        public record Pos(int row, int col);

        public static List<List<char>> ParseMap(List<string> inputLines) {
            List<List<char>> galaxyMap = new List<List<char>>();

            foreach (string line in inputLines)
            {
                List<char> lineChars = new List<char>();
                for (int c = 0; c < line.Length; c++)
                {
                    lineChars.Add(line[c]);
                }
                galaxyMap.Add(lineChars);
            }
            return galaxyMap;
        }
    }
}
