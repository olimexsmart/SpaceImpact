using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceImpact
{
    public class Bullet : SpaceComponents, ICollidable
    {        
        private Bullet() { }

        public bool OutOfBounds { get; private set; }

        private float SenRot, CosRot;
        private bool isCollided = false;
        private Explosion explosion;  

        public Bullet(Texture2D texture, int originX, int originY) : base(texture, WindowHeight / 20, WindowHeight / 20)
        {
            Speed = 30;            
            PosX = originX;
            PosY = originY;
            //This is fixed            
            rotation = (float)Math.Atan2(MousePointer.MousePositionX - PosX, PosY - MousePointer.MousePositionY);
            SenRot = (float)Math.Sin(rotation);
            CosRot = (float)Math.Cos(rotation);
            OutOfBounds = false;
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
            //New position
            PosX += (int)(SenRot * Speed * dT);
            PosY -= (int)(CosRot * Speed * dT);

            //Check if out of screen, an event here may be better
            if (PosX < 0 | PosX > WindowWidth) OutOfBounds = true;
            if (PosY < 0 | PosY > WindowHeight) OutOfBounds = true;
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
