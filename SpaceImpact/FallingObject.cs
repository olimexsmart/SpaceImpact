using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceImpact
{
    public abstract class FallingObject : SpaceComponents
    {
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
        
        //Needed to provide some game behaviour
        static protected Random caos = new Random((int)DateTime.Now.Ticks); 
    
        protected FallingObject() { }

        protected FallingObject(Texture2D texture, int maxHeight) : base(texture, 0, 0)
        {
            CreateNew();
        }

        //public override void Draw()
        //{
        //    base.Draw();
        //}

        public override void Move(long dT)
        {   //It travels in a straight line
            PosY += (int)(dT * Speed);

            //Boundaries check, reload with a new one
            if (PosY > WindowHeight + (Height / 2))
                CreateNew();
        }

        protected abstract void CreateNew();
        //{            
        //    //Dimensions not specified, but the original texture ration will be conserved
        //    Height = caos.Next(HeightMax / 10, HeightMax);
        //    float ratio = (float)Texture.Height / (float)Texture.Width;
        //    Width = (int)(Height / ratio);
        //    //Speed
        //    Speed = caos.Next(10, 20);
        //    //And position
        //    PosX = caos.Next(0, WindowWidth);
        //    PosY = -Height - caos.Next(0, 500); //It needs to start outside the window
        //}
    }
}
