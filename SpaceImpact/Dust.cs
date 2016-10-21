using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace SpaceImpact
{
    class Dust : FallingObject
    {    
        private Dust() { }

        public Dust(SpriteBatch spriteBatch, Texture2D texture) : base(spriteBatch, texture)
        {
            CreateNew();
        }                
    }
}
