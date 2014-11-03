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
    //Zoe McHenry
    public class Sky : Tile
    {
        //Constructor
        public Sky(Vector2 tileLocation, ContentManager contentRef):base(tileLocation, contentRef)
        {
            IsPassable = true;
            TileImage = contentRef.Load<Texture2D>("sky");
        }
    }
}
