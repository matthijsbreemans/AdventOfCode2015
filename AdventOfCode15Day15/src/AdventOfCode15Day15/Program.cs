using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode15Day15
{
    public class Program
    {

        public static List<Ingredient> ingredients = File.ReadAllLines("input.txt").Select(x => new Ingredient(x)).ToList();

        public void Main(string[] args)
        {
            var cookies = new List<Cookie>();

            //all possible combinations
            for (var sprinkles = 0; sprinkles < 100; sprinkles++)
            {
                for (var butterscotch = 0; butterscotch < 100 - sprinkles; butterscotch++)
                {
                    for (var chocolate = 0; chocolate < 100 - sprinkles - butterscotch; chocolate++)
                    {
                        var candy = 100 - sprinkles - butterscotch - chocolate;
                        cookies.Add(new Cookie()
                        {
                            Butterscotch = butterscotch,
                            Candy = candy,
                            Chocolate = chocolate,
                            Sprinkles = sprinkles
                        });
                    }
                }

            }

            var result = cookies.Where(x => x.Calories == 500).OrderBy(x => x.Result).Last().Result;
        }

        public static Ingredient GetIngredient(string i)
        {
            return ingredients.Single(x => x.Name == i);
        }
        public class Cookie
        {
            public int Candy { get; set; }
            public int Sprinkles { get; set; }
            public int Butterscotch { get; set; }
            public int Chocolate { get; set; }

            public int Result => CheckForZero(Capacity) * CheckForZero(Durability) * CheckForZero(Flavor) * CheckForZero(Texture);

            private int CheckForZero(int i)
            {
                return i < 0 ? 0 : i;
            }

            public int Capacity => GetIngredient("Butterscotch").Capacity * Butterscotch
                                   +
                                   GetIngredient("Candy").Capacity * Candy
                                   +
                                   GetIngredient("Sprinkles").Capacity * Sprinkles
                                   +
                                   GetIngredient("Chocolate").Capacity * Chocolate;

            public int Durability => GetIngredient("Butterscotch").Durability * Butterscotch
                                     +
                                     GetIngredient("Candy").Durability * Candy
                                     +
                                     GetIngredient("Sprinkles").Durability * Sprinkles
                                     +
                                     GetIngredient("Chocolate").Durability * Chocolate;

            public int Flavor => GetIngredient("Butterscotch").Flavor * Butterscotch
                                 +
                                 GetIngredient("Candy").Flavor * Candy
                                 +
                                 GetIngredient("Sprinkles").Flavor * Sprinkles
                                 +
                                 GetIngredient("Chocolate").Flavor * Chocolate;

            public int Texture => GetIngredient("Butterscotch").Texture * Butterscotch
                                  +
                                  GetIngredient("Candy").Texture * Candy
                                  +
                                  GetIngredient("Sprinkles").Texture * Sprinkles
                                  +
                                  GetIngredient("Chocolate").Texture * Chocolate;

            public int Calories => GetIngredient("Butterscotch").Calories * Butterscotch
                                   +
                                   GetIngredient("Candy").Calories * Candy
                                   +
                                   GetIngredient("Sprinkles").Calories * Sprinkles
                                   +
                                   GetIngredient("Chocolate").Calories * Chocolate;
        }

    }


    public class Ingredient
    {
        //capacity -1, durability -2, flavor 6, texture 3, calories 8
        public int Capacity { get; set; }
        public int Durability { get; set; }
        public int Flavor { get; set; }
        public int Texture { get; set; }
        public int Calories { get; set; }

        public string Name { get; set; }

        public Ingredient(string input)
        {
            var numbers = Regex.Matches(input, @"([-]?\d+)");
            Capacity = int.Parse(numbers[0].Value);
            Durability = int.Parse(numbers[1].Value);
            Flavor = int.Parse(numbers[2].Value);
            Texture = int.Parse(numbers[3].Value);
            Calories = int.Parse(numbers[4].Value);

            Name = input.Split(':').First();
        }

        private int PositiveCalc(int i, int j)
        {
            var x = i * j;
            return x < 0 ? 0 : x;
        }
        public int Calculate(int spoons)
        {
            return PositiveCalc(spoons, Capacity) + PositiveCalc(spoons, Durability) + PositiveCalc(spoons, Flavor) +
                   PositiveCalc(spoons, Texture); 
        }
    }
}

