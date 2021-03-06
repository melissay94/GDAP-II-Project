﻿using System;
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

        // This method changes what the game draws depending on the game state
        public void DrawCheck(TheGameStates aGameState, SpriteFont aSpriteFont, SpriteBatch aSpriteBatch)
        {
            switch (aGameState)
            {
                case TheGameStates.Start:
                    // Making all of the statements for the Start Menu
                    string title = "Alpacas with Bonnets";
                    string goalOfGame = "Collect the bonnet";
                    string instructions = "Use A and D to move left and right, W to jump";

                    // Printing all of the different statements
                    aSpriteBatch.DrawString(aSpriteFont, title, new Vector2(50, 20), Color.Black);
                    aSpriteBatch.DrawString(aSpriteFont, goalOfGame, new Vector2(50, 35), Color.Black);
                    aSpriteBatch.DrawString(aSpriteFont, instructions, new Vector2(50, 50), Color.Black);
                    
                    break;

                case TheGameStates.Pause:
                    //Make the statements for the pause
                    string pause = "Pause Game";
                    string goBack = "Press Enter to go back to Game";
                    string startMenu = "Press S to go back to Start";

                    // Print the statements to the menu
                    aSpriteBatch.DrawString(aSpriteFont, pause, new Vector2(50, 20), Color.Black);
                    aSpriteBatch.DrawString(aSpriteFont, goBack, new Vector2(50, 35), Color.Black);
                    aSpriteBatch.DrawString(aSpriteFont, startMenu, new Vector2(50, 50), Color.Black);

                    break;

                case TheGameStates.End:
                    // Making the statements to print
                    string gameOver = "Game Over";
                    string restart = "Press enter to go back to start";

                    //Printing the statements
                    aSpriteBatch.DrawString(aSpriteFont, gameOver, new Vector2(50, 20), Color.Black);
                    aSpriteBatch.DrawString(aSpriteFont, restart, new Vector2(50, 35), Color.Black);

                    break;
            }
        }

        // Handles Keyboard Input from the player
        // Used for Movement and Attacking
        public void HandleInput(GameTime gameTime, KeyboardState keyState, Character newCharacter, GameObject ammo)
        {
            // Move Left
            if (keyState.IsKeyDown(Keys.A))
            {
                newCharacter.ObjectPosX -= (newCharacter.ObjectSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds); 
            }
            // Move Right
            if (keyState.IsKeyDown(Keys.D))
            {
               newCharacter.ObjectPosX += (newCharacter.ObjectSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
        }
    }
}
