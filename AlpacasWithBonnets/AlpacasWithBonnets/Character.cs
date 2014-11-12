using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;


namespace AlpacasWithBonnets
{
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

        private int health, power;

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

        //public AlpacaType AlpacaType
        //{
        //    get { return this.alpacaType; }
        //}

        public Character(int x, int y, int width, int height, int health, int power) : base(x, y, width, height)
        {
            this.health = health;
            this.power = power;
        }

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

        public void ObjectCollide(GameObject block)
        {
            // Collision detection between the character and the block object


            if (this.ObjectPosX + this.ObjectSquare.Width >= block.ObjectPosX)
            {
                this.ObjectPosX = this.ObjectPosX - 1;
            }
            if (this.ObjectPosX <= block.ObjectPosX + block.ObjectSquare.Width)
            {
                this.ObjectPosX = this.ObjectPosX + 1;
            }
            if (this.ObjectPosY <= block.ObjectPosY + block.ObjectSquare.Width)
            {
                this.ObjectPosY = this.ObjectPosY + 1;
            }
            if (this.ObjectPosY + this.ObjectSquare.Height >= block.ObjectSquare.Height)
            {
                this.ObjectPosY = this.ObjectPosY - 1;
            }
        }

        public override void Draw(SpriteBatch objectBatch, Texture2D objectPic)
        {

            base.Draw(objectBatch, objectPic);
        }
    }
}
