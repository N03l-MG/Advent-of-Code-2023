using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Advent_of_Code_2023
{
    public class Day_18
    {
        private static Dictionary<char, int[]> directions = new Dictionary<char, int[]>{
            {'U', new int[]{1,0}},
            {'D', new int[]{-1,0}},
            {'L', new int[]{0,-1}},
            {'R', new int[]{0,1}}
        };

        public static void Start() {
            string filepath = "resources/18ExcavationInstructions.txt";
            List<string> inputLines = new List<string>(File.ReadAllLines(filepath));
            List<string[]> instructions = ParseInput(inputLines);
            Console.WriteLine(PartOne(instructions));
        }

        private static int PartOne(List<string[]> instructions) {

            int finalArea = 0;
            Pos currentPos = new Pos(0,0);
            List<Pos> perimeter = new List<Pos>();

            // mark perimeter
            for (int i = 0; i < instructions.Count; i++)
            {
                char dir = Convert.ToChar(instructions[i][0]);
                int num = int.Parse(instructions[i][1]);
                for (int j = 0; j < num; j++){
                    foreach (KeyValuePair<char, int[]> direction in directions) {
                        if (direction.Key == dir) {
                            currentPos = new Pos(currentPos.row + direction.Value[0], currentPos.col + direction.Value[1]);
                            perimeter.Add(currentPos);
                            Console.WriteLine(currentPos);
                        } else continue;
                    }
                }
            }

            List<int> perimeterLengths = new List<int>();
            for (int i = 0; i < instructions.Count; i++)
            {
                perimeterLengths.Add(int.Parse(instructions[i][1]));
            }

            int gaussian = 0;

            for (int i = 0; i < perimeter.Count; i++)
            {
                if (i == perimeter.Count - 1) {
                    gaussian += perimeter[i].col * perimeter[0].row - perimeter[0].col * perimeter[i].row;
                } else {
                    gaussian += perimeter[i].col * perimeter[i + 1].row - perimeter[i + 1].col * perimeter[i].row;
                }
            }
            finalArea = (Math.Abs(gaussian) + perimeterLengths.Sum()) / 2;
            
            return finalArea + 1;
        }

        private static List<string[]> ParseInput(List<string> lines) {
            List<string[]> instructions = new List<string[]>();

            foreach (string line in lines)
            {
                string[] directionsColour = line.Split(' ');
                instructions.Add(directionsColour);
            }
            return instructions;
        }

        public record Pos(int row, int col);
    }
}
