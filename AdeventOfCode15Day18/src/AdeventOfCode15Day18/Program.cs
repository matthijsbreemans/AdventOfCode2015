using System.IO;
using System.Linq;

namespace AdeventOfCode15Day18
{
    public class Program
    {
        public void Main(string[] args)
        {

            var input = File.ReadAllText("input.txt").Replace("\r\n", "").ToCharArray().Select(x => x == '#').ToArray();
            var grid = new Grid(100, 100, input);
            for (var i = 0; i < 100; i++)
            {
                grid.Run();
            }
            var part1 = grid.Lights.ToList().Count(x => x);

        }


    }

    public class Grid
    {

        public int Width { get; set; }
        public int Height { get; set; }

        public bool[] Lights { get; set; }


        public Grid(int width, int height, bool[] lights)
        {
            Width = width;
            Height = height;
            Lights = lights;
        }



        public void Run()
        {
            var newCells = new bool[Width * Height];

            //for part2
            newCells[Width * Height - 1] = newCells[Width * Height - Height] = newCells[Width - 1] = newCells[0] = true;

            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    var onLightsCount = GetSurroundings(x, y);

                    if (GetLight(x, y) && onLightsCount == 2 || onLightsCount == 3)
                    {
                        newCells[Width * x + y] = true;
                    }
                }
            }

            Lights = newCells;
        }
        public bool GetLight(int x, int y)
        {
            return Lights[Width * x + y];
        }
        public bool IsInBounds(int x, int y)
        {
            return (x >= 0 && x < Width && y >= 0 && y < Height);
        }

        public int GetSurroundings(int x, int y)
        {
            var i = GetLight(x, y) ? -1 : 0;

            for (var xx = 0; xx < 3; xx++)
            {
                for (var yy = 0; yy < 3; yy++)
                {
                    if (IsInBounds(x + xx - 1, y + yy - 1) && GetLight(x + xx - 1, y + yy - 1))
                    {
                        i++;
                    }
                }
            }

            return i;
        }

    }
}


