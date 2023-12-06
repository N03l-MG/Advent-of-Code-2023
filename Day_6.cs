using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Advent_of_Code_2023
{
    public class Day_6
    {
        public static void Start() {
            string filePath = "resources/6RaceRecords.txt";
            List<string> inputLines = new List<string>(File.ReadAllLines(filePath));

            Console.WriteLine(PartOne(inputLines));
            Console.WriteLine(PartTwo(inputLines));
        }

        private static int PartOne(List<string> raceRecords) {
            int finalAnswer = 0;

            List<int> marginsToBeatPerRace = new List<int>();

            // goofy aah parsing
            List<int> times = ParseLine(raceRecords[0]);
            List<int> distances = ParseLine(raceRecords[1]);
            
            // goofy aah logic
            for (int i = 0; i < times.Count; i++) // cycle races
            {
                int marginToBeat = 0;
                int raceTime = times[i];
                int distance = distances[i];
                int raceTimeLeft;
                for (int pressingTime = 0; pressingTime < raceTime; pressingTime++) // run time until end of race
                {
                    raceTimeLeft = raceTime - pressingTime;
                    int distanceTraveled = raceTimeLeft * pressingTime;
                    if (distanceTraveled > distance) {
                        marginToBeat++;
                    }
                }
                marginsToBeatPerRace.Add(marginToBeat); 
            }
            finalAnswer = marginsToBeatPerRace.Aggregate(1, (acc, x) => acc * x);
            return finalAnswer;
        }

        private static long PartTwo(List<string> raceRecords) {
            int marginToBeat = 0;

            List<int> times = ParseLine(raceRecords[0]);
            string timeString = times.Aggregate("", (acc, x) => acc + x);
            List<int> distances = ParseLine(raceRecords[1]);
            string distanceString = distances.Aggregate("", (acc, x) => acc + x);

            long time = long.Parse(timeString);
            long distance = long.Parse(distanceString);

            long raceTimeLeft;
            for (int pressingTime = 0; pressingTime < time; pressingTime++) // run time until end of race
            {
                raceTimeLeft = time - pressingTime;
                long distanceTraveled = raceTimeLeft * pressingTime;
                if (distanceTraveled > distance) {
                    marginToBeat++;
                }
            }
            return marginToBeat;
        }

        private static List<int> ParseLine(string line) {
            List<int> outputList = new List<int>();

            string[] nameVsValues = line.Split(':');
            string[] values = nameVsValues[1].Split(' ');
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] == "") {
                    continue;
                }
                outputList.Add(int.Parse(values[i]));
            }
            return outputList;
        }
    }
}