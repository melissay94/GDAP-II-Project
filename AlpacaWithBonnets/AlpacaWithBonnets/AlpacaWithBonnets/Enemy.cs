using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace AlpacaWithBonnets
{
    class Enemy :  MovingGameObject
    {

        Texture2D enemyImage;
        float startPosition;
        float counter;

        public Enemy(int x, int y, int width, int height)
            : base(x, y, width, height)
        {
            ObjectSpeed = 150f;
            startPosition = x;
        }

        //Properties
        public Texture2D EnemyImage
        {
            get { return enemyImage; }
            set { enemyImage = value; }
        }

        public void LoadEnemy(Game1 content)
        {
            enemyImage = content.Content.Load<Texture2D>("Enemy1");
        }

        public void EnemyMovement (GameTime gameTime)
        {
            counter += 0.02f;
            ObjectPosX = (startPosition + (float)Math.Sin(counter) * 100);

        }

        public void DrawEnemy(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(enemyImage, ObjectSquare, Color.White);
        }
    }
}
