using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceImpact
{  
    public class CollisionManager
    {
        private List<ICollidable> collidables;
        private List<BulletManager> bullets;

        public CollisionManager()
        {
            collidables = new List<ICollidable> { };
            bullets = new List<BulletManager> { };
        }

        public void AddCollidable(ICollidable collidable)
        {
            collidables.Add(collidable);
        }

        public void AddCollidableCollection(IEnumerable<ICollidable> collidables)
        {
            this.collidables.AddRange(collidables);
        }

        public void AddBulletManager(BulletManager bm)
        {
            bullets.Add(bm);
        }

        public void DetectCollisions()
        {
            Rectangle firstObject;
            Rectangle secondObject;
            List<ICollidable> allcollidables = new List<ICollidable>(collidables);
            foreach (BulletManager bm in bullets)
            {
                allcollidables.AddRange(bm.flyingBullets);
            }
            //These loops may be reduced by half along with simmetry checks
            foreach (ICollidable ic1 in allcollidables)
            {
                firstObject = ic1.GetRectangle();

                foreach (ICollidable ic2 in allcollidables)
                {
                    secondObject = ic2.GetRectangle();
                    if ((ic1 is Asteroid && ic2 is Asteroid) || (ic1 is Spaceship && ic2 is Bullet) || (ic1 is Bullet && ic2 is Spaceship))
                        continue;
                    //Don't impact with yourself and don't impact if already impacted
                    if (ic1 != ic2 && firstObject.Intersects(secondObject) && !ic1.IsAlreadyCollided() && !ic2.IsAlreadyCollided()) 
                    {
                        ic1.SetCollided(true);
                        ic2.SetCollided(true);
                        //Stop firing from the spacecraft                        
                        if (ic1 is Spaceship || ic2 is Spaceship)
                            foreach (BulletManager bm in bullets)
                            {
                                if (bm.spacecraft == ic1 || bm.spacecraft == ic2)
                                    bm.FireEnable = false;
                            }
                    }
                }
            }
        }
    }
}
