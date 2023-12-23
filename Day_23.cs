using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Advent_of_Code_2023
{
    public class Day_23
    {
        private static Dictionary<string, int[]> directions = new Dictionary<string, int[]>{
            {"north", new int[]{-1, 0}},
            {"east", new int[]{0, 1}},
            {"west", new int[]{0, -1}},
            {"south", new int[]{1, 0}}
        };

        public record Pos(int Row, int Col);

        public static void Start() {
            string filePath = "resources/23TrailMap.txt";
            List<string> lines = new List<string>(File.ReadAllLines(filePath));
            List<char[]> map = MapParse(lines);
            Console.WriteLine(PartOne(map));
        }

        private static int PartOne(List<char[]> map) {
            List<int> paths = new List<int>();
            Pos currentPos = new Pos(0,1);
            List<Pos> travelledPositions = new List<Pos>();
            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 1; j < map[0].Length; j++)
                {
                    do {
                        int currentPath = 0;
                        foreach (KeyValuePair<string, int[]> dir in directions)
                        {
                            int nRow = currentPos.Row + dir.Value[0];
                            int nCol = currentPos.Col + dir.Value[1];
                            if (map[nRow][nCol] == '.') {
                                currentPos = new Pos(nRow, nCol);
                                travelledPositions.Add(currentPos);
                                currentPath++;
                            } else if (map[nRow][nCol] == '^') {
                                
                            } else if (map[nRow][nCol] == '<') {
                                    
                            } else if (map[nRow][nCol] == '>') {
                                    
                            } else if (map[nRow][nCol] == 'v') {
                                    
                            } else continue;
                        }
                        paths.Add(currentPath);
                    } while (currentPos.Row != map.Count && currentPos.Col != map[0].Length - 1);
                }
            }
            return paths.Max();
        }

        private static List<char[]> MapParse(List<string> lines) {
            List<char[]> map = new List<char[]>();

            foreach (string line in lines)
            {
                char[] chars = line.ToCharArray();
                map.Add(chars);
            }

            return map; 
        }
    }
}