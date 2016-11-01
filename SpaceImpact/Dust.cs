using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace SpaceImpact
{
    class Dust : FallingObject
    {    
        private Dust() { }

        public Dust(Texture2D texture) : base(texture, WindowHeight)
        {                               
        }

        protected override void CreateNew()
        {
            //Dimensions not specified, but the original texture ration will be conserved
            Height = caos.Next(HeightMax / 4, HeightMax);
            float ratio = (float)Texture.Height / (float)Texture.Width;
            Width = (int)(Height / ratio);
            //Speed
            Speed = caos.Next(10, 20);
            //And position
            PosX = caos.Next(0, WindowWidth);
            PosY = -Height - caos.Next(0, 400); //It needs to start outside the window
        }
    }
}
