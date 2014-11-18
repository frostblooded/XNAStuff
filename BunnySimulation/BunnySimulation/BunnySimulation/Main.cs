using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

namespace BunnySimulation
{
    public class Main : Game
    {
        public static int width;
        public static int height;

        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        public static ContentManager content;
        public static MouseCursor mouse;
        public static KeysInput keyboard;
        public static Random rand;
        public static int currentTurn;


        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            content = Content;
            rand = new Random();
        }

        protected override void Initialize()
        {
            Grid.Initialize();
            HandleWindowSize();
            mouse = new MouseCursor(width, height, 1000);
            keyboard = new KeysInput();
            currentTurn = 0;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        private void HandleWindowSize()
        {
            width = Grid.gridTexture.Width;
            height = Grid.gridTexture.Height;

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

            if (keyboard.JustPressed(Keys.Space))
            {
                AdvanceTurns();
            }

            mouse.UpdateMouse(gameTime);
            keyboard.Update(gameTime);
            base.Update(gameTime);
        }

        public void AdvanceTurns()
        {
            currentTurn++;
            Console.WriteLine("Turn: " + currentTurn);

            for (int i = 0; i < Grid.bunnies.Count; i++)
            {
                Grid.bunnies[i].IncreaseAge();
                Grid.bunnies[i].Move();

                var adultMaleBunniesCount = Grid.bunnies.Count<Bunny>(bun => bun.Adult && bun.Sex == Sex.Male);

                if (Grid.bunnies[i].Sex == Sex.Female && adultMaleBunniesCount > 0)
                {
                    Grid.bunnies[i].GiveBirth();
                }
            }

            //Kill aged bunnies
            foreach (var bunny in Grid.bunniesForRemoval)
            {
                Grid.bunnies.Remove(bunny);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.ForestGreen);
            spriteBatch.Begin();

            Grid.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
