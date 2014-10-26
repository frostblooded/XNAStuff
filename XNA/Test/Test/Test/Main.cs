using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Timers;

namespace Test
{
    public class Main : Microsoft.Xna.Framework.Game
    {
        public static ContentManager content;
        public static KeysInput keyboard;
        public static MouseCursor mouse;
        Texture2D mouseTexture;
        int width, height;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Ship> ships;

        public Main()
        {
            GoFullscreenBorderless();
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            content = Content;
            content.RootDirectory = "Content";
            ships = new List<Ship>();

            ships.Add(new EnemyShip(new Vector2(500, 200), 180));
            ships.Add(new EnemyShip(new Vector2(500, 300), 180));
            ships.Add(new MainShip(new Vector2(100, 200), 0));

            keyboard = new KeysInput();
            mouse = new MouseCursor(width, height, 100);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            mouseTexture = Scripts.LoadTexture(@"Sprites\BigAssCursor");

            foreach (var ship in ships)
            {
                ship.Load();
            }

            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (this.IsActive)
            {
                keyboard.Update(gameTime);
                mouse.UpdateMouse(gameTime);

                if (keyboard.IsHeld(Keys.Escape))
                {
                    this.Exit();
                }

                foreach (var ship in ships)
                {
                    ship.Update();
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            foreach (var ship in ships)
            {
                ship.Draw(spriteBatch);
            }

            spriteBatch.Draw(mouseTexture, mouse.Position, null, Color.White, 0, new Vector2(mouseTexture.Width / 2, mouseTexture.Height / 2), 1, SpriteEffects.None, 1f);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void GoFullscreenBorderless()
        {
            IntPtr hWnd = this.Window.Handle;
            var control = System.Windows.Forms.Control.FromHandle(hWnd);
            var form = control.FindForm();
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            form.WindowState = System.Windows.Forms.FormWindowState.Maximized; 
            width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        }
    }
}
