using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceImpact
{
    public class BulletManager
    {
        private Spaceship spacecraft;
        private List<Bullet> flyingBullets;
        Texture2D bulletTexture;
        private long timeSinceLastShot = 0;
        private int fireRate = 500; //Two at sec default
        public int FireRate
        {
            get { return fireRate; }
            set
            {
                if (value < 0) throw new ArithmeticException("Negative fire ratio");
                else fireRate = value;
            }
        }

        private BulletManager() { }

        public BulletManager(Spaceship spacecraft, Texture2D bulletTexture)
        {
            this.spacecraft = spacecraft;
            this.bulletTexture = bulletTexture;
            flyingBullets = new List<Bullet> { };
        }

        public void MoveBullets(long dT)
        {
            timeSinceLastShot += dT;
            if (timeSinceLastShot > fireRate) //Crate a new bullet
            {
                flyingBullets.Add(new Bullet(bulletTexture, spacecraft.PosX, spacecraft.PosY));
                timeSinceLastShot = 0;
            }
            //Removing out-of-screen bullets
            flyingBullets.RemoveAll(x => x.OutOfBounds); //MAMMA MIAAAAAAA SEE

            foreach (Bullet b in flyingBullets)
                b.Move(dT);
        }

        public void DrawBullets()
        {
            foreach (Bullet b in flyingBullets)
                b.Draw();
        }
    }
}
