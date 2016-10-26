using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceImpact
{
    public class Bullet : Spaceship
    {        
        private Bullet() { }

        public bool OutOfBounds { get; private set; }

        private float SenRot, CosRot;        

        public Bullet(Texture2D texture, int originX, int originY) : base(texture)
        {
            Speed = 30;            
            Height = GameHeight / 20;
            Width = Height;
            PosX = originX;
            PosY = originY;
            //This is fixed
            Point mouse = Mouse.GetState().Position;
            rotation = (float)Math.Atan2(mouse.X - PosX, PosY - mouse.Y);
            SenRot = (float)Math.Sin(rotation);
            CosRot = (float)Math.Cos(rotation);
            OutOfBounds = false;
        }

        public override void Move(long dT)
        {
            //New position
            PosX += (int)(SenRot * Speed * dT);
            PosY -= (int)(CosRot * Speed * dT);

            //Check if out of screen, an event here may be better
            if (PosX < 0 | PosX > GameWidth) OutOfBounds = true;
            if (PosY < 0 | PosY > GameHeight) OutOfBounds = true;
        }

        public override void Draw()
        {
            //Collapse it so it can't be seen
            where.Height = 0;
            where.Width = 0;
            base.Draw();
        }
    }
}
