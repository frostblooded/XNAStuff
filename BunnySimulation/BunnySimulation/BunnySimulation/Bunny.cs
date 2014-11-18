using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using System.Collections.Generic;

namespace BunnySimulation
{
    public enum Sex
    {
        Male,
        Female
    }

    enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

    public class Bunny
    {
        public const int evilBunnyChance = 2;
        public static Color maleColor = Color.Blue;
        public static Color femaleColor = Color.Red;
        public static Color evilBunnyColor = Color.Black;

        public static List<string> bunnyNames = new List<string>
        {
            "Thumper",
            "Fufu",
            "Billy",
            "Bazzle",
            "Simba",
            "Wiggles",
            "Flo",
            "Elfie"
        };

        public Bunny()
        {
            Sex = (Sex)Main.rand.Next(2);
            Age = 0;
            Name = Bunny.bunnyNames[Main.rand.Next(bunnyNames.Count)];
            Evil = IsItEvil();
            Position = GetEmptyCellPosition();

            if (Evil)
            {
                Console.Write("Evil ");
            }

            Console.WriteLine("Bunny {0} was born. : {1}", Name, Sex);
        }

        public Bunny(Vector2 position)
            :this()
        {
            Position = position;
        }

        private bool IsItEvil()
        {
            if (Main.rand.Next(100) < evilBunnyChance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Vector2 GetEmptyCellPosition()
        {
            Vector2 newPosition;

            do
            {
                newPosition = new Vector2(Main.rand.Next(Grid.cellsPerRow), Main.rand.Next(Grid.cellsPerRow)); 
            } while (!Grid.IsCellEmpty((int)newPosition.Y, (int)newPosition.X));

            return newPosition;
        }

        public Sex Sex { get; set; }

        public Vector2 Position { get; set; } //This position is not measured in pixels, but in cells

        public int Age { get; set; }

        public string Name { get; set; }

        public bool Evil { get; set; }

        public bool Adult
        {
            get
            {
                return Age >= 2 && !Evil;
            }
        }

        public void Move()
        {
            List<Direction> possibleDirections = GetPossibleDirections();

            if (possibleDirections.Count > 0)
            {
                MoveInDirection(possibleDirections[Main.rand.Next(possibleDirections.Count)]); 
            }
        }

        public void GiveBirth()
        {
            var availableDirections = GetPossibleDirections();

            if (availableDirections.Count > 0)
            {
                Direction currentDirection = availableDirections[Main.rand.Next(availableDirections.Count)];

                if (currentDirection == Direction.Up)
                {
                    Grid.bunnies.Add(new Bunny(new Vector2(Position.X, Position.Y - 1)));
                }
                else if (currentDirection == Direction.Right)
                {
                    Grid.bunnies.Add(new Bunny(new Vector2(Position.X + 1, Position.Y)));
                }
                else  if (currentDirection == Direction.Down)
                {
                    Grid.bunnies.Add(new Bunny(new Vector2(Position.X, Position.Y + 1)));
                }
                else if (currentDirection == Direction.Left)
                {
                    Grid.bunnies.Add(new Bunny(new Vector2(Position.X - 1, Position.Y)));
                }
            }
        }

        public void IncreaseAge()
        {
            Age++;

            if (!Evil && Age > 10)
            {
                Die();
            }
            else if (Evil && Age > 50)
            {
                Die();
            }
        }

        public void Die()
        {
            Grid.bunniesForRemoval.Add(this);
            Console.WriteLine("Bunny" + Name + "Has died!");
        }

        private void MoveInDirection(Direction direction)
        {
            if (direction == Direction.Up)
            {
                Position = new Vector2(Position.X, Position.Y - 1);
            }
            else if (direction == Direction.Right)
            {
                Position = new Vector2(Position.X + 1, Position.Y);
            }
            else if (direction == Direction.Down)
            {
                Position = new Vector2(Position.X, Position.Y + 1);
            }
            else if (direction == Direction.Left)
            {
                Position = new Vector2(Position.X - 1, Position.Y);
            } 
            
            if (Position.Y < 0 || Position.Y > Grid.cellsPerRow || Position.X < 0 || Position.X > Grid.cellsPerRow)
            {

            }
        }

        private List<Direction> GetPossibleDirections()
        {
            List<Direction> availableDirections = new List<Direction>
            {
                Direction.Up,
                Direction.Right,
                Direction.Down,
                Direction.Left
            };

            if (Position.Y == 0 || !Grid.IsCellEmpty((int)Position.Y - 1, (int)Position.X))
            {
                availableDirections.Remove(Direction.Up);
            }
            if (Position.X == Grid.cellsPerRow - 1 || !Grid.IsCellEmpty((int)Position.Y, (int)Position.X + 1))
            {
                availableDirections.Remove(Direction.Right);
            }
            if (Position.Y == Grid.cellsPerRow - 1 || !Grid.IsCellEmpty((int)Position.Y + 1, (int)Position.X))
            {
                availableDirections.Remove(Direction.Down);
            }
            if (Position.X == 0 || !Grid.IsCellEmpty((int)Position.Y, (int)Position.X - 1))
            {
                availableDirections.Remove(Direction.Left);
            }

            return availableDirections;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Texture2D currentTexture;

            if (Adult || Evil)
            {
                currentTexture = Grid.adultTexture;
            }
            else
            {
                currentTexture = Grid.youngTexture;
            }

            Color currentColor;

            if (Evil)
            {
                currentColor = Color.Black;
            }
            else if (Sex ==  Sex.Male)
            {
                currentColor = maleColor;
            }
            else
            {
                currentColor = femaleColor;
            }

            spriteBatch.Draw(currentTexture, Grid.GetCell((int)Position.Y, (int)Position.X), currentColor);
        }

    }
}
