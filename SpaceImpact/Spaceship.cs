using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceImpact
{
    public class Spaceship : SpaceComponents
    {
        protected Point Position;        
        //These proprietiesare reference to the middle point of the spaceship, while the
        //Point object holds the top-left corner of the rectangle in order to draw it
        public int PosX { get { return Position.X + (Width / 2); } protected set { Position.X = value - (Width / 2); } }
        public int PosY { get { return Position.Y + (Height / 2); } protected set { Position.Y = value - (Height / 2); } }
        private float speed = GameSpeed;
        public float Speed
        {
            get { return speed; }
            set
            {
                if (value < 0) throw new ArithmeticException("Negative speed inerted.");
                else speed = value * GameSpeed;
            }
        }

        protected float rotation;


        protected Spaceship() { }

        public Spaceship(Texture2D texture) : base(texture, GameHeight / 10, GameHeight / 10)
        {
            Position = new Point(GameHeight / 2, GameWidth / 2);
            Speed = 20;
        }


        public override void Draw()
        {
            where.X = Position.X;
            where.Y = Position.Y;
            where.Height = Height;
            where.Width = Width;

            SpriteBatch.Draw(Texture, null , where, null, new Vector2(Texture.Bounds.Center.X, Texture.Bounds.Center.Y), rotation); //All-argument overload with default values
            
            //base.Draw(); We need rotation
        }

        public virtual void Move(long dT)
        {
            KeyboardState ks = Keyboard.GetState();
            Point mouse = Mouse.GetState().Position;            
            
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
            

            //Keep the spaceship pointed towards the mouse
            rotation = (float) Math.Atan2(mouse.X - PosX, PosY - mouse.Y);

        }
    }
}
