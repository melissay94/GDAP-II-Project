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

namespace AlpacaWithBonnets
{
    // Zoe McHenry
    class Map
    {
        private Tile[,] level;
        private int width, height;
        private int gameWidth;
        public const int SIZE = 50;

          //Constructor
        public Map(int width, int height)
        {
            level = new Tile[width, height];
            this.width = width;
            this.height = height;

            gameWidth = width * Tile.SIZE;
        }

        public Tile[,] NewMap
        {
            get { return level; }
        }

        public int GameWidth
        {
            get { return gameWidth; }
            set { gameWidth = value; }
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
            // Change so that there isn't a different class for each tile. MY
            try
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (inputToTile[i, j] == 'S') //Sky
                        {
                            level[i, j] = new Tile(new Vector2(i * SIZE, j * SIZE), contentRef, "sky");
                            level[i,j].IsPassable = true;
                        }
                        else if (inputToTile[i, j] == 'P') //Platform
                        {
                            level[i, j] = new Tile(new Vector2(i * SIZE, j * SIZE), contentRef, "platform");
                        }
                        else if (inputToTile[i, j] == 'G') //Grass
                        {
                            level[i, j] = new Tile(new Vector2(i * SIZE, j * SIZE), contentRef, "grass");
                        }
                        else if (inputToTile[i, j] == 'D') //Dirt
                        {
                            level[i, j] = new Tile(new Vector2(i * SIZE, j * SIZE), contentRef, "dirt");
                        }
                        else if (inputToTile[i, j] == 'R') //Rock
                        {
                            level[i, j] = new Tile(new Vector2(i * SIZE, j * SIZE), contentRef, "rock");
                        }
                        else if (inputToTile[i, j] == 'Z') //Goal ('G' was already taken)
                        {
                            level[i, j] = new Tile(new Vector2(i * SIZE, j * SIZE), contentRef, "goal");
                        }
                        else if (inputToTile[i, j] == 'B') //Sand, but B used for beach
                        {
                            level[i, j] = new Tile(new Vector2(i * SIZE, j * SIZE), contentRef, "sand");
                        }
                        else if (inputToTile[i, j] == 'W') //Water 
                        {
                            level[i, j] = new Tile(new Vector2(i * SIZE, j * SIZE), contentRef, "water");
                            level[i, j].IsPassable = true;
                        }
                        else if (inputToTile[i,j] == 'C') //Concrete 
                        {
                            level[i, j] = new Tile(new Vector2(i * SIZE, j * SIZE), contentRef, "concrete");
                        }
                        else if (inputToTile[i, j] == 'T') // Brick
                        {
                            level[i, j] = new Tile(new Vector2(i * SIZE, j * SIZE), contentRef, "brick");
                            level[i, j].IsPassable = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                
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
