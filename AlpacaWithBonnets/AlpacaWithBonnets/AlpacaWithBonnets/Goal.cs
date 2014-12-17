using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AlpacaWithBonnets
{
    class Goal : GameObject
    {
        Texture2D goalImage;
        bool isActive;

        public Goal()
            : base(50 * 25, 50 * 6, 50, 50)
        {
            isActive = true;
        }

        public bool ActiveGoal
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public void LoadGoal(Game1 content, String goalName)
        {
            goalImage = content.Content.Load<Texture2D>(goalName);
        }

        public void DrawGoal(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(goalImage, ObjectSquare, Color.White);
        }
    }
}
