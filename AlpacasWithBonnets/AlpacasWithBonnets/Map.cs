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
using System.IO;

namespace AlpacasWithBonnets
{
    //Zoe McHenry
    public class Map
    {
        //TODO: I need to prepare the level
        private Tile[,] level;
        private int width, height;
        public const int SIZE = 50;

        //Constructor
        public Map(int width, int height)
        {
            level = new Tile[width, height];
            this.width = width;
            this.height = height;
        }

        public Tile[,] NewMap
        {
            get { return level; }
        }

        //Methods
        public void LoadMap(string levelFilePath, ContentManager contentRef)
        {
            StreamReader levelInput = new StreamReader(levelFilePath);
            List<String> lines = new List<string>();
            //TODO: pick map size
            char[,] inputToTile = new char[width, height];

            //In case there's a problem with the file and inevitably closes StreamReader no matter what
            try
            {
                string tempLine;
                while ((tempLine = levelInput.ReadLine()) != null)
                {
                    lines.Add(tempLine);
                }
            }
            catch { }
            finally
            {
                levelInput.Close();
            }

            //Loops
            //Determine what type of Tile each member of the array is based on the .txt file
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    inputToTile[i, j] = lines[j][i];
                }
            }

            //Converts text file into map
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (inputToTile[i, j] == 'S') //Sky
                    {
                        level[i, j] = new Sky(new Vector2(i * SIZE, j * SIZE), contentRef);
                    }
                    else if (inputToTile[i, j] == 'P') //Platform
                    {
                        level[i, j] = new Platform(new Vector2(i * SIZE, j * SIZE), contentRef);
                    }
                    else if (inputToTile[i, j] == 'G') //Grass
                    {
                        level[i, j] = new Grass(new Vector2(i * SIZE, j * SIZE), contentRef);
                    }
                    else if (inputToTile[i, j] == 'D') //Dirt
                    {
                        level[i,j] = new Dirt(new Vector2(i* SIZE, j*SIZE), contentRef);
                    }
                    else if (inputToTile[i, j] == 'R') //Rock
                    {
                        level[i,j] = new Rock(new Vector2(i* SIZE, j*SIZE), contentRef);
                    }
                    else if (inputToTile[i, j] == 'Z') //Goal ('G' was already taken)
                    {
                        level[i, j] = new Goal(new Vector2(i * SIZE, j * SIZE), contentRef);
                    }
                    else
                    {
                        //TODO: throw custom exception
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 playerPos)
        {
            Rectangle rect = new Rectangle(0, 0, Tile.SIZE, Tile.SIZE);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    rect.X = i * Tile.SIZE - (int)playerPos.X;
                    rect.Y = j * Tile.SIZE - (int)playerPos.Y;
                    level[i, j].TileRectangle = rect;

                    level[i, j].Draw(spriteBatch);
                }
            }
        }
    }
}
