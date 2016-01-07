using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode15Day9
{
    public class Program
    {

        public static string[] input;
        List<string> cities = new List<string>();
        List<Route> routes = new List<Route>();

        public void Main(string[] args)
        {
            input = File.ReadAllLines("input.txt");
            //var cities = new Dictionary<string, City>();

            foreach (var line in input)
            {
                var split = line.Split(' ');

                var from = split[0];
                var to = split[2];
                var dist = int.Parse(split[4]);

                cities.Add(to);
                cities.Add(from);

                var r = new Route();
                r.Cities.Add(to);
                r.Cities.Add(from);
                r.Distance = dist;
                routes.Add(r);

                r = new Route();
                r.Cities.Add(from);
                r.Cities.Add(to);
                r.Distance = dist;
                routes.Add(r);
            }
            var permutations = Permutation.GetPermutations<string>(cities.Distinct().ToArray());

            var results = new Dictionary<string[], int>();
            foreach (string[] permutation in permutations)
            {
                var tempRoutes = new List<Route>();
                int distance = 0;
                for (int j = 0; j < permutation.Length - 1; j++)
                {
                    var from = permutation[j];
                    var to = permutation[j + 1];

                    var found =
                        routes.Where(x => x.Cities.Contains(from) && x.Cities.Contains(to))
                            .OrderBy(y => y.Distance)
                            .FirstOrDefault();
                    if (found != null)
                    {
                        tempRoutes.Add(found);
                        distance += found.Distance;
                    }
                }
                results.Add(permutation, distance);

            }

            var sorted = results.OrderBy(x => x.Value);
            var shortest = sorted.FirstOrDefault();
            var longest = sorted.FirstOrDefault();
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
}

