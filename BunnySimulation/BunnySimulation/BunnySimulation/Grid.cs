
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
namespace BunnySimulation
{
    public static class Grid
    {
        public const int cellsPerRow = 30;
        public const int startingBunnies = 5;

        public static List<Bunny> bunnies;
        private static Texture2D adultTexture;
        private static Texture2D youngTexture;
        private static Texture2D gridTexture;

        public static void Initialize()
        {
            adultTexture = Scripts.LoadTexture(@"BunnyLetters\AdultBunny");
            youngTexture = Scripts.LoadTexture(@"BunnyLetters\YoungBunny");

            gridTexture = Scripts.LoadTexture("Grid");
            bunnies = new List<Bunny>();
            CreateStartingBunnies();
        }

        private static void CreateStartingBunnies()
        {
            for (int i = 0; i < startingBunnies; i++)
            {
                bunnies.Add(new Bunny());
            }
        }

        public static Vector2 GetCell(int height, int width)
        {
            return new Vector2(height * 23 + 2, width * 23 + 2);
        }
    }
}
