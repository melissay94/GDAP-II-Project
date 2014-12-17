﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AlpacaWithBonnets
{

    class Character : MovingGameObject
    {

        Texture2D characterImage;
        float scale = 1.0f;

        int charWidth, charHeight;
        bool isJumping;
        int jumpSpeed;
        float startY, startX;

        int health;
        int power;

        Vector2 origin = new Vector2();

        KeyboardState keyState;

        int totalScore;

        public Character(int charWidth, int charHeight) 
            : base (0, 250, charWidth, charHeight)
        {
            this.charWidth = charWidth;
            this.charHeight = charHeight;
            ObjectSpeed = 150;

            isJumping = false;
            startX = ObjectPosX;
            startY = ObjectPosY;

            totalScore = 0;
            health = 100;

        }

        // Properties 
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

        public int TotalScore
        {
            get { return totalScore; }
            set { totalScore = value; }
        }

        public int TotalHealth
        {
            get { return health; }
            set { health = value; }
        }

        public void LoadCharacter(Game1 content, String character)
        {

            characterImage = content.Content.Load<Texture2D>(character);
        }

        public void HandleCharacterMovement(GameTime gameTime)
        {
            keyState = Keyboard.GetState();

            // Movement
            if (keyState.IsKeyDown(Keys.A))
            {
                ObjectPosX -= (ObjectSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                ObjectPosX += (ObjectSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            // Jumping code
            if (isJumping && keyState.IsKeyDown(Keys.A))
            {
                ObjectPosY += jumpSpeed;
                ObjectPosX -= Math.Abs(jumpSpeed);
                jumpSpeed += 1;
                if (ObjectPosY > startY)
                {
                    ObjectPosY = startY;
                    isJumping = false;
                }
            }
            else if (isJumping && keyState.IsKeyDown(Keys.D))
            {
                ObjectPosY += jumpSpeed;
                ObjectPosX += Math.Abs(jumpSpeed);
                jumpSpeed += 1;
                if (ObjectPosY > startY)
                {
                    ObjectPosY = startY;
                    isJumping = false;
                }
            }
            else if (isJumping)
            {
                ObjectPosY += jumpSpeed;
                jumpSpeed += 1;
                if (ObjectPosY > startY)
                {
                    ObjectPosY = startY;
                    isJumping = false;
                }
            }
            else
            {
                if (keyState.IsKeyDown(Keys.W))
                {
                    isJumping = true;
                    jumpSpeed = -18;
                }
            }
        }

        // Method for loading from the external
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

        public void DrawCharacter(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(characterImage, ObjectSquare, Color.White);
        }
    }
}
