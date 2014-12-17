using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace AlpacaWithBonnets
{
    abstract class MovingGameObject : GameObject
    {
         // Each object will have a different speed
        private float objectSpeed;
        // Direction
        private int direction;

        // Get Set
        public float ObjectSpeed
        {
            get { return objectSpeed; }
            set { objectSpeed = value; }
        }
        public int Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        // Constructor 
        public MovingGameObject(float objectSpeed, int x, int y, int width, int height)
            : base(x, y, width, height)
        {
            this.objectSpeed = objectSpeed;
        }

        //Character Constructor Zoe McHenry
        public MovingGameObject(int x, int y, int width, int height)
            : base(x, y, width, height)
        {

        }

        // Inherited draw method
        public override void Draw(SpriteBatch objectBatch, Texture2D objectPic)
        {
            base.Draw(objectBatch, objectPic);
        }

        // Method for objects to move left and right, direction is 0 for moving Right, direction is 1 for moving Left
        public void Move(int direction)
        {
            if (direction == 0)
            {
                ObjectPosX += ObjectSpeed;
            }
            if (direction == 1)
            {
                ObjectPosX -= ObjectSpeed;
            }
        }
    }
}
