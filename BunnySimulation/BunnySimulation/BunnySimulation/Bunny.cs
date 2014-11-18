using System;
using System.Collections.Generic;
namespace BunnySimulation
{
    public enum Sex
    {
        Male,
        Female
    }

    public class Bunny
    {
        public const int evilBunnyChance = 2;
        public static List<string> bunnyNames = new List<string>
        {
            "Thumper",
            "Fufu",
            "Billy",
            "Bazzle",
            "Simba",
            "Wiggles",
            "Flo",
            "Elfie"
        };

        public Bunny()
        {
            Sex = (Sex)Main.rand.Next(2);
            Age = 0;
            Name = Bunny.bunnyNames[Main.rand.Next(bunnyNames.Count)];
            Evil = IsItEvil();

            if (Evil)
            {
                Console.Write("Evil ");
            }

            Console.WriteLine("Bunny {0} was born.", Name);
        }

        private bool IsItEvil()
        {
            if (Main.rand.Next(100) < evilBunnyChance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Sex Sex { get; set; }

        public int Age { get; set; }

        public string Name { get; set; }

        public bool Evil { get; set; }
    }
}
