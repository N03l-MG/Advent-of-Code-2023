using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Advent_of_Code_2023
{
    public class Day_17
    {
        public static int[,] directions = new int[,] { {-1, 0},{0, 1},{0, -1},{1, 0} };

        public static void Start() {
            string filePath = "resources/17Map.txt";
            List<string> inputLines = new List<string>(File.ReadAllLines(filePath));

            List<List<int>> blockMap = ParseMap(inputLines);
            int result = PartOne(blockMap);
            Console.WriteLine(result);
            // today was particularly tough, I used guides to learn about Dijkstra's Algorithm
        }

        private static int PartOne(List<List<int>> blockMap) { // Dijkstra's Algorithm using priority queue
            int answer = 0;

            HashSet<(int, int, int, int, int)> visited = new HashSet<(int, int, int, int, int)>();
            PriorityQueue<(int, int, int, int, int, int)> pq = new PriorityQueue<(int, int, int, int, int, int)>();

            pq.Enqueue((0,0,0,1,0,0));

            while (pq.Count > 0) {
                var (heatLoss, row, col, dirR, dirC, step) = pq.Dequeue();

                Console.WriteLine($"HeatLoss: {heatLoss}, Row: {row}, Col: {col}, DirR: {dirR}, DirC: {dirC}, Step: {step}"); // step debug

                if (row == blockMap.Count - 1 && col == blockMap[0].Count - 1) {
                    answer = heatLoss;
                    break;
                }

                if (visited.Contains((row, col, dirR, dirC, step))) {
                    continue;
                }

                visited.Add((row, col, dirR, dirC, step));

                if (step < 3 && (dirR, dirC) != (0,0)) {
                    int stepR = row + dirR;
                    int stepC = col + dirC;
                    if (0 <= stepR && stepR < blockMap.Count && 0 <= stepC && stepC < blockMap[0].Count) {
                        pq.Enqueue((heatLoss + blockMap[stepR][stepC], stepR, stepC, dirR, dirC, step + 1));
                    }
                }

                for (int i = 0; i < directions.GetLength(0); i++)
                {
                    int newDirR = directions[i, 0];
                    int newDirC = directions[i, 1];

                    if (newDirR != dirR && newDirC != dirC && newDirR != -dirR && newDirC != -dirC) {
                        int stepR = row + newDirR;
                        int stepC = col + newDirC;
                        if (0 <= stepR && stepR < blockMap.Count && 0 <= stepC && stepC < blockMap[0].Count) {
                            pq.Enqueue((heatLoss + blockMap[stepR][stepC], stepR, stepC, newDirR, newDirC, 1));
                        }
                    }
                }
            }
            return answer;
        }

        private static List<List<int>> ParseMap(List<string> inputLines) { // parsing and debug printing
            
            List<List<int>> blockMap = inputLines.Select(line => line.Trim().Select(ch => int.Parse(ch.ToString())).ToList()).ToList();

            foreach (var row in blockMap)
            {
                Console.WriteLine(string.Join(" ", row));
            }

            return blockMap;
        }
    }

    public class PriorityQueue<T> where T : IComparable<T> // because c# doesnt have a native library for heap structures
    {
        private static List<T> elements = new List<T>();

        public int Count => elements.Count;

        public void Enqueue(T elem) {
            elements.Add(elem);
            int index = elements.Count - 1;
            
            while (index > 0) {
                int parent = (index - 1) / 2;
                if (elements[index].CompareTo(elements[parent]) >= 0) break;

                (elements[index], elements[parent]) = (elements[parent], elements[index]);
                index = parent;
            } 
        }

        public T Dequeue() {
            if (elements.Count == 0) {
                Console.WriteLine("Queue is empty!");
            }

            T top = elements[0];
            int lastIndex = elements.Count - 1;
            elements[0] = elements[lastIndex];
            elements.RemoveAt(lastIndex);

            int index = 0;
            while (true) {
                int leftChild = 2 * index + 1;
                int rightChild = 2 * index + 2;
                if (leftChild >= elements.Count) break;

                int smallestChild = (rightChild < elements.Count && elements[rightChild].CompareTo(elements[leftChild]) < 0) ? rightChild : leftChild;

                if (elements[index].CompareTo(elements[smallestChild]) <= 0) break;

                (elements[index], elements[smallestChild]) = (elements[smallestChild], elements[index]);
                index = smallestChild;
            }
            return top;
        }
    }
}
