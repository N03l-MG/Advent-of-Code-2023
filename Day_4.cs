using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2023
{
    public class Day_4
    {
        public static void Start() {
            string filePath = "resources/4ScratchCards.txt"; // file path tof puzzle input 
            List<string> scratchCards = new List<string>(File.ReadAllLines(filePath));

            Console.WriteLine("Part One: " + PartOne(scratchCards));
            Console.WriteLine("Part Two: " + PartTwo(scratchCards));
        }

        private static int PartOne(List<string> scratchCards) {
            int finalAnswer = 0;

            foreach (string card in scratchCards) {
                int cardScore = 0;
                int numberOfMatches = GetNumberOfMatches(card);

                cardScore = (int)Math.Pow(2, numberOfMatches - 1);
                finalAnswer += cardScore;
            }
            return finalAnswer;
        }

        private static int PartTwo(List<string> scratchCards) {
            int[] ammountOfCardsPerCard = new int[scratchCards.Count];

            for (int i = 0; i < scratchCards.Count; i++)
            {
                ammountOfCardsPerCard[i]++;
                int numberOfMatches = GetNumberOfMatches(scratchCards[i]);

                for (int j = 0; j < ammountOfCardsPerCard[i]; j++)
                {
                    for (int k = 0; k < numberOfMatches; k++)
                    {
                        ammountOfCardsPerCard[i + k + 1]++;
                    }
                }
            }
            return ammountOfCardsPerCard.Sum();
        }

        private static int GetNumberOfMatches(string card) {
            List<int> winningNumbers = new List<int>();
            List<int> gottenNumbers = new List<int>();

            string[] cardNumberAndContents = card.Split(": ");
            string[] winningAndGottenNumbers = cardNumberAndContents[1].Split("| ");

            string[] winningNumberStrings = winningAndGottenNumbers[0].Split(' ');
            string[] gottenNumberStrings = winningAndGottenNumbers[1].Split(' ');

            // extract winning numbers
            for (int i = 0; i < winningNumberStrings.Length; i++)
            {
                if (int.TryParse(winningNumberStrings[i], out int result)) {
                    if (result != 0) {
                        winningNumbers.Add(result);
                    }
                }
            }
            // extract gotten numbers
            for (int i = 0; i < gottenNumberStrings.Length; i++)
            {
                if (int.TryParse(gottenNumberStrings[i], out int result)) {
                    if (result != 0) {
                        gottenNumbers.Add(result);
                    }
                }
            }
            int numberOfMatches = gottenNumbers.Intersect(winningNumbers).Count();
            return numberOfMatches;
        }
    }
}
