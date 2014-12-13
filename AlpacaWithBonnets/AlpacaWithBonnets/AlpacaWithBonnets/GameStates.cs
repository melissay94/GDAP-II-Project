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

        Map currentMap;

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
            switch (currentState)
            {
                case TheGameStates.Start:
                    // Make all of the statements for the Start Menu
                    string title = "Alpacas with Bonnets";
                    string goalOfGame = "Collect the bonnet";
                    string instructions = "Use A and D to move left and right, W to jump";

                    // Print all of the different statements
                    spriteBatch.DrawString(gameFont, title, new Vector2(50, 20), Color.Black);
                    spriteBatch.DrawString(gameFont, goalOfGame, new Vector2(50, 35), Color.Black);
                    spriteBatch.DrawString(gameFont, instructions, new Vector2(50, 50), Color.Black);

                    // Draw all the buttons
                    play.Draw();
                    play.ButtonLocation(content.GraphicsDevice.Viewport.Width / 2 - 50, content.GraphicsDevice.Viewport.Height / 2 - 50);

                    exit.Draw();
                    exit.ButtonLocation(content.GraphicsDevice.Viewport.Width / 2 - 50, content.GraphicsDevice.Viewport.Height / 2);
                    
                    break;

                case TheGameStates.Pause:
                    //Make the statements for the pause
                    string pause = "Pause Game";
                    string goBack = "Press Enter to go back to Game";
                    string startMenu = "Press S to go back to Start";

                    // Print the statements to the menu
                    spriteBatch.DrawString(gameFont, pause, new Vector2(50, 20), Color.Black);
                    spriteBatch.DrawString(gameFont, goBack, new Vector2(50, 35), Color.Black);
                    spriteBatch.DrawString(gameFont, startMenu, new Vector2(50, 50), Color.Black);

                    // Draw all the buttons
                    returnToGame.Draw();
                    returnToGame.ButtonLocation(content.GraphicsDevice.Viewport.Width / 2 - 50, content.GraphicsDevice.Viewport.Height / 2 - 50);

                    restartGame.Draw();
                    restartGame.ButtonLocation(content.GraphicsDevice.Viewport.Width / 2 - 50, content.GraphicsDevice.Viewport.Height / 2);

                    exit.Draw();
                    exit.ButtonLocation(content.GraphicsDevice.Viewport.Width / 2 - 50, content.GraphicsDevice.Viewport.Height / 2 + 50);

                    break;

                case TheGameStates.End:
                    // Make the statements to print
                    string gameOver = "Game Over";
                    string restart = "Press enter to go back to start";

                    //Print the statements
                    spriteBatch.DrawString(gameFont, gameOver, new Vector2(50, 20), Color.Black);
                    spriteBatch.DrawString(gameFont, restart, new Vector2(50, 35), Color.Black);

                    // Draw all the buttons
                    playAgain.Draw();
                    playAgain.ButtonLocation(content.GraphicsDevice.Viewport.Width / 2 - 50, content.GraphicsDevice.Viewport.Height / 2 - 50);

                    exit.Draw();
                    exit.ButtonLocation(content.GraphicsDevice.Viewport.Width / 2 - 50, content.GraphicsDevice.Viewport.Height / 2);

                    break;
            }
        }

        // Check for the changing between states based on buttons pressed in the menus
        public void ChangeState(TheGameStates currentState, KeyboardState keyState, KeyboardState prevKeyState, GameTime gameTime)
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
                            CurrentState = TheGameStates.Start;
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
                            CurrentState = TheGameStates.Start;
                        }
                        else if (exit.ButtonUpdate())
                        {
                            content.Exit();
                        }
                        break;
                    }
            }
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
