using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceImpact
{
    class Wallpaper : SpaceComponents
    {
        private Wallpaper() { }

        //That backgrond wallpaper has to be of the same dimensions as the game window
        public Wallpaper(Texture2D texture) : base(texture, GameHeight, GameWidth)
        {
            //It's just an image after all
        }
    }
}
