using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace BunnySimulation
{
    public class Main : Microsoft.Xna.Framework.Game
    {
        public static int width;
        public static int height;
        public const int cellsPerRow = 30;

        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        public static ContentManager content;
        public static MouseCursor mouse;
        public static KeysInput keyboard;

        private Texture2D gridTexture;
        private Texture2D testTexture;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            content = Content;
        }

        protected override void Initialize()
        {
            gridTexture = Scripts.LoadTexture("Grid"); //Nobody sees this
            HandleWindowSize();
            Grid.Initialize();
            mouse = new MouseCursor(width, height, 1000);
            keyboard = new KeysInput();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            testTexture = Scripts.LoadTexture(@"BunnyLetters\AdultBunny");
        }

        private void HandleWindowSize()
        {
            width = gridTexture.Width;
            height = gridTexture.Height;

            IntPtr ptr = this.Window.Handle;
            System.Windows.Forms.Form form = (System.Windows.Forms.Form)System.Windows.Forms.Control.FromHandle(ptr);
            form.Size = new System.Drawing.Size(width, height);

            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;

            graphics.ApplyChanges();
        }

        protected override void Update(GameTime gameTime)
        {
            if (keyboard.JustPressed(Keys.Escape))
            {
                this.Exit();
            }

            mouse.UpdateMouse(gameTime);
            keyboard.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            for (int i = 0; i < cellsPerRow; i++)
            {
                for (int j = 0; j < cellsPerRow; j++)
                {
                    spriteBatch.Draw(testTexture, Grid.GetCell(i, j), Color.Blue);
                }
            }

            spriteBatch.Draw(gridTexture, new Vector2(), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
