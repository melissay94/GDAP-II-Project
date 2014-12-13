using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AlpacaWithBonnets
{
    class Tile
    {
        // Tile class
        // Zoe McHenry

            //Fields
            public const int SIZE = 50;
            private bool isPassable;
            private Vector2 tileLocation;
            private Texture2D tileImage;
            private Rectangle tileRectangle;

            // Properties
            public Vector2 TileLocation
            {
                get { return tileLocation; }
                set { tileLocation = value; }
            }
            public Texture2D TileImage
            {
                get { return tileImage; }
                set { this.tileImage = value; }
            }
            public bool IsPassable
            {
                get { return isPassable; }
                set { this.isPassable = value; }
            }
            public Rectangle TileRectangle
            {
                get { return tileRectangle; }
                set { this.tileRectangle = value; }
            }

            // Constructor
            public Tile(Vector2 tileLocation, Texture2D tileImage, bool isPassable)
            {
                this.tileLocation = tileLocation;
                this.tileImage = tileImage;
                this.isPassable = isPassable;
                IsPassable = false;
            }
            public Tile(Vector2 tileLocation, ContentManager contentRef, string type)
            {
                this.TileLocation = tileLocation;
                tileRectangle = new Rectangle((int)tileLocation.X, (int)tileLocation.Y, SIZE, SIZE);
                TileImage = contentRef.Load<Texture2D>(type);
            }

            // Method
            public virtual void Draw(SpriteBatch spriteBatch)
            {
                spriteBatch.Draw(tileImage, tileRectangle, Color.White);
            }
    }
}
