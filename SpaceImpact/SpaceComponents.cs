using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/*
    This class holds all the basic stuff needed to a drawable object to move and draw itself
*/

namespace SpaceImpact
{
    public abstract class SpaceComponents : IDrawable
    {
        private Point position;
        protected float rotation = 0;
        //These proprieties are reference to the middle point of the spaceship, while the
        //Point object holds the top-left corner of the rectangle in order to draw it
        public int PosX { get { return position.X; } protected set { position.X = value; } }
        public int PosY { get { return position.Y; } protected set { position.Y = value; } }
        //Static fields, objects common to all the childrens
        protected Texture2D Texture;
        public static SpriteBatch SpriteBatch;
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
        private static int windowWidth = 640; //Space of existence 
        private static int windowHeight = 480;
        public static int WindowWidth
        {
            get { return windowWidth; }
            set
            {
                if (value < 0) throw new ArithmeticException("Negative dimension");
                else windowWidth = value;
            }
        }
        public static int WindowHeight
        {
            get { return windowHeight; }
            set
            {
                if (value < 0) throw new ArithmeticException("Negative dimension");
                else windowHeight = value;
            }
        }
        //Dimensions of the object
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
        //TODO, event update of all speeds when GameSpeed changes    
        private float speed = 20 * GameSpeed; //Speed of this object, default
        public float Speed
        {
            get { return speed; }
            set
            {
                if (value < 0) throw new ArithmeticException("Negative speed inserted.");
                else speed = value * GameSpeed;
            }
        }
        //Some things are mandatory
        protected SpaceComponents() { }

        //The texture can be only loaded, 
        //The spriteBatch will be the same object for all components
        //The position will be updated so it need to be accessible
        protected SpaceComponents(Texture2D texture, int height, int width)
        {
            Texture = texture;
            Width = width;
            Height = height;
            position = new Point(0, 0);
        }

        //Every child object gives a personal interpretation of the method, depending on their position
        virtual public void Draw()
        {
            /*
                So basically this demostrates how specifying the origin poit, which has to be in the middle of the original texture, 
                the first two prametres of the rectangle contructor hold the central position of the texture
                No need of complicated offsets this way
            */
            SpriteBatch.Draw(Texture,
                                null,
                                new Rectangle(PosX, PosY, Width, Height),
                                null,
                                new Vector2(Texture.Bounds.Center.X, Texture.Bounds.Center.Y),
                                rotation); //All-argument overload with default values
        }

        abstract public void Move(long dT);
    }
}
