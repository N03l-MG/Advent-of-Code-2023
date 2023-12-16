using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Advent_of_Code_2023
{
    public class Day_16
    {
        public record Pos(int row, int col);
        public record Beam(Pos pos, string direction);

        public static void Start() {
            // parsing
            string filePath = "resources/16MirrorLayout.txt";
            List<string> inputLines = new List<string>(File.ReadAllLines(filePath));
            List<List<char>> mirrorMap = ParseInput(inputLines);
            // solve parts
            Console.WriteLine(PartTwo(mirrorMap));
        }

        private static int PartOne(List<List<char>> mirrorMap, Beam statringBeam) {
            int height = mirrorMap.Count;
            int width = mirrorMap[0].Count;

            Beam currentBeam = statringBeam;
            Pos currentPos = currentBeam.pos;
            string currentDir = currentBeam.direction;

            List<Beam> beams = [currentBeam]; 
            HashSet<Pos> energizedPositions = [currentPos];
            HashSet<Beam> beamReg = new HashSet<Beam>();

            while (beams.Count != 0) {
                List<Beam> uBeams = new List<Beam>();
                foreach (Beam beam in beams)
                {
                    currentPos = beam.pos;
                    currentDir = beam.direction;
                    int cRow = currentPos.row;
                    int cCol = currentPos.col;
                    if (cRow < 0 || cRow >= height || cCol < 0 || cCol >= width) { // bounds check
                        continue;
                    } else if (mirrorMap[cRow][cCol] == '/' && currentDir == "east") { // reflectors
                        currentDir = "north";                   
                    } else if (mirrorMap[cRow][cCol] == '/' && currentDir == "west") {
                        currentDir = "south";
                    } else if (mirrorMap[cRow][cCol] == '\\' && currentDir == "east") {
                        currentDir = "south";
                    } else if (mirrorMap[cRow][cCol] == '\\' && currentDir == "west") {
                        currentDir = "north";
                    } else if (mirrorMap[cRow][cCol] == '/' && currentDir == "north") {
                        currentDir = "east";
                    } else if (mirrorMap[cRow][cCol] == '/' && currentDir == "south") {
                        currentDir = "west";
                    } else if (mirrorMap[cRow][cCol] == '\\' && currentDir == "north") {
                        currentDir = "west";
                    } else if (mirrorMap[cRow][cCol] == '\\' && currentDir == "south") {
                        currentDir = "east";
                    } else if (mirrorMap[cRow][cCol] == '-' && (currentDir == "south" || currentDir == "north")) { // splitters
                        Beam sBeam = new Beam(new Pos(currentPos.row + directions["east"][0], currentPos.col + directions["east"][1]), "east");
                        uBeams.Add(sBeam);
                        currentDir = "west";
                        energizedPositions.Add(sBeam.pos);
                    } else if (mirrorMap[cRow][cCol] == '|' && (currentDir == "east" || currentDir == "west")) {
                        Beam sBeam = new Beam(new Pos(currentPos.row + directions["north"][0], currentPos.col + directions["north"][1]), "north");
                        uBeams.Add(sBeam);
                        currentDir = "south";
                        energizedPositions.Add(sBeam.pos);
                    }
                    // update
                    Beam nBeam = new Beam(new Pos(currentPos.row + directions[currentDir][0], currentPos.col + directions[currentDir][1]), currentDir);
                    uBeams.Add(nBeam);
                    energizedPositions.Add(nBeam.pos);
                }
                beams = uBeams.Where(b => !beamReg.Contains(b)).ToList();
                beams.ForEach(b => beamReg.Add(b));
            }
            return energizedPositions.Where(p => p.row >= 0 && p.row < height && p.col >= 0 && p.col < width).Count();
        }

        private static int PartTwo(List<List<char>> mirrorMap) {
            // brute force approach
            List<int> outcomes = new List<int>();
            
            for (int i = 0; i < mirrorMap.Count; i++) // east west possibilities
            {
                Pos pos = new Pos(i, 0);
                Beam beam = new Beam(pos, "east");
                outcomes.Add(PartOne(mirrorMap, beam));

                pos = new Pos(i, mirrorMap[0].Count - 1);
                beam = new Beam(pos, "west");
                outcomes.Add(PartOne(mirrorMap, beam));
            }
            for (int i = 0; i < mirrorMap[0].Count; i++) // south north possibilities
            {
                Pos pos = new Pos(0, i);
                Beam beam = new Beam(pos, "south");
                outcomes.Add(PartOne(mirrorMap, beam));

                pos = new Pos(mirrorMap.Count - 1, i);
                beam = new Beam(pos, "north");
                outcomes.Add(PartOne(mirrorMap, beam));
            }
            return outcomes.Max();
        }

        private static List<List<char>> ParseInput(List<string> inputLines) {
            List<List<char>> mirrorMap = new List<List<char>>();

            for (int i = 0; i < inputLines.Count; i++)
            {
                char[] chars = inputLines[i].ToCharArray();
                List<char> line = new List<char>(chars);
                mirrorMap.Add(line);
            }

            return mirrorMap;
        }

        private static Dictionary<string, int[]> directions = new Dictionary<string, int[]>{
            {"north", new int[]{-1, 0}},
            {"east", new int[]{0, 1}},
            {"west", new int[]{0, -1}},
            {"south", new int[]{1, 0}}
        };
    }
}
