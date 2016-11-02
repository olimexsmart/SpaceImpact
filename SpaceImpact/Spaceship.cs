using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceImpact
{
    public class Spaceship : SpaceComponents, ICollidable
    {
        private bool isCollided = false;
        private Explosion explosion;

        protected Spaceship() { }

        public Spaceship(Texture2D texture) : base(texture, WindowHeight / 10, WindowHeight / 10)
        {   //Put it right in the middle
            PosX = WindowWidth / 2;
            PosY = WindowHeight / 2;
            explosion = new Explosion(Height, Width);
        }


        public override void Draw()
        {
            if (isCollided)
                explosion.DrawExplosion(PosX, PosY);
            else
                base.Draw(); //Draw it, no hassle            
        }

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

        public Rectangle GetRectangle()
        {
            return new Rectangle(PosX - (Width / 2), PosY + (Height / 2), Width, Height);
        }

        public void SetCollided(bool isItTrue)
        {
            isCollided = isItTrue;
        }

        public bool IsAlreadyCollided()
        {
            return isCollided;
        }

    }
}
