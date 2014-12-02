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

namespace AlpacasWithBonnets
{
    // Tile class
    // Zoe McHenry
    public abstract class Tile
    {
        //Fields
        public const int SIZE = 50;
        private bool isPassable;
        private Vector2 tileLocation;
        private Texture2D tileImage;
        private Rectangle tileRectangle;

        // Properties
        public Vector2 TileLocation
        {
            get { return this.TileLocation; }
            set { tileLocation = value; }
        }
        public Texture2D TileImage
        {
            get { return this.tileImage; }
            set { this.tileImage = value; }
        }
        public bool IsPassable
        {
            get { return this.isPassable; }
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
        }
        public Tile(Vector2 tileLocation, ContentManager contentRef)
        {
            this.TileLocation = tileLocation;
            tileRectangle = new Rectangle((int)tileLocation.X, (int)tileLocation.Y, SIZE, SIZE);
        }

        // Method
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tileImage, tileRectangle, Color.White);
        }
    }
}
