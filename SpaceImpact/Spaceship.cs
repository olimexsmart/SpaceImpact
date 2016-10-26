using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceImpact
{
    public class Spaceship : SpaceComponents, ICollidable
    {
        private Point Position;        
        //These proprietiesare reference to the middle point of the spaceship, while the
        //Point object holds the top-left corner of the rectangle in order to draw it
        public int PosX { get { return Position.X; } protected set { Position.X = value; } }
        public int PosY { get { return Position.Y; } protected set { Position.Y = value; } }
        private float speed = GameSpeed;
        public float Speed
        {
            get { return speed; }
            set
            {
                if (value < 0) throw new ArithmeticException("Negative speed inerted.");
                else speed = value * GameSpeed;
            }
        }

        protected float rotation;
        //Explosion things
        public static Texture2D[] Explosion;
        protected bool isExploding = false;        
        private int explodingPhase = 0;
        private uint explosionTime = 0;

        protected Spaceship() { }

        public Spaceship(Texture2D texture) : base(texture, GameHeight / 10, GameHeight / 10)
        {
            PosX = GameWidth / 2;
            PosY = GameHeight / 2;
            Speed = 20;
        }


        public override void Draw()
        {
            if (!isExploding)
            {
                where.X = PosX;
                where.Y = PosY;
                where.Height = Height;
                where.Width = Width;

                SpriteBatch.Draw(Texture, null, where, null, new Vector2(Texture.Bounds.Center.X, Texture.Bounds.Center.Y), rotation); //All-argument overload with default values
            }
            else           
                base.Draw(); //Draw it, no hassle            
        }

        public virtual void Move(long dT)
        {
            if (!isExploding)
            {
                KeyboardState ks = Keyboard.GetState();
                Point mouse = Mouse.GetState().Position;

                //Moving UP
                if (ks.IsKeyDown(Keys.W) && ks.IsKeyUp(Keys.S))
                    PosY -= (int)(dT * Speed);
                //Moving DOWN
                if (ks.IsKeyUp(Keys.W) && ks.IsKeyDown(Keys.S))
                    PosY += (int)(dT * Speed);

                //Moving RIGHT
                if (ks.IsKeyDown(Keys.D) && ks.IsKeyUp(Keys.A))
                    PosX += (int)(dT * Speed);

                //Moving LEFT
                if (ks.IsKeyUp(Keys.D) && ks.IsKeyDown(Keys.A))
                    PosX -= (int)(dT * Speed);

                //Don't fly too far
                //Y boundaries
                PosY = Math.Min(PosY, GameHeight);
                PosY = Math.Max(PosY, 0);
                //X boundaries
                PosX = Math.Min(PosX, GameWidth);
                PosX = Math.Max(PosX, 0);


                //Keep the spaceship pointed towards the mouse
                rotation = (float)Math.Atan2(mouse.X - PosX, PosY - mouse.Y);
            }
            else
            {
                //Explosion management
                Texture = Explosion[explodingPhase]; //Apply current frame
                explosionTime += (uint)dT;
                if (explosionTime > 50)
                {
                    explodingPhase++;
                    //At the end keep it on the last frame
                    explodingPhase = Math.Min(explodingPhase, Explosion.Length - 1);
                }                                
            }
        }

        public void DetectCollision(ICollidable component)
        {
            if (!isExploding & !component.IsExploded()) //Once exploded become harmless
            {
                isExploding = where.Intersects(component.GetRectangle());
                component.JustCollided(isExploding);
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
