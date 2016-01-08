using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode15Day10
{
    public class Program
    {
        public void Main(string[] args)
        {

            //var input = "111221"; // -> 312211
            var input = "3113322113";

            var output = new StringBuilder();
            //40x for part1, 50x for part2
            for (int j = 0; j < 50; j++)
            {
                int tmpCount = 0;
                for (int i = 0; i < input.Length; i++)
                {
                    var letter = input[i];
                    char next;
                    tmpCount++;
                    if (i < input.Length - 1)
                    {
                        next = input[i + 1];
                    }
                    else
                    {
                        output.Append(tmpCount + letter.ToString());
                        break;
                    }


                    if (letter == next)
                    {

                    }
                    else
                    {
                        output.Append(tmpCount + letter.ToString());
                        tmpCount = 0;
                    }
                }
                input = output.ToString();
                output = new StringBuilder();
            }
            Console.WriteLine(input.Length);
        }
    }
}
