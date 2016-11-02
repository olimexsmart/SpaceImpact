using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceImpact
{
    public class Asteroid : FallingObject, ICollidable
    {
        private Texture2D[] frames; //Array of frames giving the illusion of 3D
        //These two will help deciding when update the texture      
        private long prevFrameTime = 0;
        private byte frameNumber = 0;
        private bool isCollided = false;
        private Explosion explosion;

        protected Asteroid() { }

        //Start with the first image
        public Asteroid(Texture2D[] texture) : base(texture[0], WindowHeight / 6)
        {                     
            frames = texture;                        
            CreateNew();
        }

        public override void Draw()
        {
            if (isCollided)
                explosion.DrawExplosion(PosX, PosY);
            else
                base.Draw(); //Draw it, no hassle            
        }

        public override void Move(long dT)
        {
            base.Move(dT);
            //Reset explosion after a new was created            
            prevFrameTime += (uint)dT;
            if (prevFrameTime > 33) //33 FPS
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
            Height = caos.Next(HeightMax / 6, HeightMax);
            float ratio = (float)Texture.Height / (float)Texture.Width;
            Width = (int)(Height / ratio);
            //Speed
            Speed = caos.Next(10, 25);
            //And position
            PosX = caos.Next(0, WindowWidth);
            PosY = -Height - caos.Next(0, 400); //It needs to start outside the window
            frameNumber = (byte) caos.Next(0, frames.Length);
            explosion = new Explosion(Height, Width);
            isCollided = false;
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle(PosX - (Width / 2), PosY + (Height / 2), Width, Height);
        }

        public void SetCollided(bool isItTrue)
        {
            isCollided = isItTrue;
        }

        public bool IsAlreadyCollided()
        {
            return isCollided;
        }
    }
}
