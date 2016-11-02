using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceImpact
{
    public class Explosion
    {
        public static Texture2D[] frames;
        public static SpriteBatch spriteBatch;
        private int frameNumber = 0;
        int height, width;

        private Explosion() { }

        public Explosion(int height, int width)
        {
            this.height = height;
            this.width = width;
        }

        public void DrawExplosion(int posx, int posy)
        {
            if(frameNumber < frames.Length) //Whene the explosion is done do nothing
            {
                spriteBatch.Draw(frames[frameNumber], new Rectangle(posx - (width / 2), posy - (height / 2), width, height), Color.Wheat);
                frameNumber++;
            }
        }
    }
}
