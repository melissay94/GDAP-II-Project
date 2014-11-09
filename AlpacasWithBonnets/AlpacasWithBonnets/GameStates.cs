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

namespace AlpacasWithBonnets
{
    class GameStates
    {
        // Sara Nuffer, James Borger
        // This class will have all of the information that the Game1 class will need to be able to work with the game states

        // Makes a keyboard state which can be used to tell what keys are being pressed
        KeyboardState Key;
        // This method changes the menus depending on the game state
        public TheGameStates MenuCheck(TheGameStates currentGameState) // Would have to take in a key pressed
        {
            TheGameStates returningGameState = TheGameStates.Start;
            switch (currentGameState)
            {
                case TheGameStates.Start:

                    returningGameState = TheGameStates.Game;
                    return returningGameState;
                case TheGameStates.Game:

                    returningGameState = TheGameStates.End;
                    return returningGameState;
                case TheGameStates.End:

                    returningGameState = TheGameStates.Start;
                    return returningGameState;
            }

            // returns Start if it does not go through the switch for whatever reason
            return returningGameState;
        }

        // This method changes what the game draws depending on the game state
        public void DrawCheck(TheGameStates aGameState, SpriteFont aSpriteFont, SpriteBatch aSpriteBatch) // Also needs the name of the spritefont
        {
            switch (aGameState)
            {
                case TheGameStates.Start:
                    // Making all of the statements for the Start Menu
                    string title = "Alpacas with Bonnets";
                    string goalOfGame = "Collect the bonnet";
                    string instructions = "Use A and D to move left and right, W to jump, and enter to play the game";

                    // Printing all of the different statements
                    aSpriteBatch.DrawString(aSpriteFont, title, new Vector2(10, 10), Color.Black);
                    aSpriteBatch.DrawString(aSpriteFont, goalOfGame, new Vector2(10, 25), Color.Black);
                    aSpriteBatch.DrawString(aSpriteFont, instructions, new Vector2(10, 40), Color.Black);
                    
                    break;
                case TheGameStates.Game:

                    break;
                case TheGameStates.End:
                    // Making the statements to print
                    string gameOver = "Game Over";
                    string restart = "Potentially press enter to go back to start";

                    //Printing the statements
                    aSpriteBatch.DrawString(aSpriteFont, gameOver, new Vector2(10, 10), Color.Black);
                    aSpriteBatch.DrawString(aSpriteFont, restart, new Vector2(10, 10), Color.Black);

                    break;
            }
        }

        // Handles Keyboard Input from the player
        //For Movement and attacking
        public void HandleInput()
        {
            Key = Keyboard.GetState();
            if (Key.IsKeyDown(Keys.W))
            {
                // Add Jump code here
            }
            if (Key.IsKeyDown(Keys.A))
            {
                // Movement to the Left here
            }
            if (Key.IsKeyDown(Keys.D))
            {
                // Movement to the Right here
            }
            if (Key.IsKeyDown(Keys.Space))
            {
                // attack Code here
            }
            if (Key.IsKeyDown(Keys.Escape))
            {
                // Pause the game
            }
        }
    }
}
