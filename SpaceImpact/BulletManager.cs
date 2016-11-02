using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceImpact
{
    public class BulletManager
    {
        public Spaceship spacecraft;
        public List<Bullet> flyingBullets;
        private Texture2D bulletTexture;
        private long timeSinceLastShot = 0;
        private int fireRate = 250; //Two at sec default
        public int FireRate
        {
            get { return fireRate; }
            set
            {
                if (value < 0) throw new ArithmeticException("Negative fire ratio");
                else fireRate = value;
            }
        }
        public bool FireEnable = true;

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
            if (timeSinceLastShot > fireRate && FireEnable) //Crate a new bullet
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
