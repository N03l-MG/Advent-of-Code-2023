using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Advent_of_Code_2023
{
    public record RLPair(string L, string R);
    public record RLCodeNodes(char[] RLCode, Dictionary<string,RLPair> Nodes);

    public class Day_8
    {
        public static void Start() {
            string filePath = "resources/8MapNodes.txt";
            List<string> inputLines = new List<string>(File.ReadAllLines(filePath));
            // running and displaying
            RLCodeNodes inputParsed = ParseInput(inputLines);
            Console.WriteLine(PartOne(inputParsed));
            Console.WriteLine(PartTwo(inputParsed));
        }

        private static int PartOne(RLCodeNodes parsedInput) {
            char[] RLCode = parsedInput.RLCode;
            Dictionary<string, RLPair> nodes = parsedInput.Nodes;
            string currentNode = "AAA";
            int steps = 0;
            int looper = 0;

            while(currentNode != "ZZZ") {
                char currentRL = RLCode[looper];
                
                if (currentRL == 'R') {
                    currentNode = nodes[currentNode].R;
                } else if (currentRL == 'L') {
                    currentNode = nodes[currentNode].L;
                }
                steps++;
                looper = looper >= RLCode.Length - 1 ? 0 : looper + 1;
            }
            return steps;
        }

        private static long PartTwo(RLCodeNodes parsedInput) {
            char[] RLCode = parsedInput.RLCode;
            Dictionary<string, RLPair> nodes = parsedInput.Nodes;

            List<string> currentNodes = nodes.Keys.Where(k => k.EndsWith('A')).ToList();
            List<long> nodeSteps = new List<long>();

            for (int i = 0; i < currentNodes.Count; i++)
            {
                string currentNode = currentNodes[i];
                long steps = 0;
                int looper = 0;
                while(!currentNode.EndsWith('Z')) {
                    char currentRL = RLCode[looper];

                    if (currentRL == 'R') {
                        currentNode = nodes[currentNode].R;
                    } else if (currentRL == 'L') {
                        currentNode = nodes[currentNode].L;
                    }
                    steps++;
                    looper = looper >= RLCode.Length - 1 ? 0 : looper + 1;
                }
                nodeSteps.Add(steps);
            }
            // math
            static long gcd(long n1, long n2)
            {
                if (n2 == 0) {
                    return n1;
                } else {
                    return gcd(n2, n1 % n2);
                }
            }
            long LCM = nodeSteps.Aggregate((S, val) => S * val / gcd(S, val));
            return LCM;
        }
        // parsing
        private static RLCodeNodes ParseInput(List<string> inputLines) {

            char[] RLCode = inputLines[0].ToCharArray();
            inputLines.Remove(inputLines[0]); inputLines.Remove(inputLines[0]);
            Dictionary<string, RLPair> nodes = new Dictionary<string, RLPair>();

            foreach (string line in inputLines)
            {
                string[] nodeVSpair = line.Split(" = " );
                string[] rightVSLeft = nodeVSpair[1].Split(", ");
                string left = rightVSLeft[0].Trim(new char[] {'('}); 
                string right = rightVSLeft[1].Trim(new char[] {')'});
                RLPair pair = new RLPair(left, right);
                nodes.Add(nodeVSpair[0], pair);
            }
            return new RLCodeNodes(RLCode, nodes);
        }
    }
}
