using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlpacasWithBonnets
{
    // Base class for any object to be made in game 
    // Author: Melissa Young, Zoe McHenry

    abstract public class GameObject
    {
        // Rectangle for objects
        private Rectangle objectSquare;
        // Postition
        private float x;
        private float y;

        public Color MyColor
        {
            get;
            set;
        }

        // Constructor
        public GameObject(int x, int y, int width, int height)
        {
            objectSquare = new Rectangle(x, y, width, height);
            MyColor = Color.White;
        }

        // Properties for the rectangle and its x and y value
        public Rectangle ObjectSquare
        {
            get { return objectSquare; }
            set { objectSquare = value; }
        }
        public float ObjectPosX
        {
            get { return objectSquare.X; }
            set { objectSquare.X = (int)(x = value); }
        }
        public float ObjectPosY
        {
            get { return objectSquare.Y; }
            set { objectSquare.Y = (int)(y = value); }
        }

        // Base draw method for the objects 
        public virtual void Draw(SpriteBatch objectBatch, Texture2D objectPic)
        {
            objectBatch.Draw(objectPic, ObjectSquare, MyColor);
        }
    }
}
