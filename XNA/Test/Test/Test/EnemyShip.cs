using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace Test
{
    public class EnemyShip : Ship
    {
        const string enemyShipSprite = @"Sprites\EnemyShip";

        public Rectangle rect;

        public EnemyShip(Vector2 position, float angle)
            :base (position, angle)
        {
        }

        public override void Load()
        {
            Texture = Scripts.LoadTexture(enemyShipSprite);
            base.Load();
            rect = new Rectangle((int)Position.X - (int)Origin.X, (int)Position.Y - (int)Origin.Y, Texture.Width, Texture.Height);
        }

        public override void Update()
        {
            rect = MathAid.UpdateRectViaVector(rect, Position - Origin);

            for (int i = 0; i < MainShip.Bullets.Count; i++)
            {
                if (MainShip.Bullets[i].rect.Intersects(rect))
                {
                    MainShip.Bullets.Remove(MainShip.Bullets[i]);
                }
            }
        }
    }
}
