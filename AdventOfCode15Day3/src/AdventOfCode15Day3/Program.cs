using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode15Day3
{
    public class Program
    {
        public void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt").ToCharArray();

            var houses = new List<string>();

            var santaPos = new Pos(0, 0);
            var roboSantaPos = new Pos(0, 0);

            var curPos = santaPos;
            bool isSanta = true;
            foreach (var direction in input)
            {
                curPos = isSanta ? santaPos : roboSantaPos;

                houses.Add(curPos.ToString());
                Move(curPos, direction);
                houses.Add(curPos.ToString());
                isSanta = !isSanta;
            }

            var uniquehouses = houses.Distinct().Count();


        }

        public static void Move(Pos pos, char direction)
        {
            switch (direction)
            {
                case '>':
                    pos.X += 1;
                    break;
                case '<':
                    pos.X -= 1;
                    break;
                case '^':
                    pos.Y += 1;
                    break;
                case 'v':
                    pos.Y -= 1;
                    break;
            }
        }


        public class Pos
        {
            public Pos(int x, int y)
            {
                X = x; Y = y;
            }
            public int X { get; set; }
            public int Y { get; set; }

            public override string ToString()
            {
                return X + "x" + Y;
            }
        }

    }
}
