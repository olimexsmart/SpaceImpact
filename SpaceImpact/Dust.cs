using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace SpaceImpact
{
    class Dust : SpaceComponents
    {
        private Point Position;
        public int PosX { get { return Position.X; } private set { Position.X = value; } }
        public int PosY { get { return Position.Y; } private set { Position.Y = value; } }
        private float speed = 0.2f;
        public float Speed
        {
            get { return speed; }
            set
            {
                if (value < 0) throw new ArithmeticException("Negative speed inserted.");
                else speed = value;
            }
        }

        private Dust() { }

        public Dust(SpriteBatch sprteBatch, Texture2D texture, int height, int width, float speed, int maxX, int maxY) : base(sprteBatch, texture, height, width)
        {
            Random caos = new Random();
            Speed = speed;
            PosX = caos.Next(0, maxX);
            PosY = 0;
        }
    }
}
