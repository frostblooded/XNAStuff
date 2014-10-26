using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Test
{
    public class Bullet : Entity
    {
        public const int speed = 20;
        const string bulletSprite = @"Sprites\ShipBullet";

        public Rectangle rect;

        public Bullet(Vector2 position, float angle)
        {
            Position = position;
            Angle = MathHelper.ToRadians(angle);
            Direction = GetDir(angle);
            Load();
            Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
            rect = new Rectangle((int)Position.X - (int)Origin.X, (int)Position.Y - (int)Origin.Y, Texture.Width, Texture.Height);
        }

        public Vector2 Direction { get; set; }

        private Vector2 GetDir(float angle)
        {
            if (angle == 0)
            {
                return new Vector2(speed, 0);
            }
            else if (angle == 90)
            {
                return new Vector2(0, speed);
            }
            else if (angle == 180)
            {
                return new Vector2(-speed, 0);
            }
            else if (angle == -90)
            {
                return new Vector2(0, -speed);
            }

            return new Vector2(69, 69);
        }

        public  override void Load()
        {
            Texture = Scripts.LoadTexture(bulletSprite);
        }

        public  override void Update()
        {
            Position += Direction;
            rect = MathAid.UpdateRectViaVector(rect, Position - Origin);
        }

        public  override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, Angle, Origin, 1, SpriteEffects.None, 1f);
        }
    }
}
