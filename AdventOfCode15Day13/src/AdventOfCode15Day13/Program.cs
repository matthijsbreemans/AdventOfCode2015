using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode15Day13
{
    public class Program
    {
        public void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            var people = new List<string>();
            List<Route> routes = new List<Route>();

            foreach (var line in input)
            {
                var first = line.Split(' ').First();
                var regexResult = Regex.Match(line, @"([\-\+]?\d+)").Value;
                if (line.Contains(" lose "))
                {
                    regexResult = "-" + regexResult;
                }
                var love = int.Parse(regexResult);
                var dest = line.Replace(".", "").Split(' ').Last();

                routes.Add(new Route()
                {
                    Cities = new List<string>()
                    {
                        first, dest
                    }, Distance = love

                }); 
                people.Add(first);
            }
            
            //part2
            foreach (var ppl in people.Distinct())
            {
                routes.Add(new Route()
                {
                    Cities = new List<string>()
                    {
                        "Matthijs", ppl
                    },
                    Distance = 0

                });
                
            }
            people.Add("Matthijs");
            var perm = Permutation.GetPermutations(people.Distinct().ToArray());
            var results = new Dictionary<string[], int>();

            foreach (var permutation in perm)
            {
                int distance = 0;
                for (int j = 0; j < permutation.Length - 1; j++)
                {
                    var from = permutation[j];
                    var to = permutation[j + 1];

                    var found = routes.Where(x => x.Cities.Contains(from) && x.Cities.Contains(to));
                    if (found.Any())
                    {

                        distance += found.Sum(x => x.Distance);
                    }
                }
                //connect last & first and calculate
                var last = permutation[permutation.Length - 1];
                var first = permutation[0];

                var i = routes.Where(x => x.Cities.Contains(last) && x.Cities.Contains(first));
                if (i.Any())
                {
                    distance += i.Sum(x => x.Distance);
                }


                results.Add(permutation, distance);

            }
           var output =  results.OrderByDescending(x => x.Value).First();
        }
    }

    public class Route
    {
        public Route()
        {
            Cities = new List<string>();
        }
        public List<string> Cities { get; set; }
        public int Distance { get; set; }
    }

    public class Permutation
    {

        public static IEnumerable<T[]> GetPermutations<T>(T[] items)
        {
            int[] work = new int[items.Length];
            for (int i = 0; i < work.Length; i++)
            {
                work[i] = i;
            }
            foreach (int[] index in GetIntPermutations(work, 0, work.Length))
            {
                T[] result = new T[index.Length];
                for (int i = 0; i < index.Length; i++) result[i] = items[index[i]];
                yield return result;
            }
        }

        public static IEnumerable<int[]> GetIntPermutations(int[] index, int offset, int len)
        {
            if (len == 1)
            {
                yield return index;
            }
            else if (len == 2)
            {
                yield return index;
                Swap(index, offset, offset + 1);
                yield return index;
                Swap(index, offset, offset + 1);
            }
            else
            {
                foreach (int[] result in GetIntPermutations(index, offset + 1, len - 1))
                {
                    yield return result;
                }
                for (int i = 1; i < len; i++)
                {
                    Swap(index, offset, offset + i);
                    foreach (int[] result in GetIntPermutations(index, offset + 1, len - 1))
                    {
                        yield return result;
                    }
                    Swap(index, offset, offset + i);
                }
            }
        }

        private static void Swap(int[] index, int offset1, int offset2)
        {
            int temp = index[offset1];
            index[offset1] = index[offset2];
            index[offset2] = temp;
        }

    }
}

