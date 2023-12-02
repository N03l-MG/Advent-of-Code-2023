using System;
using System.Collections.Generic;
using System.IO;

namespace Advent_of_Code_2023
{
    public class Day_2
    {
        static void Main(string[] args) {
            string filePath = "resources/GameResults_Day2.txt";
            List<string> gamesList = new List<string>(File.ReadAllLines(filePath));

            Console.WriteLine("Part 1: " + PartOne(gamesList));
            Console.WriteLine("Part 2: " + PartTwo(gamesList));
        }

        static int PartOne(List<string> games) {
            int finalAnswer = 0;
            
            Dictionary<string, int> maxColourValues = new Dictionary<string, int>{
                {"red", 12},
                {"green", 13},
                {"blue", 14}
            }; 

            foreach (string game in games) {
                bool isValid = true;
                string[] gameIDSplit = game.Split(": ");
                string[] revealsPerGame = gameIDSplit[1].Split("; ");
                for (int i = 0; i < revealsPerGame.Length; i++)
                {
                    string[] colourAmmount = revealsPerGame[i].Split(", ");
                    for (int j = 0; j < colourAmmount.Length; j++)
                    {
                        string[] colourAndValue = colourAmmount[j].Split(" ");
                        int ammount = int.Parse(colourAndValue[0]);
                        string colour = colourAndValue[1];
                        int maxColourAmmount = maxColourValues[colour];

                        if (ammount > maxColourAmmount) {
                            isValid = false;
                        }
                    }
                }
                if (isValid == true) {
                    int gameID = int.Parse(gameIDSplit[0].Split(" ")[1]);
                    finalAnswer += gameID;
                }
            }
            return finalAnswer;
        }

        static int PartTwo(List<string> games) {
            int finalAnswer = 0;

            foreach (string game in games)
            {
                Dictionary<string, int> colourAmmountDict = new Dictionary<string, int>{
                    {"red", 0},
                    {"green", 0},
                    {"blue", 0}
                };

                string[] gameIDSplit = game.Split(": ");
                string[] revealsPerGame = gameIDSplit[1].Split("; ");
                for (int i = 0; i < revealsPerGame.Length; i++)
                {
                    string[] colourAmmount = revealsPerGame[i].Split(", ");
                    for (int j = 0; j < colourAmmount.Length; j++)
                    {
                        string[] colourAndValue = colourAmmount[j].Split(" ");
                        int ammount = int.Parse(colourAndValue[0]);
                        string colour = colourAndValue[1];
                        
                        if (ammount > colourAmmountDict[colour]) {
                            colourAmmountDict[colour] = ammount;
                        }
                    }
                }
                int powerOfSet = colourAmmountDict["red"] * colourAmmountDict["green"] * colourAmmountDict["blue"];
                finalAnswer += powerOfSet;
            }
            return finalAnswer;
        }
    }
}
