﻿using System;
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
    public class Dirt : Tile
    {
        //Constructor
        public Dirt(Vector2 tileLocation, ContentManager contentRef):base(tileLocation, contentRef)
        {
            IsPassable = false;
            TileImage = contentRef.Load<Texture2D>("dirt");
        }
    }
}
