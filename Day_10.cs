using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2023
{
    public class Day_10
    {
        public record YXpos(int y, int x);

        public static void Start() {
            string filePath = "resources/10PipeSketch.txt";
            List<string> lines = File.ReadAllLines(filePath).ToList();
            char[,] pipeMatrix = MapToMatrix(lines);
            Console.WriteLine(PartOne(pipeMatrix));
        }

        private static int PartOne(char[,] pipeMatrix) {

            YXpos startingPos = null;
            for (int y = 0; y < pipeMatrix.GetLength(0); y++)
            {
                for (int x = 0; x < pipeMatrix.GetLength(1); x++)
                {
                    if (pipeMatrix[y,x] == 'S') {
                        startingPos = new YXpos(y,x);
                        break;
                    }
                    else continue;
                }
            }
            
            YXpos currentPos = startingPos;
            string directionHeaded = "";
            foreach (KeyValuePair<string, int[]> direction in directions)
            {
                YXpos nextPos = new YXpos(currentPos.y + direction.Value[0], currentPos.x + direction.Value[1]);
                char currentchar = pipeMatrix[nextPos.y, nextPos.x];
                if (currentchar == '.') continue;
                string[] currentPipeDir = pipeDirections[currentchar];
                string compatibleDir = OppositeDirection(direction.Key);
                for (int i = 0; i < currentPipeDir.Length; i++)
                {
                    if (currentPipeDir[i] == compatibleDir) {
                        directionHeaded = direction.Key;
                        break;
                    }
                }
                if (directionHeaded != "") {
                    break;
                }
            }

            int pipeLength = 0;
            char currentPipe;
            do {
                pipeLength++;
                int[] moveVector = directions[directionHeaded];
                currentPos = new YXpos(currentPos.y + moveVector[0], currentPos.x + moveVector[1]);
                currentPipe = pipeMatrix[currentPos.y, currentPos.x];
                if (currentPipe != 'S') {
                    string[] currentPipeDir = pipeDirections[currentPipe];
                    directionHeaded = currentPipeDir.Where(d => d != OppositeDirection(directionHeaded)).First();
                    Console.WriteLine(directionHeaded);
                }
            } while (currentPipe != 'S');

            return pipeLength / 2;
        }

        private static char[,] MapToMatrix(List<string> lines) {
            char[,] charMatrix = new char[lines.Count, lines[0].Length];
            for (int i = 0; i < lines.Count; i++)
            {
                char[] lineChars = lines[i].ToCharArray();
                for (int j = 0; j < lines[i].Length; j++)
                {
                    charMatrix[i,j] = lineChars[j];
                }
            }
            return charMatrix;
        }

        private static Dictionary<string, int[]> directions = new Dictionary<string, int[]>{
            {"north", new int[]{-1, 0}},
            {"east", new int[]{0, 1}},
            {"west", new int[]{0, -1}},
            {"south", new int[]{1, 0}}
        };

        private static Dictionary<char, string[]> pipeDirections = new Dictionary<char, string[]>{
            {'|', new string[]{"north", "south"}},       
            {'-', new string[]{"east", "west"}},
            {'L', new string[]{"north", "east"}},
            {'J', new string[]{"north", "west"}},
            {'7', new string[]{"west", "south"}},
            {'F', new string[]{"east", "south"}}
        };

        private static string OppositeDirection(string direction) {
            switch (direction) {
                case "north": return "south";
                case "south": return "north"; 
                case "east": return "west"; 
                case "west": return "east";
            }
            return null;
        }
    }
}
