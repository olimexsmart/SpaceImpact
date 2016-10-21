using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceImpact
{
    public class SpaceComponents
    {
        protected Texture2D Texture;
        protected SpriteBatch SpriteBatch;
        protected Rectangle where;
        //Dimensions
        private int height, width;
        public int Height
        {
            get { return height; }
            set
            {
                if (value < 0) throw new ArithmeticException("No negative dimensions.");
                else
                    height = value;
            }
        }
        public int Width
        {
            get { return width; }
            set
            {
                if (value < 0) throw new ArithmeticException("No negative dimensions.");
                else
                    width = value;
            }
        }

        //Every components needs to know how much the screen is
        //VGA deafult value
        private static int maxX = 640; 
        private static int maxY = 480;
        public static int GameWidth
        {
            get { return maxX; }
            set
            {
                if (value < 0) throw new ArithmeticException("Negative dimension");
                else maxX = value;
            }
        }
        public static int GameHeight
        {
            get { return maxY; }
            set
            {
                if (value < 0) throw new ArithmeticException("Negative dimension");
                else maxY = value;
            }
        }

        private static float gameSpeed = 0.02f; //Default
        public static float GameSpeed
        {
            get { return gameSpeed; }
            set
            {
                if (value < 0) throw new ArithmeticException("No negative speed.");
                else
                    gameSpeed = value;
            }
        }


        protected SpaceComponents() { }

        //The texture can be only loaded, 
        //The spriteBatch will be the same object for all components
        //The position will be updated so it need to be accessible
        protected SpaceComponents(SpriteBatch spriteBatch, Texture2D texture, int height, int width)
        {
            Texture = texture;
            SpriteBatch = spriteBatch;            
            Width = width;
            Height = height;
            where = new Rectangle(0, 0, Width, Height);
        }

        //Every child object gives a personal interpretation of the rectangle, depending on their position
        virtual public void Draw()
        {
            SpriteBatch.Draw(Texture, where, Color.White);
        }
    }
}
