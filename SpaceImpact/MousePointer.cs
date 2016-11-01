using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceImpact
{
    public class MousePointer
    {
        private Texture2D mouseTexture;
        private SpriteBatch spriteBatch;
        //The mouse is one and only
        private static Point mousePosition;
        public static int MousePositionX { get { return mousePosition.X; } private set { } }
        public static int MousePositionY { get { return mousePosition.Y; } private set { } }

        private int windowHeight;
        private int windowWidth;

        private int size;

        private MousePointer() { }

        public MousePointer(SpriteBatch spriteBatch, Texture2D mouseTexture, int windowHeight, int windowWidth)
        {
            this.mouseTexture = mouseTexture;
            this.spriteBatch = spriteBatch;
            this.windowHeight = windowHeight;
            this.windowWidth = windowWidth;
            size = windowHeight / 10;
            mousePosition = Mouse.GetState().Position;
        }

        public void MoveMouse()
        {
            mousePosition = Mouse.GetState().Position;

            //Block the position at edge of the window
            mousePosition.X = Math.Min(windowWidth, mousePosition.X);
            mousePosition.Y = Math.Min(windowHeight, mousePosition.Y);
            mousePosition.X = Math.Max(0, mousePosition.X);
            mousePosition.Y = Math.Max(0, mousePosition.Y);
        }

        public void Draw()
        {
            spriteBatch.Draw(mouseTexture, new Rectangle(mousePosition.X - size, mousePosition.Y - size, size, size), Color.White);
        }
    }
}
