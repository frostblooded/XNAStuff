using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
namespace Test
{
    public abstract class Ship : Entity
    {

        public Ship(Vector2 position, float angle)
        {
            Position = position;
            Angle = MathHelper.ToRadians(angle);
        }

        public override void Load()
        {
            Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
        }

        public override void Update() { }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, Angle, Origin, 1, SpriteEffects.None, 1f);
        }
    }
}
