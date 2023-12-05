using System;
using System.Collections.Generic;
using System.IO;

namespace Advent_of_Code_2023
{
    public class Day_3
    {
        public static void Start() {
            string filePath = "resources/3EngineSchematic.txt"; // path to text file with the engine schematic
            List<string> lines = new List<string>(File.ReadAllLines(filePath));
            // set up two-dimensional array and load it with data from schematic
            int width = lines[0].Length;
            int height = lines.Count;
            char[,] schematicXY = new char[width,height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    schematicXY[x,y] = lines[y][x]; // populating the 2D array
                }
            }
            // display answers
            Console.WriteLine("Part One: " + PartOne(schematicXY));
            Console.WriteLine("Part Two: " + PartTwo(schematicXY));
        }

        private static int PartOne(char[,] schematicXY) {
            int finalAnswer = 0;
            // load 2D array dimensions
            int numCols = schematicXY.GetLength(0);
            int numRows = schematicXY.GetLength(1);

            string previousFullNumber = ""; // for storing the last valid number
            // loop through the coordinates of the array
            for (int y = 0; y < numRows; y++)
            {
                for (int x = 0; x < numCols; x++)
                {
                    if (char.IsDigit(schematicXY[x, y])) {
                        if (IsNumberAdjacentToSymbol(schematicXY, x, y)) { // when a digit is found check its adjacency to a symbol using the IsNumberAdjacentToSymbol method

                            string fullNumber = ""; // for constructing the full number that this digit belongs to
                            int xScan = x; // value for checking the columns next to the number found

                            while (xScan < numRows && char.IsDigit(schematicXY[xScan, y])) // loop forward until the number ends and log its digits inside fullNumber
                            {
                                fullNumber = fullNumber + schematicXY[xScan, y];
                                xScan++;
                            }
                            xScan = x - 1; // re-initialize scan position to one spot before the start to avoid duplicate digit 
                            while (xScan >= 0 && char.IsDigit(schematicXY[xScan, y])) // loop backward until the number ends and log its digits inside fullNumber
                            {
                                fullNumber = schematicXY[xScan, y] + fullNumber;
                                xScan--;
                            }
                            if (fullNumber != previousFullNumber) { // if the number that has been scanned isnt the previous it is valid and added to the answer sum
                                int validFullNumber = int.Parse(fullNumber);
                                finalAnswer += validFullNumber;
                                // set previous number to this new one for checking the next
                                previousFullNumber = fullNumber;
                            }
                        }
                    }
                }
            }
            return finalAnswer;
        }
        // method to check for symbols adjacent to a number in the 2D array
        private static bool IsNumberAdjacentToSymbol(char[,] schematicXY, int x, int y) {
            int numCols = schematicXY.GetLength(0);
            int numRows = schematicXY.GetLength(1);
            // gather the vectors around the current coordinate to find adjacent symbols
            int[,] neighborhoodOffsets = TranslationVectors.vectors;
            // loop through these adjacent positions and check if there is a symbol
            for (int i = 0; i < neighborhoodOffsets.GetLength(0); i++)
            {
                int newX = x + neighborhoodOffsets[i,0];
                int newY = y + neighborhoodOffsets[i,1];

                if (newX >= 0 && newX < numRows && newY >= 0 && newY < numCols) {
                    if (!char.IsDigit(schematicXY[newX, newY]) && (schematicXY[newX, newY] != '.')) {
                        return true;
                    }
                }
            }
            return false;
        }
        // many structures in part two are identical to part one so those won't be commented
        private static int PartTwo(char[,] schematicXY) {
            int finalAnswer = 0;

            int numCols = schematicXY.GetLength(0);
            int numRows = schematicXY.GetLength(1);
            
            string previousFullNumber = "";

            int[,] neighborhoodOffsets = TranslationVectors.vectors;
            // loop through the coordinates in the 2D array until a * symbol is found
            for (int y = 0; y < numRows; y++)
            {
                for (int x = 0; x < numCols; x++)
                {
                    if (schematicXY[x, y] == '*') {
                        List<int> adjacentNumbers = new List<int>(); // list to store adjacent numbers
                        for (int i = 0; i < neighborhoodOffsets.GetLength(0); i++)
                        {
                            int newX = x + neighborhoodOffsets[i,0];
                            int newY = y + neighborhoodOffsets[i,1];
                            // this time the line scanning to find full numbers occurs only if the adjacent position to the found * is a digit
                            if (newX >= 0 && newX < numRows && newY >= 0 && newY < numCols && char.IsDigit(schematicXY[newX, newY])) {
                                string fullNumber = "";
                                int xScan = newX;

                                while (xScan < numRows && char.IsDigit(schematicXY[xScan, newY]))
                                {
                                    fullNumber = fullNumber + schematicXY[xScan, newY];
                                    xScan++;
                                }
                                xScan = newX - 1;
                                while (xScan >= 0 && char.IsDigit(schematicXY[xScan, newY]))
                                {
                                    fullNumber = schematicXY[xScan, newY] + fullNumber;
                                    xScan--;
                                }
                                if (fullNumber != previousFullNumber) {
                                    int validFullNumber = int.Parse(fullNumber);
                                    adjacentNumbers.Add(validFullNumber); // add the numbers adjacent to the * to the list
                                    previousFullNumber = fullNumber;
                                }
                            }
                        }
                        if (adjacentNumbers.Count == 2) { // if this list only has two entries it means this * only has two adjacent numbers to it and thus they multiplied
                            int gearRatio = adjacentNumbers[0] * adjacentNumbers[1];
                            finalAnswer += gearRatio;
                        }
                    }
                }
            }
            return finalAnswer;
        }
    }

    public class TranslationVectors // class storing the translation vectors for all positions around the a coordinate in the main 2D array
    {
        public static int[,] vectors = {
            {-1, -1}, {0, -1}, {1, -1},
            { -1, 0},          { 1, 0},
            { -1, 1}, { 0, 1}, { 1, 1}
        };
    }
} // please excuse the nesting catastrophe that this class is
