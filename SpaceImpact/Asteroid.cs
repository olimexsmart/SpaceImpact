using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceImpact
{
    public class Asteroid : FallingObject//, ICollidable
    {
        private static Texture2D[] frames; //Array of frames giving the illusion of 3D
        //These two will help deciding when update the texture      
        long prevFrameTime = 0;
        byte frameNumber = 0;

        protected Asteroid() { }

        //Start with the first image
        public Asteroid(Texture2D[] texture) : base(texture[0], WindowHeight / 10)
        {                     
            frames = texture;            
            //CreateNew(); Already called by the base class
        }
        
        public override void Move(long dT)
        {
            base.Move(dT);
            //Reset explosion after a new was created            
            prevFrameTime += (uint)dT;
            if (prevFrameTime > 30) //33 FPS
            {
                prevFrameTime = 0;
                frameNumber++;
                frameNumber %= (byte)frames.Length; //Not overflow but cycle
                Texture = frames[frameNumber]; //Update the frame
            }
        }

        protected override void CreateNew()
        {
            //Dimensions not specified, but the original texture ration will be conserved
            Height = caos.Next(HeightMax / 8, HeightMax);
            float ratio = (float)Texture.Height / (float)Texture.Width;
            Width = (int)(Height / ratio);
            //Speed
            Speed = caos.Next(10, 20);
            //And position
            PosX = caos.Next(0, WindowWidth);
            PosY = -Height - caos.Next(0, 400); //It needs to start outside the window
        }

        //public void DetectCollision(ICollidable component)
        //{
        //    if (!isExploding & !component.IsExploded()) //If it is already exploding it's harmless
        //    {
        //        isExploding = where.Intersects(component.GetRectangle());
        //        component.JustCollided(isExploding);
        //        rotation = 0;
        //        prevFrame = 0;
        //    }
        //}

        //public Rectangle GetRectangle()
        //{
        //    return where;
        //}

        //public void JustCollided(bool isItTrue)
        //{
        //    if (!isExploding && isItTrue) //Once it exploded, no way back
        //        isExploding = true;
        //}

        //public bool IsExploded()
        //{
        //    return isExploding;
        //}
    }
}
