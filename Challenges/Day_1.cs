using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2023
{
    class Day_1 {
        static void Main(string[] args) {
            string filePath = "resources/LinesList_Day1.txt"; //path to text file containing all codes
            List<string> lines = new List<string>(File.ReadAllLines(filePath));
            // display answers
            Console.WriteLine("Part 1: " + PartOne(lines));
            Console.WriteLine("Part 2: " + PartTwo(lines));
        }
        
        // solve part one
        static int PartOne(List<string> lines) {
            char firstDigit = '\0';
            char lastDigit = '\0';
            int finalAnswer = 0;

            // extract calibration values
            foreach (var line in lines)
            {
                int calibrationValue;
                // find first digit
                for (int i = 0; i < line.Length; i++)
                {
                    char currentChar = line[i];
                    if (char.IsDigit(currentChar)) {
                        firstDigit = currentChar;
                        break;
                    }
                    else if (char.IsLetter(currentChar)) {
                        continue;
                    }
                }
                // find last digit
                for (int j = line.Length - 1; j >= 0; j--)
                {
                    char currentChar = line[j];
                    if (char.IsDigit(currentChar)) {
                        lastDigit = currentChar;
                        break;
                    }
                    else if (char.IsLetter(currentChar)) {
                        continue;
                    }
                }
                // determine calibration value for this line and add it to total
                if (char.IsDigit(firstDigit) && char.IsDigit(lastDigit)){
                    calibrationValue = int.Parse(firstDigit.ToString() + lastDigit.ToString());
                    finalAnswer += calibrationValue;
                }
            }
            return(finalAnswer);
        }

        // solve part two
        static int PartTwo(List<string> lines) {
            // dict to indetify numbers within the lines
            Dictionary<string, int> numbers = new Dictionary<string, int> { 
                {"one", 1},
                {"1", 1},
                {"two", 2}, 
                {"2", 2},
                {"three", 3},
                {"3", 3}, 
                {"four", 4}, 
                {"4", 4},
                {"five", 5}, 
                {"5", 5},
                {"six", 6}, 
                {"6", 6},
                {"seven", 7}, 
                {"7", 7},
                {"eight", 8}, 
                {"8", 8},
                {"nine", 9}, 
                {"9", 9}
            };

            int finalAnswer = 0;
            // cycle through every line and find calibration value
            foreach (var line in lines) {
                int firstDigit = 0;
                int lastDigit = 0;
                // arbitrary high and low limits for the index conditions to be met
                int highestIndex = 100;
                int lowestIndex = -1;
                // determine which numbers from the dict are found and determine the first and last based on index value
                foreach (KeyValuePair<string, int> entry in numbers)
                {
                    int indexOfFirstFound = line.IndexOf(entry.Key);
                    int indexOfLastFound = line.LastIndexOf(entry.Key);

                    if (indexOfFirstFound > -1 && indexOfFirstFound < highestIndex) {
                        highestIndex = indexOfFirstFound;
                        firstDigit = entry.Value;
                    }
                    if (indexOfLastFound > -1 && indexOfLastFound > lowestIndex) {
                        lowestIndex = indexOfLastFound;
                        lastDigit = entry.Value;
                    }
                }
                // combine first and last numbers and add up every one subsequently
                finalAnswer += firstDigit * 10 + lastDigit;
            }
            return finalAnswer;
        }
    }
}
