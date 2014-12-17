using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AlpacaWithBonnets
{
    class Collectible : GameObject
    {
        bool isActive;

        // Constructor
        public Collectible(int x, int y)
            : base(x, y, 50, 50)
        {
            isActive = true;
        }

        // Properties
        public bool ActiveCoin
        {
            get { return isActive; }
            set { isActive = value; }
        }

        // check if the coin has been run into yet
        public bool CheckCollision(Character mainCharater)
        {

            if (isActive == true)
            {
                if (mainCharater.ObjectSquare.Intersects(this.ObjectSquare))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        // Draw the coin if active
        public void DrawCoin(SpriteBatch spritebatch, Texture2D coinTexture)
        {
            if (isActive == true)
            {
                spritebatch.Draw(coinTexture, ObjectSquare, Color.White);
            }
        }
    }
}
