using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2023
{
    public class Day_5 
    {
        // custom class to pass and return two variables in the ProcessAlmanac method
        public record Almanac(long[] seeds, List<long[,]> maps); 

        public static void Start() { // entry point
            string filePath = "resources/5SeedAlmanac.txt";
            List<string> almanacLines = new List<string>(File.ReadAllLines(filePath));

            Console.WriteLine(PartOne(ProcessAlmanac(almanacLines)));
            // Part two is insane, sorry.
        }

        private static long PartOne(Almanac almanac) {
            List<long> locationValues = new List<long>();
            long destinationRangeStart;
            long sourceRangeStart;
            long rangeLength;

            // process seeds into locations
            for (int h = 0; h < almanac.seeds.Length; h++)
            {
                long conversionInput = almanac.seeds[h];
                for (int i = 0; i < almanac.maps.Count; i++)
                {
                    for (int j = 0; j < almanac.maps[i].GetLength(0); j++)
                    {
                        destinationRangeStart = almanac.maps[i][j,0];
                        sourceRangeStart = almanac.maps[i][j,1];
                        rangeLength = almanac.maps[i][j,2];

                        if (sourceRangeStart <= conversionInput && conversionInput <= (sourceRangeStart + rangeLength - 1)) {
                            conversionInput = conversionInput + (destinationRangeStart - sourceRangeStart);
                            break;
                        }
                        else {
                            continue;
                        }
                    }
                    Console.WriteLine(conversionInput);
                }
                locationValues.Add(conversionInput);
            }
            long lowestLocationValue = locationValues.Min();
            return lowestLocationValue;
        }

        // method for parsing through the input and storing its data (yes it is THAT involved)
        private static Almanac ProcessAlmanac(List<string> almanacLines) { 

            // initialize a list to store 2D arrays
            List<long[,]> seedInfoMaps = new List<long[,]>();

            // separate seeds from the rest of the data
            string[] seedsLine = almanacLines[0].Split(": ");
            string[] seedStrings = seedsLine[1].Split(' ');
            long[] seeds = new long[seedStrings.Length];
            for (int i = 0; i < seedStrings.Length; i++)
            {
                seeds[i] = long.Parse(seedStrings[i]);
            }
            // remove seeds and empty line from list
            almanacLines.Remove(almanacLines[0]); almanacLines.Remove(almanacLines[0]);

            // split maps into sections for the list
            List<List<string>> sections = new List<List<string>>();
            List<string> currentSection = new List<string>(); // to store the currently scanned section
            foreach (string line in almanacLines)
            {
                if (string.IsNullOrWhiteSpace(line)) { // find the end of a section
                    if (currentSection.Any()) {
                        sections.Add(currentSection);
                        currentSection = new List<string>(); // reset current section
                    }
                }
                else {
                    currentSection.Add(line); // add lines to the current section if it hasn't ended
                }
            }
            if (currentSection.Any()) { // fix for adding last section
                sections.Add(currentSection);
            }

            // iterate over each section to extract and store the data
            foreach (List<string> section in sections)
            {
                string sectionName = section[0].Trim(':');
                List<string> dataLines = section.Skip(1).ToList();

                // convert data lines to a 2D array of integers
                int maxLength = 3;
                long[,] dataArray = new long[dataLines.Count, maxLength];
                // populate the arrays
                for (int i = 0; i < dataLines.Count; i++)
                {
                    long[] row = Array.ConvertAll(dataLines[i].Split(), long.Parse);
                    for (int j = 0; j < row.Length; j++)
                    {
                        dataArray[i, j] = row[j];
                    }
                }
                seedInfoMaps.Add(dataArray); // add array to list
            }
            return new Almanac(seeds, seedInfoMaps);
        } 
    }
}
