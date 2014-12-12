using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace AlpacasWithBonnets
{
    // The alpaca type
    public enum AlpacaType
    {
        Warrior,
        Wizard,
        Dunce
    }

    public class Character : MovingObject
    {
        /* Class for base methods that character and enemy will share
         * Include base movement and using an attack
         */ 
        //Author: Zoe McHenry

        //Doesn't look like anyone has done anything with this and I need it done so I'm gonna do it. -ZM
        //I'm changing this to a public instead of an abstract, as I'm changing the way the character loading works. -ZM

        // Atributes
        // Health and Power
        private int health, power;

        // Attributes for the walk cycle
        float timer = 0f;
        float interval = 200f;
        int currentFrame = 0;
        int frameWidth = 100;
        int frameHeight = 100; 
        Texture2D texture;
        Vector2 position, origin;
        Rectangle sourceRect;

        // Keyboard States
        KeyboardState currentKBState;
        KeyboardState pastKBState;

        // Get Sets
        public int Health
        {
            get { return health; }
            set { this.health = value; }
        }
        public int Power
        {
            get { return this.power; }
            set { this.power = value; }
        }
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }

        }
        public Rectangle SourceRect
        {
            get { return sourceRect; }
            set { sourceRect = value; }
        }
        //public AlpacaType AlpacaType
        //{
        //    get { return this.alpacaType; }
        //}

        // Construcor
        public Character(int x, int y, int width, int height, int health, int power, Texture2D texture, int currentFrame)
            : base(x, y, width, height)
        {
            this.health = health;
            this.power = power;
            this.texture = texture;
            this.currentFrame = currentFrame;
        }

        // Methods
        public bool Collision(Tile worldTile)
        {
            if (worldTile.TileRectangle.Intersects(this.ObjectSquare))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Not Used May Delete
        //public void ObjectCollide(GameObject block)
        //{
        //    // Collision detection between the character and the block object
        //    if (this.ObjectPosX + this.ObjectSquare.Width >= block.ObjectPosX)
        //    {
        //        this.ObjectPosX = this.ObjectPosX - 1;
        //    }
        //    if (this.ObjectPosX <= block.ObjectPosX + block.ObjectSquare.Width)
        //    {
        //        this.ObjectPosX = this.ObjectPosX + 1;
        //    }
        //    if (this.ObjectPosY <= block.ObjectPosY + block.ObjectSquare.Width)
        //    {
        //        this.ObjectPosY = this.ObjectPosY + 1;
        //    }
        //    if (this.ObjectPosY + this.ObjectSquare.Height >= block.ObjectSquare.Height)
        //    {
        //        this.ObjectPosY = this.ObjectPosY - 1;
        //    }
        //}

        // Check for if a key is being pressed. If not, it sets the alpaca to neutral position
        public void WalkCheck(GameTime gameTime)
        {
            pastKBState = currentKBState;
            currentKBState = Keyboard.GetState();

            sourceRect = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);

            if (currentKBState.GetPressedKeys().Length == 0)
            {
                if (currentFrame > 0 && currentFrame <= 3)
                {
                    currentFrame = 2;
                }
                if (currentFrame > 3 && currentFrame <= 6)
                {
                    currentFrame = 5;
                }
            }

            if(currentKBState.IsKeyDown(Keys.D))
            {
                WalkControl(gameTime, 1, 3);
            }

            if (currentKBState.IsKeyDown(Keys.A))
            {
                WalkControl(gameTime, 4, 6);
            }

            origin = new Vector2(sourceRect.Width/2, sourceRect.Height/2);
        }

        // Take in the frame the direction starts and ends at and creates a walk cycle for between those frames. 
        public void WalkControl(GameTime gameTime, int startFrame, int endFrame)
        {
            if (currentKBState != pastKBState)
            {
                currentFrame = startFrame;
            }

            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer > interval)
            {
                currentFrame++;
                if (currentFrame > endFrame)
                {
                    currentFrame = startFrame;
                }
                timer = 0f;
            }
        }

        public override void Draw(SpriteBatch objectBatch, Texture2D objectPic)
        {
            base.Draw(objectBatch, objectPic);
        }

        public void Load(string filename)
        {
            using (var reader = new BinaryReader(File.OpenRead(filename)))
            {
                health = reader.ReadInt32();
                power = reader.ReadInt32();

                byte r, g, b, a;
                r = reader.ReadByte();
                g = reader.ReadByte();
                b = reader.ReadByte();
                a = reader.ReadByte();

                MyColor = Color.FromNonPremultiplied(r, g, b, a);
            }
        }
    }
}
