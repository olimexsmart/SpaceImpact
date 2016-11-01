using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceImpact
{
    public class Spaceship : SpaceComponents//, ICollidable
    {
        protected Spaceship() { }

        public Spaceship(Texture2D texture) : base(texture, WindowHeight / 10, WindowHeight / 10)
        {   //Put it right in the middle
            PosX = WindowWidth / 2;
            PosY = WindowHeight / 2;
        }


        //public override void Draw()
        //{
        //    base.Draw(); //Draw it, no hassle            
        //}

        public override void Move(long dT)
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
            PosY = Math.Min(PosY, WindowHeight);
            PosY = Math.Max(PosY, 0);
            //X boundaries
            PosX = Math.Min(PosX, WindowWidth);
            PosX = Math.Max(PosX, 0);


            //Keep the spaceship pointed towards the mouse
            rotation = (float)Math.Atan2(mouse.X - PosX, PosY - mouse.Y);
        }

        //public void DetectCollision(ICollidable component)
        //{
        //    if (!isExploding & !component.IsExploded()) //Once exploded become harmless
        //    {
        //        isExploding = where.Intersects(component.GetRectangle());
        //        component.JustCollided(isExploding);
        //    }
        //}

        //public Rectangle GetRectangle()
        //{
        //    return where;
        //}

        //public void JustCollided(bool isItTrue)
        //{
        //    if (!isExploding && isItTrue) //Once it exploded, no way back
        //        isExploding = true;
        //}

        //public bool IsExploded()
        //{
        //    return isExploding;
        //}
    }
}
