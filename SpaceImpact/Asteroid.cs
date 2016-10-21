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
        int prevPos;

        //Start with the first image
        private Asteroid(SpriteBatch spriteBatch, Texture2D[] texture) : base(spriteBatch, texture[0])
        {
            rotation = 0;
            CreateNew();
            prevPos = PosY;
        }

        //HEREEEEEEEEEEEEEEEEEEEEEEEEEEEEE
        public override void Move(long dT)
        {
            base.Move(dT);
            throw new NotImplementedException();
        }
    }
}
