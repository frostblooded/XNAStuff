using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace Test
{
    public static class Scripts
    {
        public static Texture2D LoadTexture(string asset)
        {
            try
            {
                return Main.content.Load<Texture2D>(asset) as Texture2D;
            }
            catch (ContentLoadException)
            {
                Console.WriteLine("Texture not found: " + asset);
                return null;
            }
        }
    }
}
