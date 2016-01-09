using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode15Day11
{
    public class Program
    {
        public void Main(string[] args)
        {
            string input = "cqjxxyzz";

            string output = input;
            bool first = true;
            while (first || !(!hasIllegalCharacters(output) && hasPairs(output) && hasIncrement(output)))
            {
                first = false;
                output = createPass(output);

            }
        }

        public string createPass(string i)
        {
            var next = i.Last() == 'z' ? 'a' : (char)(((int)(char)i.Last()) + 1);
            return next == 'a' ? createPass(i.Substring(0, i.Length -1 )) + "a" : i.Substring(0, i.Length - 1) + next;
        }

        public static bool hasIllegalCharacters(string input)
        {
            return input.Contains("i") || input.Contains("o") || input.Contains("l");
        }

        public static bool hasPairs(string input)
        {
            return Regex.Matches(input, @"([a-z])\1{1}").Count > 1;
        }

        public static bool hasIncrement(string input)
        {
            var hasPair = false;
            for (int i = 0; i < input.Length - 2; i++)
            {
                var first = (char)input[i];
                var second = (char)input[i + 1];
                var third = (char)input[i + 2];

               if(!hasPair)
                    hasPair = (second == (char) ((int) first + 1) && third == (char) ((int) second + 1));
            }
            return hasPair;
        }

    }
}

