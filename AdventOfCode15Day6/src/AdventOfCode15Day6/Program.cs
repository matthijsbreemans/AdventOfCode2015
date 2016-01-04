using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode15Day6
{
    public class Program
    {
        public void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            //var input = new string[]
            //{
            //   "toggle 0,0 through 999,0"
            //};
            Part1(input);

            Part2(input);

        }


        public static void Part2(string[] input)
        {
            var grid = new int[1000, 1000];
            var list = new List<int>();
            foreach (var line in input)
            {
                var split = line.Split(' ');
                list = new List<int>();
                if (split[0] == "turn")
                {
                    var on = split[1] == "on";

                    var startX = int.Parse(split[2].Split(',')[0]);
                    var startY = int.Parse(split[2].Split(',')[1]);
                    var endX = int.Parse(split[4].Split(',')[0]);
                    var endY = int.Parse(split[4].Split(',')[1]);

                    for (var i = 0; i < 1000; i++)
                    {
                        for (var j = 0; j < 1000; j++)
                        {
                            if (i >= startX && i <= endX && j >= startY && j <= endY)
                            {
                                if (on || (grid[i, j] != 0))
                                {
                                    grid[i, j] = split[1] == "on" ? grid[i, j] + 1 : grid[i, j] - 1;
                                }
                            }
                            list.Add(grid[i, j]);
                        }
                    }
                }
                else if (split[0] == "toggle")
                {
                    var startX = int.Parse(split[1].Split(',')[0]);
                    var startY = int.Parse(split[1].Split(',')[1]);
                    var endX = int.Parse(split[3].Split(',')[0]);
                    var endY = int.Parse(split[3].Split(',')[1]);

                    for (var i = 0; i < 1000; i++)
                    {
                        for (var j = 0; j < 1000; j++)
                        {
                            if (i >= startX && i <= endX && j >= startY && j <= endY)
                            {
                                grid[i, j] = grid[i, j] + 2;
                            }
                            list.Add(grid[i, j]);
                        }
                    }
                }
            }
            var total = list.Sum();
        }


        public static void Part1(string[] input)
        {
            var grid = new bool[1000, 1000];
            var list = new List<bool>();
            foreach (var line in input)
            {
                var split = line.Split(' ');
                list = new List<bool>();
                if (split[0] == "turn")
                {
                    var on = split[1] == "on";

                    var startX = int.Parse(split[2].Split(',')[0]);
                    var startY = int.Parse(split[2].Split(',')[1]);
                    var endX = int.Parse(split[4].Split(',')[0]);
                    var endY = int.Parse(split[4].Split(',')[1]);

                    for (var i = 0; i < 1000; i++)
                    {
                        for (var j = 0; j < 1000; j++)
                        {
                            if (i >= startX && i <= endX && j >= startY && j <= endY)
                            {
                                grid[i, j] = @on;
                            }
                            list.Add(grid[i, j]);
                        }
                    }
                }
                else if (split[0] == "toggle")
                {
                    var startX = int.Parse(split[1].Split(',')[0]);
                    var startY = int.Parse(split[1].Split(',')[1]);
                    var endX = int.Parse(split[3].Split(',')[0]);
                    var endY = int.Parse(split[3].Split(',')[1]);

                    for (var i = 0; i < 1000; i++)
                    {
                        for (var j = 0; j < 1000; j++)
                        {
                            if (i >= startX && i <= endX && j >= startY && j <= endY)
                            {
                                grid[i, j] = !grid[i, j];
                            }
                            list.Add(grid[i, j]);
                        }
                    }
                }
            }
            var total = list.Count(x => x);
        }
    }
}


