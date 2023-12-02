using System;
using System.Collections.Generic;
using System.IO;

namespace Advent_of_Code_2023
{
    public class Day_2
    {
        public static void Start() {
            string filePath = "resources/GameResults_Day2.txt"; // filepath to save the games into a list 
            List<string> gamesList = new List<string>(File.ReadAllLines(filePath));
            // display answers
            Console.WriteLine("Part 1: " + PartOne(gamesList));
            Console.WriteLine("Part 2: " + PartTwo(gamesList));
        }

        static int PartOne(List<string> games) {
            int finalAnswer = 0;
            // log the max ammount for each given colour
            Dictionary<string, int> maxColourValues = new Dictionary<string, int>{
                {"red", 12},
                {"green", 13},
                {"blue", 14}
            }; 

            // loop through every game and determine if it is valid
            foreach (string game in games) {
                bool isValid = true;
                string[] gameIDSplit = game.Split(": ");
                string[] revealsPerGame = gameIDSplit[1].Split("; ");
                for (int i = 0; i < revealsPerGame.Length; i++) // loop through each reveal of cubes in the game
                {
                    string[] colourAmmount = revealsPerGame[i].Split(", ");
                    for (int j = 0; j < colourAmmount.Length; j++) // loop through each individual pair of colour-ammount in the reveal
                    {
                        string[] colourAndValue = colourAmmount[j].Split(" ");
                        int ammount = int.Parse(colourAndValue[0]); // extract the ammount cubes
                        string colour = colourAndValue[1]; // and extract their colour
                        int maxColourAmmount = maxColourValues[colour]; // identify the max this colour can have based on the dictionary
                        // rule out invalids
                        if (ammount > maxColourAmmount) {
                            isValid = false;
                        }
                    }
                }
                if (isValid == true) {
                    int gameID = int.Parse(gameIDSplit[0].Split(" ")[1]); //extract the valid game ID
                    finalAnswer += gameID; // add the valid game IDs up
                }
            }
            return finalAnswer;
        }

        static int PartTwo(List<string> games) {
            int finalAnswer = 0;

            foreach (string game in games) // same process of looping through games, reveals and colour-value pairs as in Part 1
            {
                // associate colour with a highest value per game strating at 0
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
                        // determine if this ammount of cubes of this colour is higher than the value in the dictionary and if not replace said value
                        if (ammount > colourAmmountDict[colour]) {
                            colourAmmountDict[colour] = ammount;
                        }
                    }
                }
                // with the highest value of each colour in this game stored in the dictionary simply multiply them to obtain the power
                int powerOfSet = colourAmmountDict["red"] * colourAmmountDict["green"] * colourAmmountDict["blue"];
                finalAnswer += powerOfSet; // sum up powers for every game
            }
            return finalAnswer;
        }
    }
}
