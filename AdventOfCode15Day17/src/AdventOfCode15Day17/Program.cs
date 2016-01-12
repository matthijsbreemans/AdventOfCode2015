using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace AdventOfCode15Day17
{
    public class Program
    {
        public void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt").Select(int.Parse).ToList();
            var output = new List<List<int>>();
            int setLength = input.Count;
            int powerSetLength = 1 << setLength;
            for (int bitMask = 0; bitMask < powerSetLength; bitMask++)
            {
                var subSet = input.Where((u, i) => ((1 << i) & bitMask) != 0).Select(x => x);
                output.Add(subSet.ToList());
            }
            var results = output.Where(x => x.Sum() == 150);

            var part1 = results.Count();
            var min = results.Min(x => x.Count);
            var part2 = results.Count(s => s.Count == min);
        }
    }
}
