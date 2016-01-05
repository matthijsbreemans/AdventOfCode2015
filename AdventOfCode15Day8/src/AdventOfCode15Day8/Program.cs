using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode15Day8
{
    public class Program
    {
        public void Main(string[] args)
        {
            //Part1();
            Part2();
        }

        private void Part1()
        {
            var strippedCount = 0;
            var totalCount = 0;
            foreach (var line in File.ReadAllLines("input.txt"))
            {
                totalCount += line.Length;
                var stripped = line.Substring(0, line.Length - 1).Substring(1).Replace("\\\"", "\"").Replace("\\\\", "\\");
                Console.WriteLine(line + " --->>> " + stripped);

                while (stripped.IndexOf("\\x", StringComparison.Ordinal) > -1)
                {
                    var pos = stripped.IndexOf("\\x", StringComparison.Ordinal);
                    long output;

                    if (pos + 4 <= stripped.Length && long.TryParse(stripped.Substring(pos + 2, 2), System.Globalization.NumberStyles.HexNumber, null,
                        out output))
                    {
                        stripped = stripped.Remove(pos, 3); // leave one char behind for the count
                    }
                    else
                    {
                        Console.WriteLine("Invalid hex found");
                        stripped = stripped.Remove(pos, 1).Insert(pos, "/"); //cheating a bit to prevent being stuck in a loop
                    }

                }
                Console.WriteLine(line + " --->>> " + stripped);

                strippedCount += stripped.Length;
            }

            var answer = totalCount - strippedCount;
        }

        private void Part2()
        {
            var strippedCount = 0;
            var totalCount = 0;
            foreach (var line in File.ReadAllLines("input.txt"))
            {
                totalCount += line.Length;
                var newline = line.Replace("\\", "\\\\").Replace("\"", "\\\"");
                Console.WriteLine(line + " --->>> " + newline);
                strippedCount += ("\"" + newline + "\"").Length;
            }

            var answer = strippedCount - totalCount;
        }
    }
}
