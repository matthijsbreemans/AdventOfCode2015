using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode15Day5
{
    public class Program
    {
        public void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            //var input = new string[]
            //{
            //    "qjhvhtzxzqqjkmpb", "xxyxx", "uurcxstgmygtbstg", "ieodomkazucvgmuy"
            //};

            Part1(input);

            var good = new List<string>();
            foreach (var word in input)
            {
                var wordArr = word.ToCharArray();
                var pairs = new List<pos>();
                var matchFound = false;

                for (var i = 0; i < wordArr.Length; i++)
                {
                    if (i != 0)
                        pairs.Add(new pos(i - 1, i, wordArr[i - 1] + wordArr[i].ToString()));

                    if (i + 2 < wordArr.Length && !matchFound)
                    {
                        var cur = wordArr[i];
                        var next = wordArr[i + 2];
                        if (cur == next)
                        {
                            matchFound = true;
                        }
                    }
                }
                var twice = pairs.GroupBy(x => x.chars).Where(g => g.Count() > 1).SelectMany(g => g).OrderBy(x => x.start);

                var final = new List<string>();
                var hasOne = new List<pos>();
                if (twice.Any())
                {
                    pos prev = twice.First();
                    hasOne.Add(prev);
                    foreach (var pair in twice.Skip(1))
                    {
                        if (prev.end != pair.start)
                        {
                            if (hasOne.Any(x => x.chars == pair.chars))
                            {
                                final.Add(pair.chars);
                            }
                            else
                            {
                                hasOne.Add(pair);
                            }
                        }

                    }
                    if (matchFound && final.Distinct().Any())
                    {
                        good.Add(word);
                    }
                }

            }
        }

        public class pos
        {
            public pos(int x, int y, string chars)
            {
                this.start = x;
                this.end = y;
                this.chars = chars;
            }
            public int start;
            public int end;

            public string chars;
        }

        public static void Part1(string[] input)
        {
            var badStrings = new string[]
         {
                        "ab", "cd", "pq","xy"
         };
            var niceWords = new List<string>();
            var vowels = "aeiou".ToCharArray();
            foreach (var word in input)
            {
                var wordArr = word.ToCharArray();
                var hasDouble = false;

                for (var i = 1; i < wordArr.Length; i++)
                {
                    if (wordArr[i - 1] != wordArr[i]) continue;
                    hasDouble = true;
                    break;
                }

                if (!badStrings.Any(x => word.Contains(x)) && wordArr.Where(x => vowels.Contains(x)).Count() > 2 && hasDouble)
                {
                    niceWords.Add(word);
                }
            }
        }
    }
}
