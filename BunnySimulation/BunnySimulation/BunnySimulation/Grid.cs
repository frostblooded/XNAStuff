
using Microsoft.Xna.Framework;
namespace BunnySimulation
{
    public static class Grid
    {
        private static int width;
        private static int height;

        public static void Initialize()
        {
            width = Main.width;
            height = Main.height;
        }

        public static Vector2 GetCell(int height, int width)
        {
            return new Vector2(height * 23 + 2, width * 23 + 2);
        }
    }
}
