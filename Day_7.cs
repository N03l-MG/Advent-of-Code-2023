using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2023
{
    public class Day_7
    {
        public static void Start() {
            string filePath = "resources/7PokerHandBids.txt"; // puzzle input
            List<string> pokerBids = new List<string>(File.ReadAllLines(filePath));
            // output answer
            Console.WriteLine(PartOne(ParseBids(pokerBids)));
        }

        private static int PartOne(Dictionary<string, int> pokerBids) {
            // define hand type & card values, initialize variables
            int finalAnswer = 0;
            Dictionary<string, int> typeScore = new Dictionary<string, int> {
                {"AAAAA",7000000},{"AAAAB",6000000},{"AAABB",5000000},{"AAABC",4000000},{"AABBC",3000000},{"AABCD",2000000},{"ABCDE",1000000}
            };
            Dictionary<char, int> cardScores = new Dictionary<char, int> {
                {'A',13},{'K',12},{'Q',11},{'J',10},{'T',9},{'9',8},{'8',7},{'7',6},{'6',5},{'5',4},{'4',3},{'3',2},{'2',1}
            };
            Dictionary<int, int> bidRankDict = new Dictionary<int, int>();

            foreach (KeyValuePair<string, int> handBid in pokerBids)
            {
                // determine hand type
                string handType = "";
                Dictionary<char,int> cardFrequency = handBid.Key.ToCharArray()
                .GroupBy(x => x)
                .Select(x => new {card = x.Key, Count = x.Count()})
                .ToDictionary(k => k.card, c => c.Count);

                switch (cardFrequency.Count) 
                {
                case 1:
                    handType = "AAAAA";
                    break;
                case 2:
                    handType = cardFrequency.Values.Max() == 4 ? "AAAAB" : "AAABB";
                    break;
                case 3:
                    handType = cardFrequency.Values.Max() == 3 ? "AAABC" : "AABBC";
                    break;
                case 4:
                    handType = "AABCD";
                    break;
                case 5:
                    handType = "ABCDE";
                    break;
                }

                // determine hand score
                string hand = handBid.Key;
                char[] cards = hand.ToCharArray();
                int currentHandScore = 0;

                for (int i = 0; i < cards.Length; i++)
                {
                    foreach (KeyValuePair<string, int> type in typeScore)
                    {
                        if (handType == type.Key) {
                            currentHandScore = type.Value;
                        }
                    }
                    foreach (KeyValuePair<char, int> cardScore in cardScores)
                    {
                        for (int j = 0; j < cards.Length; j++)
                        {
                            if(cards[j] == cardScore.Key) {
                                currentHandScore += cardScore.Value * (int)Math.Pow(14, cards.Length - j - 1);
                            }
                        }
                    }
                }
                bidRankDict.Add(handBid.Value, currentHandScore);
            }
            
            // sort by rank
            List<KeyValuePair<int, int>> sortedList = bidRankDict.ToList();
            sortedList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
            bidRankDict.Clear();
            for (int i = 0; i < sortedList.Count; i++)
            {
                sortedList[i] = new KeyValuePair<int, int>(sortedList[i].Key, i + 1);
                Console.WriteLine(sortedList[i]);
                bidRankDict.Add(sortedList[i].Key, sortedList[i].Value);
            }

            // do the math
            foreach (KeyValuePair<int, int> entry in bidRankDict)
            {
                finalAnswer += entry.Key * entry.Value;
            }
            return finalAnswer;
        }

    	// parse the input
        private static Dictionary<string, int> ParseBids(List<string> pokerBids) {

            Dictionary<string, int> handsBidsDict = new Dictionary<string, int>();
            
            foreach (string line in pokerBids)
            {
                string[] handVSbid = line.Split(' ');
                string hand = handVSbid[0];
                int bid = int.Parse(handVSbid[1]);

                handsBidsDict.Add(hand, bid);
            }
            return handsBidsDict;
        }
    }
}
