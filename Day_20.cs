using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Advent_of_Code_2023
{
    public class Day_20
    {
        public static void Start() {
            string filePath = "resources/20ModuleConfig.txt";
            List<string> inputLines = new List<string>(File.ReadAllLines(filePath));
            Dictionary<string, Module> modules = ParseInput(inputLines);
            
            Console.WriteLine(PartOne(modules));
        }

        private static int PartOne(Dictionary<string, Module> mods) {

            List<string> broadcastTargets = new List<string>();

            foreach (var kvp in mods)
            {
                string name = kvp.Key;
                Module module = kvp.Value;
                if(name == "broadcaster") {
                    foreach (string target in module.Targets)
                    {
                        broadcastTargets.Add(target);
                    }
                }

                foreach (string target in module.Targets)
                {
                    if (mods.ContainsKey(target) && mods[target].Type == "&")
                    {
                        ((Dictionary<string, string>)mods[target].State)[name] = "lo";
                    }
                }
            }

            int low = 0;
            int high = 0;
            
            for (int i = 0; i < 1000; i++)
            {
                low += 1;
                Queue<(string, string, string)> q = new Queue<(string, string, string)>();

                foreach (string x in broadcastTargets)
                {
                    q.Enqueue(("broadcaster", x, "lo"));
                }

                while (q.Count > 0)
                {
                    (string origin, string target, string pulse) = q.Dequeue();

                    if (pulse == "low") {
                        low += 1;
                    } else {
                        high += 1;
                    }

                    if (!mods.ContainsKey(target)) {
                        continue;
                    }

                    Module module = mods[target];

                    if (module.Type == "%") {
                        if (pulse == "low") {
                            module.State = module.State == "off" ? "on" : "off";
                            string outgoing = module.State == "on" ? "high" : "low";
                            foreach (string x in module.Targets)
                            {
                                q.Enqueue((module.Name, x, outgoing));
                            }
                        }
                    } else {
                        ((Dictionary<string, string>)module.State)[origin] = pulse;
                        string outgoing = ((Dictionary<string, string>)module.State).Values.All(x => x == "high") ? "high" : "low";
                        foreach (string x in module.Targets)
                        {
                            q.Enqueue((module.Name, x, outgoing));
                        }
                    }
                }
            }
            return low * high;
        }

        private static Dictionary<string, Module> ParseInput(List<string> input) {

            Dictionary<string, Module> modules = new Dictionary<string, Module>();

            foreach (string line in input)
            {
                string type;
                string name;
                List<string> targets = new List<string>();
                dynamic state;
                string[] modVStarget = line.Split(" -> ");
                if (modVStarget[0] != "broadcaster") {
                    type = modVStarget[0].Substring(0,1);
                    name = modVStarget[0].Remove(0,1);
                    string[] targetArray = modVStarget[1].Split(", ");
                    foreach (string target in targetArray) 
                    {
                        targets.Add(target);
                    }
                    state = type == "%" ? "off" : new Dictionary<string, string>();
                } else {
                    type = null;
                    name = modVStarget[0];
                    string[] targetArray = modVStarget[1].Split(", ");
                    foreach (string target in targetArray) 
                    {
                        targets.Add(target);
                    }
                    state = null;
                }
                Module mod = new(name, type, targets, state);
                modules[name] = mod;
            }
            return modules;
        }
    }

    public class Module {
        public string Type { get; set; }
        public string Name { get; set; } 
        public List<string> Targets { get; set; } 
        public dynamic State { get; set; }

        public Module(string name, string type, List<string> targets, dynamic state) {
            Type = type;
            Name = name;
            Targets = targets;
            State = state;

            if (type == "%") {
                State = "off";
            } else {
                State = new Dictionary<string, string>();
            }
        }

        public override string ToString() {
            return $"{Name} {{type={Type}, targets={string.Join(",", Targets)}, state={State}}}";
        }
    }
}
