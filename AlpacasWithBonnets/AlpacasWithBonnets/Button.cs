using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace AlpacasWithBonnets
{
    class Button
    {

        // Attribute for each button
        private Texture2D image;
        private SpriteFont font;
        Rectangle location;
        string text;
        Vector2 textLocation;
        SpriteBatch spriteBatch;
        MouseState mouse;

        // Initial button state is always unclicked
        bool clicked = false;

        // Constructor
        public Button(Texture2D image, SpriteFont font, SpriteBatch sBatch, String text)
        {
            this.image = image;
            this.font = font;
            this.text = text;
            spriteBatch = sBatch;

        }

        // Set the location of the button and the text within the button
        public void ButtonLocation(int x, int y)
        {
            location = new Rectangle(x, y, 170, 50);

            textLocation = new Vector2();
            textLocation.Y = y + location.Height / 4;
            textLocation.X = x + location.Width / 4;
        }

        // Set the text for the button
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        // Return whether a button has been clicked on or not
        public bool ButtonUpdate()
        {
            mouse = Mouse.GetState();

            if (mouse.LeftButton == ButtonState.Pressed)
            {
                if (location.Contains(new Point(mouse.X, mouse.Y)))
                {
                    return clicked = true;
                }
                else
                {
                    return clicked = false;
                }
            }
            else
            {
                return clicked = false;
            }
        }

        // Draw the button based on mouse location
        public void Draw()
        {
            //Commenting this out for the time being, it was giving me errors running the game in Debug - ZM

            //if (location.Contains(new Point(mouse.X, mouse.Y)))
            //{
            //    spriteBatch.Draw(image, location, Color.DarkGreen);
            //}
            //else
            //{
            //    spriteBatch.Draw(image, location, Color.ForestGreen);
            //}

            spriteBatch.DrawString(font, text, textLocation, Color.Black);

            if (clicked)
            {
                spriteBatch.DrawString(font, text, textLocation, Color.Black);
            }
        }
    }
}
