using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdventOfCode15Day2
{
    public class Program
    {
        public void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            //var input = new string[]
            //{
            //    "2x3x4", "1x1x10"
            //};
            var total = 0;
            var ribbon = 0;
            foreach (var i in input)
            {
                var split = i.Split('x').Select(int.Parse).ToArray();
                var length = (split[0]);
                var width = (split[1]);
                var height = (split[2]);

                var lengthwidth = (length * width);
                var widthheight = (width * height);
                var heightlength = (height * length);
              

                Console.WriteLine($"l*w = {lengthwidth}. w*h = {widthheight}. h*l = { heightlength}");
                total += (2 * lengthwidth) + (2 * widthheight) + (2 * heightlength) + Lowest(lengthwidth, widthheight, heightlength);

                ribbon += ((split.Sum() - split.Max()) * 2) + (length * width * height);
                
            }
        }

        public static int Lowest(params int[] inputs)
        {
            return inputs.Min();
        }

    }
}
