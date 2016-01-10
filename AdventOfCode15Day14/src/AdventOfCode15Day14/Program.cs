using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode15Day14
{
    public class Program
    {
        public void Main(string[] args)
        {
            var result = new Dictionary<string, int>();

            List<Reindeer>[] points = new List<Reindeer>[2503];
            
            var deers = new List<Reindeer>();

            foreach (var line in File.ReadAllLines("input.txt"))
            {
                var numbers = Regex.Matches(line, @"(\d+)");

                var isFlying = true;
                var currentStateCount = 0;
                decimal km = 0;

                var runningTime = decimal.Parse(numbers[1].Value);
                var sleeptime = decimal.Parse(numbers[2].Value);
                var speed = decimal.Parse(numbers[0].Value);
                var name = line.Split(' ').First();
                //km = Math.Ceiling(2503 / (runningTime + sleeptime)) * (speed * runningTime); //formula for part1
                for (var i = 0; i < 2503; i++)
                {

                    if ((isFlying && currentStateCount == runningTime) ||
                        (!isFlying && currentStateCount == sleeptime))
                    {
                        isFlying = !isFlying;
                        currentStateCount = 0;
                    }

                    if (isFlying)
                        km += speed;

                    if (points[i] == null)
                    {
                        points[i] = new List<Reindeer>
                        {new Reindeer()
                        {
                            Distance = km,
                            Name = name
                        }};
                    }
                    else
                    {
                        points[i].Add(new Reindeer()
                        {
                            Distance = km,
                            Name = name
                        });
                    }
                    currentStateCount++;

                }
                result.Add(name, (int)km);
                deers.Add(new Reindeer()
                {
                    Name = name
                });
            }

            foreach (var winner in points.Select(p => p.Where(y => y.Distance == p.Max(x => x.Distance))).SelectMany(winners => winners))
            {
                deers.Single(x => x.Name == winner.Name).Points += 1;
            }
            var output = result.OrderBy(x => x.Value).Last(); //part1

            var output2 = deers.OrderByDescending(x => x.Points).First(); //part2


        }

        public class Reindeer
        {
            public string Name { get; set; }
            public decimal Distance { get; set; }
            public int Points { get; set; }
        }
    }
}
