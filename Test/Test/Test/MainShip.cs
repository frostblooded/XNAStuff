using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
namespace Test
{
    public class MainShip : Ship
    {
        const string shipSprite = @"Sprites\SpaceShip";

        public MainShip(Vector2 position, float angle)
            :base (position, angle)
        {
            Bullets = new List<Bullet>();
        }

        public static List<Bullet> Bullets { get; set; }
        public Texture2D BulletTexture { get; set; }

        public override void Load()
        {
            Texture = Scripts.LoadTexture(shipSprite);
 	        base.Load();
        }

        public override void Update()
        {
            foreach (var bullet in Bullets)
            {
                bullet.Update();
            }

            HandleInput();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var bullet in Bullets)
            {
                bullet.Draw(spriteBatch);
            }

            base.Draw(spriteBatch);
        }

        private void Shoot()
        {
            Bullets.Add(new Bullet(Position, MathHelper.ToDegrees(Angle)));
        }

        private void HandleInput()
        {
            if (Main.keyboard.IsHeld(Keys.W))
            {
                Position = new Vector2(Position.X, Position.Y - 10);
                Angle = MathHelper.ToRadians(-90f);
            }

            if (Main.keyboard.IsHeld(Keys.A))
            {
                Position = new Vector2(Position.X - 10, Position.Y);
                Angle = MathHelper.ToRadians(180f);
            }

            if (Main.keyboard.IsHeld(Keys.S))
            {
                Position = new Vector2(Position.X, Position.Y + 10);
                Angle = MathHelper.ToRadians(90f);
            }

            if (Main.keyboard.IsHeld(Keys.D))
            {
                Position = new Vector2(Position.X + 10, Position.Y);
                Angle = 0;
            }

            if (Main.keyboard.JustPressed(Keys.Space))
            {
                Shoot();
            }

            if (Main.mouse.LeftClick())
            {
                Shoot();
            }
        }
    }
}
