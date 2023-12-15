using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advent_of_Code_2023
{
    public class Script_Runner
    {
        static void Main(string[] args) { // this is the entry point for the whole project
            string day;
            
            // ask the user for a day to run
            Console.WriteLine("Which day would you like to run? (1-25)");
            day = Convert.ToString(Console.ReadLine());
            if (!int.TryParse(day, out _) || int.Parse(day) > 25) {
                Console.WriteLine("Invalid Input");
            } else {
                // execute that start method for the selected day
                Console.WriteLine("Here are the results for Day " + day + ":");
                var type = Type.GetType("Advent_of_Code_2023.Day_" + day);
                var method = type.GetMethod("Start");
                method.Invoke(null, null);
            }
        }
    }
}
