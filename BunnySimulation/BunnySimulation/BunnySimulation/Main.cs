using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using System.Threading;

namespace BunnySimulation
{
    public class Main : Game
    {
        public const int bunniesCap = 200;

        public static int width;
        public static int height;
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        public static ContentManager content;
        public static MouseCursor mouse;
        public static KeysInput keyboard;
        public static Random rand;
        public static int currentTurn;

        private TimeSpan lastTurnChange = new TimeSpan(0, 0, 0);
        private TimeSpan secondsBetweenTurns = new TimeSpan(0, 0, 1);

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

            if (keyboard.JustPressed(Keys.Space) || lastTurnChange + secondsBetweenTurns < gameTime.TotalGameTime)
            {
                AdvanceTurns();
                lastTurnChange = gameTime.TotalGameTime;
            }

            if (Grid.bunnies.Count > bunniesCap || keyboard.JustPressed(Keys.K))
            {
                Massacre();
            }

            mouse.UpdateMouse(gameTime);
            keyboard.Update(gameTime);

            UpdateConsole();

            base.Update(gameTime);
        }

        private void UpdateConsole()
        {
            Console.Clear();

            var evilBunniesCount = Grid.bunnies.Count(bun => bun.Evil);

            Console.WriteLine("Bunnies: {0} ({1} normal and {2} evil)", Grid.bunnies.Count, Grid.bunnies.Count - evilBunniesCount, evilBunniesCount);
        }

        public void Massacre()
        {
            for (int i = 0; i < bunniesCap / 2; i++)
            {
                Grid.bunnies[Main.rand.Next(Grid.bunnies.Count)].Die(); 
            }

            Console.WriteLine("Massacre!!!");
            Console.WriteLine("Massacre!!!");
            Console.WriteLine("Massacre!!!");
            Console.WriteLine("Massacre!!!");
            Thread.Sleep(1000);
        }

        public void AdvanceTurns()
        {
            currentTurn++;
            Console.WriteLine("Turn: " + currentTurn);

            for (int i = 0; i < Grid.bunnies.Count; i++)
            {
                Grid.bunnies[i].IncreaseAge();

                if (Grid.bunnies[i].Evil)
                {
                    Grid.bunnies[i].MakeAdjacentBunnyEvil();
                }

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

            var evilBunnies =
                from bunny in Grid.bunnies
                where bunny.Evil
                select bunny;
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
