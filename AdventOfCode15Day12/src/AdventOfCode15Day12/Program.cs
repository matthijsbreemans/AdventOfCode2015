using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AdventOfCode15Day12
{
    public class Program
    {
        public void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");


            //part1 
            var regexResult = Regex.Matches(input, @"([\-\+]?\s*\d+\s*)");
            var total = 0;
            foreach (Match x in regexResult)
            {
                var calc = int.Parse(x.Value);
                total += calc;
            }
            var json = JArray.Parse(input);
            var i = test(json);

        }


        public static int test(JToken token)
        {
            if (token is JArray)
            {
                return ((JArray)token).Sum(test);
            }
            if (token is JObject)
            {
                var jo = (JObject)token;
                return jo.Properties().Select(p => p.Value).OfType<JValue>().Select(j => j.Value).OfType<string>().Any(j => j == "red") ? 0 : jo.Properties().Select(p => p.Value).Sum(test);
            }
            if (token is JValue)
            {
                int z = 0;
                return int.TryParse(((JValue)token).Value.ToString(), out z) ? z : 0;
            }
            return 0;
        }
    }
}
