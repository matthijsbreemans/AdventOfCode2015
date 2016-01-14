using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode15Day19
{
    public class Program
    {
        public void Main(string[] args)
        {
            var result = new List<string>();
            var all = File.ReadAllLines("input.txt");
            var input = all.Take(all.Length - 1);
            
            foreach (var line in input)
            {
                var key = line.Replace(" =>", "").Split(' ')[0];
                var val = line.Replace(" =>", "").Split(' ')[1];

                result.AddRange(from Match m in Regex.Matches(all.Last(), key) select all.Last().Remove(m.Index, key.Length).Insert(m.Index, val));
            }
            var x = result.Distinct().Count();
        }

    }

}

