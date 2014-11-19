using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace AlpacasWithBonnets
{
    class Attack 
    {
        private Texture2D texture;

        private Vector2 position;
        private Vector2 velocity;
        private Vector2 origin;

        private bool isVisible;

        public Attack(Texture2D texture)
        {
            this.texture = texture;
            isVisible = false;
        }

        // Properties

        // The attack texture will change depending on the bonnet
        public Texture2D AttackTexture
        {
            get { return texture; }
            set { texture = value; }
        }

        public Vector2 AttackPosition
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 AttackVelocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public Vector2 AttackOrigin
        {
            get { return origin; }
            set { origin = value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, origin, 1f, SpriteEffects.None, 1);
        }

    }
}
