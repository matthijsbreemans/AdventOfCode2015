using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode15Day7
{
    public class Program
    {

        public static Dictionary<string, ushort> dict = new Dictionary<string, ushort>();
        public void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            Part1(input);

        }

        public static void Save(Rule rule)
        {
            if (dict.ContainsKey(rule.To))
            {
                dict[rule.To] = (ushort)(rule.Value);
            }
            else
            {
                dict.Add(rule.To, (ushort)(rule.Value));
            }
            rule.Processed = true;
        }

        public class Rule
        {
            public Rule(Operators operand, string to, string operandOne, string operandTwo)
            {
                this.Operation = operand;
                this.To = to;
                this.First = operandOne;
                this.Second = operandTwo;
            }
            public Operators Operation { get; set; }
            public bool Processed { get; set; }
            public string To { get; set; }
            public string First { get; set; }

            public ushort? SecondValue { get; set; }
            public ushort? FirstValue { get; set; }


            public string Second { get; set; }
            public ushort? Value { get; set; }
        }

        public enum Operators
        {
            AND, OR, NOT, LSHIFT, RSHIFT, Provided

        }
        public static List<Rule> Parse(string[] input)
        {
            var rules = new List<Rule>();
            foreach (var line in input)
            {
                var split = line.Split(' ');
                var upperChars = line.Where(char.IsUpper).Select(x => x.ToString());
                var op = upperChars.Any() ? upperChars.Aggregate((cur, next) => cur + next) : "";
                var to = split.Last();

                if (op == "AND")
                {
                    rules.Add(new Rule(Operators.AND, to, split[0], split[2]));
                    //Save(dict, to, dict[split[0]] & dict[split[2]]);
                }
                else if (op == "OR")
                {
                    rules.Add(new Rule(Operators.OR, to, split[0], split[2]));
                    //Save(dict, to, dict[split[0]] | dict[split[2]]);
                }
                else if (op == "NOT")
                {
                    rules.Add(new Rule(Operators.NOT, to, split[1], null));
                    //Save(dict, to, ~dict[split[1]]);
                }
                else if (op == "LSHIFT")
                {
                    rules.Add(new Rule(Operators.LSHIFT, to, split[0], split[2]));
                    //Save(dict, to, dict[split[0]] << ushort.Parse(split[2]));
                }
                else if (op == "RSHIFT")
                {
                    rules.Add(new Rule(Operators.RSHIFT, to, split[0], split[2]));
                    //Save(dict, to, dict[split[0]] >> ushort.Parse(split[2]));
                }
                else
                {
                    rules.Add(new Rule(Operators.Provided, to, split[0], null));
                    //Save(dict, to, ushort.Parse(split[0]));
                }
            }
            return rules;
        }

        public static void Part1(string[] input)
        {


            var rules = Parse(input);
        

            Process(rules);

            var output = dict["a"];

            var newInput = input.ToList();
            var b = newInput.IndexOf(newInput.First(x => x.EndsWith(" -> b")));
            newInput.RemoveRange(b, 1);
            newInput.Insert(b, output + " -> b");

            rules = Parse(newInput.ToArray());
            dict = new Dictionary<string, ushort>();

            Process(rules);
            var secondOutput = dict["a"];
        }

        private static void Process(List<Rule> rules)
        {
            int i = 0;
            while (rules.Any(x => !x.Processed))
            {

                var rule = rules[i % rules.Count];


                switch (rule.Operation)
                {
                    case Operators.AND:
                        ProcessAND(rule);
                        break;
                    case Operators.OR:
                        ProcessOR(rule);
                        break;
                    case Operators.NOT:
                        ProcessNOT(rule);
                        break;
                    case Operators.LSHIFT:
                        ProcessLSHIFT(rule);
                        break;
                    case Operators.RSHIFT:
                        ProcessRSHIFT(rule);
                        break;
                    case Operators.Provided:
                        ProcessInsert(rule);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                SetValues(rules, rule);

                i++;
            }
        }

        private static void SetValues(List<Rule> rules, Rule value)
        {
            if (value.Processed && value.Value.HasValue)
            {

                foreach (var rule in rules)
                {
                    if (rule.First == value.To)
                    {
                        rule.FirstValue = value.Value;
                    }
                    else if (rule.Second == value.To)
                    {
                        rule.SecondValue = value.Value;
                    }
                }
            }
        }

        private static void ProcessInsert(Rule rule)
        {
            ushort first;
            if (ushort.TryParse(rule.First, out first) || rule.FirstValue.HasValue)
            {
                if (rule.FirstValue.HasValue)
                {
                    first = rule.FirstValue.Value;
                }

                rule.Value = (ushort)(first);
                Save(rule);
            }

        }

        private static void ProcessRSHIFT(Rule rule)
        {
            ushort first = 0;
            ushort second = 0;
            if ((rule.FirstValue.HasValue || ushort.TryParse(rule.First, out first)) && (rule.SecondValue.HasValue || ushort.TryParse(rule.Second, out second)))
            {
                if (rule.FirstValue.HasValue)
                {
                    first = rule.FirstValue.Value;
                }
                if (rule.SecondValue.HasValue)
                {
                    second = rule.SecondValue.Value;
                }
                
                rule.Value = (ushort)(first >> second);
                Save(rule);
            }
        }

        private static void ProcessLSHIFT(Rule rule)
        {
            ushort first = 0;
            ushort second = 0;
            if ((rule.FirstValue.HasValue || ushort.TryParse(rule.First, out first)) && (rule.SecondValue.HasValue || ushort.TryParse(rule.Second, out second)))
            {
                if (rule.FirstValue.HasValue)
                {
                    first = rule.FirstValue.Value;
                }
                if (rule.SecondValue.HasValue)
                {
                    second = rule.SecondValue.Value;
                }

                rule.Value = (ushort)(first << second);
                Save(rule);
            }
        }

        private static void ProcessNOT(Rule rule)
        {
            ushort first;
            if (ushort.TryParse(rule.First, out first) || rule.FirstValue.HasValue)
            {
                if (rule.FirstValue.HasValue)
                {
                    first = rule.FirstValue.Value;
                }

                rule.Value = (ushort)(~first);
                Save(rule);
            }
        }

        private static void ProcessOR(Rule rule)
        {
            ushort first = 0;
            ushort second = 0;
            if ((rule.FirstValue.HasValue || ushort.TryParse(rule.First, out first)) && (rule.SecondValue.HasValue || ushort.TryParse(rule.Second, out second)))
            {
                if (rule.FirstValue.HasValue)
                {
                    first = rule.FirstValue.Value;
                }
                if (rule.SecondValue.HasValue)
                {
                    second = rule.SecondValue.Value;
                }

                rule.Value = (ushort)(first | second);
                Save(rule);
            }
        }

        private static void ProcessAND(Rule rule)
        {
            ushort first = 0;
            ushort second = 0;
            if ((rule.FirstValue.HasValue || ushort.TryParse(rule.First, out first)) && (rule.SecondValue.HasValue || ushort.TryParse(rule.Second, out second)))
            {
                if (rule.FirstValue.HasValue)
                {
                    first = rule.FirstValue.Value;
                }
                if (rule.SecondValue.HasValue)
                {
                    second = rule.SecondValue.Value;
                }

                rule.Value = (ushort)(first & second);
                Save(rule);
            }
        }
    }
}
