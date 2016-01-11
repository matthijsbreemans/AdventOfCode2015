using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode15Day16
{
    public class Program
    {
        public void Main(string[] args)
        {
            var sueToMatch = new Sue("Testsue: children: 3, cats: 7, samoyeds: 2, pomeranians: 3, akitas: 0, vizslas: 0, goldfish: 5, trees: 3, cars: 2, perfumes: 1");
            var input = File.ReadAllLines("input.txt").Select(x => new Sue(x)).Single(x => x.Match(sueToMatch));
        }



        public class Sue
        {
            public string Name { get; set; }
            public int? Children { get; set; }
            public int? Cats { get; set; }
            public int? Samoyeds { get; set; }
            public int? Pomeranians { get; set; }
            public int? Akitas { get; set; }
            public int? Vizslas { get; set; }
            public int? Goldfish { get; set; }
            public int? Trees { get; set; }
            public int? Cars { get; set; }
            public int? Perfumes { get; set; }

            public Sue(string input)
            {
                Name = input.Split(':').First();

                foreach (Match match in Regex.Matches(input, "(([\\w]+): ([-]?[\\d]+))"))
                {
                    typeof(Sue).GetProperties().Single(x => x.PropertyType == typeof(int?) && x.Name.ToLower() == match.Groups[2].Value).SetValue(this, int.Parse(match.Groups[3].Value));
                }
            }

            public bool Match(Sue sueToMatch)
            {
                int correct = 0;
                if (Cats.HasValue && Cats.Value > sueToMatch.Cats.Value)
                    correct++;

                if (Samoyeds.HasValue && Samoyeds.Value == sueToMatch.Samoyeds.Value)
                    correct++;

                if (Pomeranians.HasValue && Pomeranians.Value < sueToMatch.Pomeranians.Value)
                    correct++;

                if (Akitas.HasValue && Akitas.Value == sueToMatch.Akitas.Value)
                    correct++;

                if (Vizslas.HasValue && Vizslas.Value == sueToMatch.Vizslas.Value)
                    correct++;

                if (Goldfish.HasValue && Goldfish.Value < sueToMatch.Goldfish.Value)
                    correct++;

                if (Trees.HasValue && Trees.Value > sueToMatch.Trees.Value)
                    correct++;

                if (Cars.HasValue && Cars.Value == sueToMatch.Cars.Value)
                    correct++;
                if (Perfumes.HasValue && Perfumes.Value == sueToMatch.Perfumes.Value)
                    correct++;
                if (Children.HasValue && Children.Value == sueToMatch.Children.Value)
                    correct++;
                return correct == 3;
            }
        }
    }
}
