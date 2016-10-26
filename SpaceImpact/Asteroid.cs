using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceImpact
{
    class Asteroid : FallingObject, ICollidable
    {
        private static Texture2D[] asteroid;
        //These two will help deciding when update the texture
        byte rotation;
        uint prevFrame;
        //Explosion things
        public static Texture2D[] Explosion;
        private bool isExploding = false;
        //private int explodingPhase = 0;
        //private uint explosionTime = 0;

        protected Asteroid() { }

        //Start with the first image
        public Asteroid(Texture2D[] texture) : base(texture[0])
        {
            HeightMax = GameWidth / 6;
            rotation = 0;
            asteroid = texture;
            prevFrame = 0;
            CreateNew();            
        }

        //HEREEEEEEEEEEEEEEEEEEEEEEEEEEEEE
        public override void Move(long dT)
        {
            base.Move(dT);
            //Reset explosion after a new was created
            if (PosY < 0) isExploding = false;
            prevFrame += (uint)dT;
            if (prevFrame > 30) //33 FPS
            {
                if (isExploding)
                {
                    Texture = Explosion[rotation];
                    rotation++;
                    rotation = (byte)Math.Min(rotation, Explosion.Length - 1);
                    prevFrame = 0;
                }
                else
                {
                    prevFrame = 0;
                    rotation++;
                    rotation %= (byte)asteroid.Length; //Not overflow but cycle
                    Texture = asteroid[rotation]; //Update the frame
                }
            }
        }

        public void DetectCollision(ICollidable component)
        {
            if (!isExploding & !component.IsExploded()) //If it is already exploding it's harmless
            {
                isExploding = where.Intersects(component.GetRectangle());
                component.JustCollided(isExploding);
                rotation = 0;
                prevFrame = 0;
            }
        }

        public Rectangle GetRectangle()
        {
            return where;
        }

        public void JustCollided(bool isItTrue)
        {
            if (!isExploding && isItTrue) //Once it exploded, no way back
                isExploding = true;
        }

        public bool IsExploded()
        {
            return isExploding;
        }
    }
}
