using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


/*
    What should have all objects that can collide with each others?
*/
namespace SpaceImpact
{
    public interface ICollidable
    {
        //Start process of "self-destructing" of the object
        //void DetectCollision(ICollidable component);
        Rectangle GetRectangle(); //It's necessary to retreive the space where the object lives in
        void SetCollided(bool isItTrue);
        bool IsAlreadyCollided();
    }
}
