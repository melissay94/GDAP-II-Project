using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AlpacaWithBonnets
{

    class Character 
    {

        Game1 content;
        SpriteBatch spriteBatch;

        Texture2D characterImage;
        float timer, interval;
        int currentFrame, charWidth, charHeight, charSpeed;
        Rectangle scrRect;
        Vector2 position;
        Vector2 origin;

        KeyboardState keyState;
        KeyboardState prevKeyState;

        public Character(int currentFrame, int charWidth, int charHeight)
        {
           
            timer = 0f;
            interval = 200f;

            this.currentFrame = currentFrame;
            currentFrame = 0;

            this.charWidth = charWidth;
            charWidth = 100;

            this.charHeight = charHeight;
            charHeight = 100;

            charSpeed = 50;

        }

        // Properties 
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

        public Texture2D Image
        {
            get { return characterImage; }
            set { characterImage = value; }
        }

        public Rectangle Source
        {
            get { return scrRect; }
            set { scrRect = value; }
        }

        public void LoadCharacter(Game1 content, Texture2D charImage)
        {
            this.characterImage = charImage;
            charImage = content.Content.Load<Texture2D>("walkcycle");
            
        }

        public void HandleCharacterMovement(GameTime gameTime)
        {
            prevKeyState = keyState;
            keyState = Keyboard.GetState();

            scrRect = new Rectangle(currentFrame * charWidth, 0, charWidth, charHeight);

            if (keyState.GetPressedKeys().Length == 0)
            {
                if (currentFrame == 0 || currentFrame == 2)
                {
                    currentFrame = 1;
                }
                if (currentFrame == 3 || currentFrame == 5)
                {
                    currentFrame = 4;
                }
            }

            // Check for key presses 
            if (keyState.IsKeyDown(Keys.A))
            {
                GoLeft(gameTime);
            }
            else if (keyState.IsKeyDown(Keys.D))
            {
                GoRight(gameTime);
            }

            origin = new Vector2(scrRect.Width / 2, scrRect.Height / 2);
        }

        // Methods for which direction has which frames
        public void GoLeft(GameTime gameTime)
        {
            if (keyState != prevKeyState)
            {
                currentFrame = 0;
            }

            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer > interval)
            {
                currentFrame++;

                if (currentFrame > 2)
                {
                    currentFrame = 0;
                }

                timer = 0f;
            }
        }

        public void GoRight(GameTime gameTime)
        {
            if (keyState != prevKeyState)
            {
                currentFrame = 3;
            }

            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer > interval)
            {
                currentFrame++;

                if (currentFrame > 5)
                {
                    currentFrame = 3;
                }

                timer = 0f;
            }
        }

        public void DrawCharacter(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(characterImage, scrRect, Color.White);
        }

        // Walk cycle link: http://www.dreamincode.net/forums/topic/194878-xna-animated-sprite/

    }
}
