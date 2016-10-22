using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceImpact
{
    class Asteroid : FallingObject
    {
        public Point Position;
        private Texture2D[] asteroid;
        //These two will help deciding when update the texture
        int rotation;
        long prevPos;

        protected Asteroid() { }

        //Start with the first image
        public Asteroid(SpriteBatch spriteBatch, Texture2D[] texture) : base(spriteBatch, texture[0])
        {
            HeightMax = GameWidth / 6;
            rotation = 0;
            asteroid = texture;
            CreateNew();
            prevPos = PosY;
        }

        //HEREEEEEEEEEEEEEEEEEEEEEEEEEEEEE
        public override void Move(long dT)
        {
            base.Move(dT);
            prevPos += dT;
            if (prevPos > 30) //33 FPS
            {
                prevPos = 0;              
                rotation++;
                rotation %= asteroid.Length; //Not overflow
                Texture = asteroid[rotation]; //Update the frame
            }
        }

        //public override void Draw()
        //{            
        //    base.Draw();
        //}
    }
}
