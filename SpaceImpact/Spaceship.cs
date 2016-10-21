using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceImpact
{
    class Spaceship : SpaceComponents
    {
        private Point Position;        
        //These proprieties are reference to the middle point of the spaceship, while the
        //Point object holds the top-left corner of the rectangle in order to draw it
        public int PosX { get { return Position.X + (Width / 2); } private set { Position.X = value - (Width / 2); } }
        public int PosY { get { return Position.Y + (Height / 2); } private set { Position.Y = value - (Height / 2); } }
        private static float speed = GameSpeed;
        public static float Speed
        {
            get { return speed; }
            set
            {
                if (value < 0) throw new ArithmeticException("Negative speed inerted.");
                else speed = value * GameSpeed;
            }
        }


        private Spaceship() { }

        public Spaceship(SpriteBatch spriteBatch, Texture2D texture, int height, int width, int positionX, int positionY) : base(spriteBatch, texture, height, width)
        {
            Position = new Point(positionX, positionY);
            Speed = 20;
        }


        public override void Draw()
        {
            where.X = Position.X;
            where.Y = Position.Y;
            where.Height = Height;
            where.Width = Width;
            base.Draw();
        }

        public void Move(long dT)
        {
            KeyboardState ks = Keyboard.GetState();
            
            //Moving UP
            if (ks.IsKeyDown(Keys.W) && ks.IsKeyUp(Keys.S))
                PosY -= (int) (dT * Speed);
            //Moving DOWN
            if(ks.IsKeyUp(Keys.W) && ks.IsKeyDown(Keys.S))
                PosY += (int)(dT * Speed);

            //Moving RIGHT
            if (ks.IsKeyDown(Keys.D) && ks.IsKeyUp(Keys.A))
                PosX += (int)(dT * Speed);

            //Moving LEFT
            if (ks.IsKeyUp(Keys.D) && ks.IsKeyDown(Keys.A))
                PosX -= (int)(dT * Speed);

            //Don't fly too far
            //Y boundaries
            PosY = Math.Min(PosY, GameHeight);
            PosY = Math.Max(PosY, 0);
            //X boundaries
            PosX = Math.Min(PosX, GameWidth);
            PosX = Math.Max(PosX, 0);
        }
    }
}
