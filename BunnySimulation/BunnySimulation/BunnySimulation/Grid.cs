
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
        public static List<Bunny> bunniesForRemoval;
        public static Texture2D adultTexture;
        public static Texture2D youngTexture;
        public static Texture2D gridTexture;

        public static void Initialize()
        {
            adultTexture = Scripts.LoadTexture(@"BunnyLetters\AdultBunny");
            youngTexture = Scripts.LoadTexture(@"BunnyLetters\YoungBunny");
            gridTexture = Scripts.LoadTexture("Grid");

            bunnies = new List<Bunny>();
            bunniesForRemoval = new List<Bunny>();
            CreateStartingBunnies();
        }

        private static void CreateStartingBunnies()
        {
            for (int i = 0; i < startingBunnies; i++)
            {
                bunnies.Add(new Bunny());
            }
        }

        public static Vector2 GetCell(int Y, int X)
        {
            return new Vector2(X * 23 + 2, Y * 23 + 2);
        }

        public static bool IsCellEmpty(int height, int width)
        {
            foreach (var bunny in bunnies)
            {
                if (bunny.Position.X == width && bunny.Position.Y == height)
                {
                    return false;
                }
            }

            return true;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(gridTexture, new Vector2(), Color.White);
            bunnies.ForEach(bun => bun.Draw(spriteBatch));
        }
    }
}
