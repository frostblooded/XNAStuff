using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Test
{
    public abstract class Entity
    {
        public Vector2 Position { get; set; }
        public Vector2 Origin { get; set; }
        public Texture2D Texture { get; set; }
        public float Angle { get; set; }

        public abstract void Load();
        public abstract void Update();
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
