using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Advent_of_Code_2023
{
    public class Day_14
    {
        record Pos(int row, int col);

        public static void Start() {
            string filePath = "resources/14Rocks.txt";
            List<string> inputLines = File.ReadAllLines(filePath).ToList();
            Console.WriteLine(PartOne(inputLines));
            Console.ReadLine();
        }

        private static int PartOne(List<string> rockMap) {
            // get init positions of rocks
            List<Pos> rockIndexes = new List<Pos>();
            for (int i = 0; i < rockMap.Count; i++)
            {
                for (int j = 0; j < rockMap[i].Length; j++)
                {
                    if (rockMap[i][j] == 'O') {
                        rockIndexes.Add(new Pos(i, j));
                    }
                    else continue;
                }
            }
            // roll every rock north until it hits a wall
            foreach (Pos rock in rockIndexes)
            {
                if (rock.row != 0)
                {
                    Pos newRock = rock;
                    char currentRock;
                    char spaceAbove;
                    string currentRow;
                    string rowAbove;
                    do
                    {
                        currentRock = rockMap[newRock.row][newRock.col];
                        spaceAbove = rockMap[newRock.row - 1][newRock.col];
                        currentRow = rockMap[newRock.row];
                        rowAbove = rockMap[newRock.row - 1];

                        if (spaceAbove != '#' && spaceAbove != 'O')
                        {
                            rockMap[newRock.row - 1] = rowAbove.Remove(newRock.col, 1).Insert(newRock.col, currentRock.ToString());
                            rockMap[newRock.row] = currentRow.Remove(newRock.col, 1).Insert(newRock.col, ".");
                            newRock = new Pos(newRock.row - 1, newRock.col);
                        }
                        else break;
                    } while (spaceAbove != '#' && spaceAbove != 'O' && newRock.row != 0);
                }
                //Print(rockMap);
            }
            
            int finalAnswer = 0;
            int weight = 0;
            for (int i = rockMap.Count - 1; i >= 0; i--)
            {
                weight++;
                int rocksPerLine = 0;
                for (int j = 0; j < rockMap[i].Length; j++)
                {
                    if (rockMap[i][j] == 'O') {
                        rocksPerLine++;
                    }
                }
                finalAnswer += rocksPerLine * weight;
            }
            return finalAnswer;
        }

        private static void Print(List<string> rockMap) {
            for (int i = 0; i < rockMap.Count; i++)
            {
                for (int j = 0; j < rockMap[i].Length; j++)
                {
                    Console.Write(rockMap[i][j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
