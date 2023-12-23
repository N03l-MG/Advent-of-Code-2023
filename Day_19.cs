using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Advent_of_Code_2023
{
    public class Day_19
    {
        public record PWF(List<Dictionary<char, int>> partDict, Dictionary<string, List<Tuple<string, string, int, string>>> workflowsDict);

        public static void Start() {
            string filepath = "resources/19PartWorkflow.txt";
            List<string> workflows = File.ReadLines(filepath).TakeWhile(line => !string.IsNullOrWhiteSpace(line)).ToList();
            List<string> parts = File.ReadLines(filepath).SkipWhile(line => !string.IsNullOrWhiteSpace(line)).Skip(1).ToList();

            PWF processedInput = ParseInput(workflows, parts);
            Console.WriteLine(PartOne(processedInput));
        }

        private static int PartOne(PWF input) {

            List<Dictionary<char, int>> parts = input.partDict;
            Dictionary<string, List<Tuple<string, string, int, string>>> workflow = input.workflowsDict;

            List<int> partChecklist = new List<int>();

            Dictionary<string, Func<int, int, bool>> ops = new Dictionary<string, Func<int, int, bool>>
            {
                { ">", (a, b) => a > b },
                { "<", (a, b) => a < b }
            };

            foreach (Dictionary<char, int> part in parts)
            {
                int xmasSum = part.Values.Sum();
                string name = "in";
                do {
                    if (workflow.ContainsKey(name)) {
                        var rules = workflow[name];

                        foreach (var rule in rules)
                        {
                            var (letterStr, op, num, target) = rule;
                            if (letterStr == null) {
                                name = target;
                                break;
                            }
                            char letter = letterStr[0];
                            if (ops[op](part[letter], num)) {
                                name = target;
                                break;
                            }
                        }
                    }
                } while (name.Length != 1);

                if (name == "R") continue;
                if (name == "A") partChecklist.Add(xmasSum);
            }
            return partChecklist.Sum();
        }

        private static PWF ParseInput(List<string> WF, List<string> P) {

            List<Dictionary<char, int>> partsList = new List<Dictionary<char, int>>();
            for (int i = 0; i < P.Count; i++)
            {
                P[i] = P[i].Trim('{', '}');
                string[] components = P[i].Split(',');

                Dictionary<char, int> letterNums = new Dictionary<char, int>();

                for (int j = 0; j < components.Length; j++)
                {
                    string[] letternumpair = components[j].Split('='); 
                    letterNums.Add(Convert.ToChar(letternumpair[0]), int.Parse(letternumpair[1]));
                }
                partsList.Add(letterNums);
            }

            Dictionary<string, List<Tuple<string, string, int, string>>> workflowsDict = new Dictionary<string, List<Tuple<string, string, int, string>>>();
            for (int i = 0; i < WF.Count; i++)
            {
                string[] nameVSrules = WF[i].Split('{');
                string name = nameVSrules[0];
                nameVSrules[1] = nameVSrules[1].Trim('}');
                string[] rules = nameVSrules[1].Split(',');

                string fallback = rules.Last();
                rules = rules.Take(rules.Length - 1).ToArray();
                List<Tuple<string, string, int, string>> rulesT = [];

                foreach (string rule in rules)
                {
                    string[] ruleParts = rule.Split(':');
                    string comparison = ruleParts[0];
                    string target = ruleParts[1];
                    string letter = comparison[0].ToString();
                    string op = comparison[1].ToString();
                    int num = int.Parse(comparison[2..]);
                    Tuple<string, string, int, string> ruleTuple = new Tuple<string, string, int, string>(letter, op, num, target);
                    rulesT.Add(ruleTuple);
                }
                rulesT.Add(new Tuple<string, string, int, string> (null, null, 0, fallback));
                workflowsDict.Add(name, rulesT);
            }

            Print(partsList, workflowsDict);
            PWF parsedInput = new PWF(partsList, workflowsDict);
            return parsedInput;
        }

        private static void Print(List<Dictionary<char, int>> partsList, Dictionary<string, List<Tuple<string, string, int, string>>> workflowsDict) {
            foreach (KeyValuePair<string, List<Tuple<string, string, int, string>>> entry in workflowsDict)
            {
                Console.Write(entry.Key + " - ");
                for (int i = 0; i < entry.Value.Count; i++)
                {
                    Console.Write(entry.Value[i] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            foreach (Dictionary<char, int> xmasVals in partsList)
            {
                Console.Write(xmasVals['x'] + " " + xmasVals['m'] + " " + xmasVals['a'] + " " + xmasVals['s']);
                Console.WriteLine();
            }
        }
    }
}
