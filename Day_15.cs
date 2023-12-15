using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2023
{
    public class Day_15
    {
        public static void Start() {
            // parsing
            string filePath = "resources/15InitSequence.txt";
            string[] steps = File.ReadAllText(filePath).Split(',');
            // solve parts
            Console.WriteLine(PartOne(steps));
            Console.WriteLine(PartTwo(steps));
        }

        private static int PartOne(string[] steps) {
            List<int> stepValues = new List<int>();
            foreach (string step in steps)
            {
                int stepValue = HASH(step);
                Console.WriteLine(step + " " + stepValue);
                stepValues.Add(stepValue);
            }
            int finalAnswer = stepValues.Sum();
            return finalAnswer;
        }

        private static int PartTwo(string[] steps) {
            List<string>[] boxes = new List<string>[256];
            Dictionary<string, int> focalLengths = new Dictionary<string, int>();

            for (int i = 0; i < boxes.Length; i++)
            {
                boxes[i] = new List<string>();
            }

            int finalAnswer = 0;
            foreach (string step in steps)
            {
                if (step.Contains("-")) {
                    string label = step.Substring(0, step.Length - 1);
                    int index = HASH(label);

                    if (boxes[index].Contains(label)) {
                        boxes[index].Remove(label);
                    }
                } else {
                    string[] labelLength = step.Split("=");
                    string label = labelLength[0];
                    int focalLength = int.Parse(labelLength[1]);
                    int index = HASH(label);
                    
                    if (!boxes[index].Contains(label)) {
                        boxes[index].Add(label);
                    }
                    focalLengths[label] = focalLength;
                }
            }

            for (int i = 0; i < boxes.Length; i++)
            {
                for (int j = 0; j < boxes[i].Count; j++)
                {
                    finalAnswer += (i + 1) * (j + 1) * focalLengths[boxes[i][j]];
                }
            }
            return finalAnswer;
        }

        // HASH Algorithm
        private static int HASH(string step) {
            const int mult = 17;
            const int div = 256;
            char[] chars = step.ToCharArray();
            int stepValue = 0;
            for (int i = 0; i < chars.Length; i++)
            {
                int ASCIIval = chars[i];
                stepValue += ASCIIval;
                stepValue = stepValue * mult % div;
            }
            return stepValue;
        }
    }
}
