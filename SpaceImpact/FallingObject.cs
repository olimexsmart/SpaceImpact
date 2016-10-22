using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceImpact
{
    public class FallingObject : SpaceComponents
    {
        private Point Position;
        //These proprieties are reference to the middle point of the spaceship, while the
        //Point object holds the top-left corner of the rectangle in order to draw it
        public int PosX { get { return Position.X + (Width / 2); } private set { Position.X = value - (Width / 2); } }
        public int PosY { get { return Position.Y + (Height / 2); } private set { Position.Y = value - (Height / 2); } }
        private float speed = GameSpeed;
        protected float Speed
        {
            get { return speed; }
            set
            {
                if (value < 0) throw new ArithmeticException("Negative speed inserted.");
                else speed = value * GameSpeed;
            }
        }

        private int heightMax;
        protected int HeightMax
        {
            get { return heightMax; }
            set
            {
                if (value < 0) throw new ArithmeticException("Negative dimension");
                else heightMax = value;
            }
        }     

        static private Random caos = new Random((int)DateTime.Now.Ticks); //This object does what it wants
    
        protected FallingObject() { }

        protected FallingObject(SpriteBatch spriteBatch, Texture2D texture) : base(spriteBatch, texture, 0, 0)
        {
            
        }

        public override void Draw()
        {
            where.X = Position.X;
            where.Y = Position.Y;
            where.Height = Height;
            where.Width = Width;
            base.Draw();
        }

        public virtual void Move(long dT)
        {   //It travels in a straight line
            PosY += (int)(dT * Speed);

            //Boundaries check, reload with a new one
            if (PosY > GameHeight + Height)
                CreateNew();
        }

        protected void CreateNew()
        {            
            //Dimensions not specified, but the original texture ration will be conserved
            Height = caos.Next(HeightMax / 10, HeightMax);
            float ratio = (float)Texture.Height / (float)Texture.Width;
            Width = (int)(Height / ratio);
            //Speed
            Speed = caos.Next(10, 20);
            //And position
            PosX = caos.Next(0, GameWidth);
            PosY = -Height - caos.Next(0, 500); //It needs to start outside the window
        }
    }
}
