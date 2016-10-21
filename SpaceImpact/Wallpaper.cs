using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceImpact
{
    class Wallpaper : SpaceComponents
    {
        private Wallpaper() { }

        public Wallpaper(SpriteBatch spriteBatch, Texture2D texture, int heigh, int width) : base(spriteBatch, texture, heigh, width)
        {
            
        }
    }
}
