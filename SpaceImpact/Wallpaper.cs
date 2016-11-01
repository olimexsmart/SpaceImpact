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
        public Wallpaper(Texture2D texture) : base(texture, WindowHeight, WindowWidth)
        {
            //It's just an image after all
            //Putting it centered and big as the window
            PosX = WindowWidth / 2;
            PosY = WindowHeight / 2;
            Height = WindowHeight;
            Width = WindowWidth;
        }

        public override void Move(long dT)
        {
            //This thing doesn't move at all
        }
    }
}
