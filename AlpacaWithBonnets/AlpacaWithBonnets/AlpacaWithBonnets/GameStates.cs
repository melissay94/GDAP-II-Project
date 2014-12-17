using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AlpacaWithBonnets
{

    // Create game states
    public enum TheGameStates
    {
        Start, // Show title and instructions
        Game, // Hold levels of the game
        Pause, // Pause the game
        End // Show final score
    }

    // Hold logic for each of the game states
    class GameStates
    {
        TheGameStates currentState;
        Game1 content;
       
        // Everything to draw a button
        Buttons play;
        Buttons exit;
        Buttons playAgain;
        Buttons returnToGame;
        Buttons restartGame;

        Texture2D buttonImage;

        // Window height and width at dead center
        int centerWidth;
        int centerHeight;

        public GameStates(Game1 content)
        {
            this.content = content;
        }

        // Load all the buttons
        public void LoadButtons(SpriteFont gameFont, SpriteBatch spriteBatch)
        {
            buttonImage = content.Content.Load<Texture2D>("button");

            play = new Buttons(buttonImage, gameFont, spriteBatch, "Click to Play!");
            exit = new Buttons(buttonImage, gameFont, spriteBatch, "Exit");
            playAgain = new Buttons(buttonImage, gameFont, spriteBatch, "Play Again?");
            returnToGame = new Buttons(buttonImage, gameFont, spriteBatch, "Back to Game");
            restartGame = new Buttons(buttonImage, gameFont, spriteBatch, "Restart Game");
        }

        public TheGameStates CurrentState
        {
            get { return currentState; }
            set { currentState = value; }
        }

        // Change what's drawn based on the current game state
        public void DrawState(TheGameStates currentState, SpriteFont gameFont, SpriteBatch spriteBatch)
        {
            centerWidth = content.GraphicsDevice.Viewport.Width / 2;
            centerHeight = content.GraphicsDevice.Viewport.Height / 2;

            switch (currentState)
            {
                case TheGameStates.Start:
                    // Make all of the statements for the Start Menu
                    string title = "Alpacas with Bonnets";
                    string goalOfGame = "Collect the bonnet";
                    string instructions = "Use A and D to move left and right, W to jump";

                    // Print all of the different statements
                    spriteBatch.DrawString(gameFont, title, new Vector2(centerWidth - (int)title.Length * 4, centerHeight - 100), Color.Black);
                    spriteBatch.DrawString(gameFont, goalOfGame, new Vector2(centerWidth - (int)goalOfGame.Length * 4, centerHeight + 75), Color.Black);
                    spriteBatch.DrawString(gameFont, instructions, new Vector2(centerWidth - (int)instructions.Length * 4, centerHeight + 100), Color.Black);

                    // Draw all the buttons
                    play.Draw();
                    play.ButtonLocation(centerWidth - 170 / 2, centerHeight - 50);

                    exit.Draw();
                    exit.ButtonLocation(centerWidth - 170 / 2, centerHeight);
                    
                    break;

                case TheGameStates.Pause:
                    //Make the statements for the pause
                    string pause = "Paused Game";

                    // Print the statements to the menu
                    spriteBatch.DrawString(gameFont, pause, new Vector2(centerWidth - (int)pause.Length * 4, centerHeight - 100), Color.Black);

                    // Draw all the buttons
                    returnToGame.Draw();
                    returnToGame.ButtonLocation(centerWidth - 170 / 2, centerHeight - 50);

                    restartGame.Draw();
                    restartGame.ButtonLocation(centerWidth - 170 / 2, centerHeight);

                    exit.Draw();
                    exit.ButtonLocation(centerWidth - 170 / 2, centerHeight + 50);

                    break;

                case TheGameStates.End:
                    // Make the statements to print
                    string gameOver = "Game Over";

                    //Print the statements
                    spriteBatch.DrawString(gameFont, gameOver, new Vector2(centerWidth - (int)gameOver.Length * 4, centerHeight - 100), Color.Black);
      
                    // Draw all the buttons
                    playAgain.Draw();
                    playAgain.ButtonLocation(centerWidth - 170 / 2, centerHeight - 50);

                    exit.Draw();
                    exit.ButtonLocation(centerWidth - 170 / 2, centerHeight);

                    break;
            }
        }

        // Check for the changing between states based on buttons pressed in the menus
        public void ChangeState(TheGameStates currentState, KeyboardState keyState, KeyboardState prevKeyState, Character mainCharacter, GameTime gameTime)
        {
            // Allow use of mouse during menu states
            content.IsMouseVisible = true;

            switch (currentState)
            {
                // Start changes based on play and exit
                case TheGameStates.Start:
                    {
                        // Check for when buttons are clicked
                        if (play.ButtonUpdate())
                        {
                            CurrentState = TheGameStates.Game;
                        }
                        else if (exit.ButtonUpdate())
                        {
                            content.Exit();
                        }
                        break;
                    }

                // Game doesn't change based on buttons, just p for pause
                case TheGameStates.Game:
                    {
                        // Disallow use of mouse during gameplay
                        content.IsMouseVisible = false;

                        if (CurrentState == TheGameStates.Game && SingleKeyPress(Keys.P, keyState, prevKeyState))
                        {
                            CurrentState = TheGameStates.Pause;
                        }

                        break;
                    }

                // Pause changes based on return button, restart button, or the exit button
                case TheGameStates.Pause:
                    {
                        // Check for when buttons are clicked
                        if (returnToGame.ButtonUpdate())
                        {
                            CurrentState = TheGameStates.Game;
                        }
                        else if (restartGame.ButtonUpdate())
                        {
                            ResetGame(mainCharacter);
                            CurrentState = TheGameStates.Game;
                        }
                        else if (exit.ButtonUpdate())
                        {
                            content.Exit();
                        }
                        break;
                    }

                // End changes based on play again button and exit button
                case TheGameStates.End:
                    {
                        // Check for when buttons are clicked
                        if (playAgain.ButtonUpdate())
                        {
                            ResetGame(mainCharacter);
                            CurrentState = TheGameStates.Game;
                        }
                        else if (exit.ButtonUpdate())
                        {
                            content.Exit();
                        }

                        break;
                    }
            }
        }

        public void ResetGame(Character mainCharacter)
        {
            mainCharacter.TotalScore = 0;
            mainCharacter.ObjectPosX = 0;
            mainCharacter.ObjectPosY = 250;
        }

        // Used by the handleStateInput method to make sure p was properly pressed
        protected Boolean SingleKeyPress(Keys keyPress, KeyboardState keyState, KeyboardState prevKeyState)
        {
            if (prevKeyState != null && prevKeyState.IsKeyDown(keyPress))
            {
                return false;
            }

            return keyState.IsKeyDown(keyPress);
        }
    }
}
