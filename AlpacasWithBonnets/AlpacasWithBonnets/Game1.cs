using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AlpacasWithBonnets
{
    /// <summary>
    /// Game: Aplacas!
    /// </summary>
    /// 

    // Creating the Game States
    public enum TheGameStates
    {
        Start, // Show title and instructions
        Game, //  levels of gameplay
        Pause, // Pause the game 
        End  // Show final score 
    }

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        // Attributes
        // Starting attributes
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Jump attributes
        bool jumping;
        float startY;
        float startX;
        float jumpspeed = 0;
        // Single bolt attribute
        bool isActive;

        // The single items
        Character character;
        CharacterIO characterIO;
        Goal goal;
        Platform block;
        TheGameStates currentGameState;
        SpriteFont theFont;

        // Moving Objects
        MovingObject bolt;
        MovingObject enemy;

        // Keyboard States
        KeyboardState keyState;
        KeyboardState previouskbState;

        // Game Button Objects
        Button playButton;
        Button exitButton;
        Button playAgainButton;

        // Texture2D
        Texture2D test;
        Texture2D boltImage;
        Texture2D enemyImage;
        Texture2D walk1;
        Texture2D walkCycle;
        Texture2D buttonImage;

        // Points
        Point frameSize = new Point(50, 50);
        Point currentFrame = new Point(0, 250);
        Point sheetSize = new Point(1, 3);

        // Vector2
        Vector2 spritePosition;
        Vector2 goalPosition;
        Vector2 blockPosition;

        // Objects
        Map map = new Map(16, 10);
        GameStates myGameState = new GameStates();

        // Constructor
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            currentGameState = TheGameStates.Start;
            jumping = false;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            map.LoadMap("testLevel.txt", Content);

            // Font
            theFont = this.Content.Load<SpriteFont>("AvoiderFont");

            //CharacterIO
            characterIO = new CharacterIO();
            //character = characterIO.LoadCharacter("testFile.alpaca");

            // Colliding objects for goal and block to collide with in order to get to the end of the game
            goalPosition = new Vector2(GraphicsDevice.Viewport.Width - 50, 250);
            goal = new Goal(goalPosition, Content);

            // Block Position
            blockPosition = new Vector2(150,250);
            block = new Platform(blockPosition, Content);

            // Character Walk Cycle
            walkCycle = this.Content.Load<Texture2D>("walk1");
            character = new Character(0, 250, 100, 100, 100, 50, walkCycle, 2);
            walk1 = this.Content.Load<Texture2D>("walk1");

            // Enemy
            enemy = new MovingObject(0, 550, 250, 100, 100);
            enemyImage = this.Content.Load<Texture2D>("enemyTest");

            // Sky
            test = this.Content.Load<Texture2D>("sky");
           
            // Bolt
            bolt = new MovingObject((int)character.ObjectPosX + (int)character.ObjectSquare.Width,
                (int)character.ObjectPosY + (int)character.ObjectSquare.Height / 2, 10, 20);
            spritePosition = new Vector2(640, 450);
            boltImage = this.Content.Load<Texture2D>("bolt");
            isActive = false;

            // Starting character positions
            startY = character.ObjectPosY;
            startX = character.ObjectPosX;

            // Buttons
            buttonImage = this.Content.Load<Texture2D>("button");
            playButton = new Button(buttonImage, theFont, spriteBatch, "Play!");
            exitButton = new Button(buttonImage, theFont, spriteBatch, "Exit");
            playAgainButton = new Button(buttonImage, theFont, spriteBatch, "Play Again");    
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Keyboard states
            previouskbState = keyState;
            keyState = Keyboard.GetState();
            
            // Exit the game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // Start of the game
            if (currentGameState == TheGameStates.Start)
            {
                // Making the mouse visible to click the buttons
                IsMouseVisible = true;
                // Position the button
                playButton.ButtonLocation(GraphicsDevice.Viewport.Width / 2 - 50, GraphicsDevice.Viewport.Height / 2 - 50);
                playButton.ButtonUpdate();
                //Check the value of buttonupdate
                if (playButton.ButtonUpdate() == true)
                {
                    currentGameState = TheGameStates.Game;
                    IsMouseVisible = false;
                }
            }

            // Playing the game
            if (currentGameState == TheGameStates.Game)
            {
                // Handling input
                myGameState.HandleInput(gameTime, keyState, character, bolt);
                // Detecting Collisions
                CollisionDetection();
                // Checking the walk cycle
                character.WalkCheck(gameTime);

                // Checking what to do if a key is pressed
                if (SingleKeyPress(Keys.Space) && !isActive)
                {
                    // Bolt activity
                    isActive = true;
                    // Moving the bolt
                    while (bolt.ObjectPosX + bolt.ObjectSquare.Width <= 200)
                    {
                        bolt.ObjectPosX += (bolt.ObjectSpeed * 2 * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                    // Changing the Bolt's position
                    bolt.ObjectPosX = character.ObjectPosX + character.ObjectSquare.Width;
                    bolt.ObjectPosY = character.ObjectPosY + character.ObjectSquare.Height / 2;
                }

                // Jump in the correct direction
                // Jump to the left
                if (jumping && keyState.IsKeyDown(Keys.A))
                {
                    character.ObjectPosY += jumpspeed;
                    character.ObjectPosX -= Math.Abs(jumpspeed);
                    jumpspeed += 1;
                    if (character.ObjectPosY > startY)
                    {
                        character.ObjectPosY = startY;
                        jumping = false;
                    }
                }
                // Jumping to the right
                else if (jumping && keyState.IsKeyDown(Keys.D))
                {
                    character.ObjectPosY += jumpspeed;
                    character.ObjectPosX += Math.Abs(jumpspeed);
                    jumpspeed += 1;
                    if (character.ObjectPosY > startY)
                    {
                        character.ObjectPosY = startY;
                        jumping = false;
                    }
                }
                // Jumping strait up
                else if (jumping)
                {
                    character.ObjectPosY += jumpspeed;
                    jumpspeed += 1;
                    if (character.ObjectPosY > startY)
                    {
                        character.ObjectPosY = startY;
                        jumping = false;
                    }
                }
                // Jumping strait up
                else
                {
                    if (keyState.IsKeyDown(Keys.W))
                    {
                        jumping = true;
                        jumpspeed = -14;
                    }
                }
            }

            // End of the game
            if (currentGameState == TheGameStates.End)
            {
                // Making the mouse visible to click the buttons
                IsMouseVisible = true;
                
                // Buttons
                // Exit button
                exitButton.ButtonLocation(GraphicsDevice.Viewport.Width / 2 - 50, GraphicsDevice.Viewport.Height / 2 - 50);
                exitButton.ButtonUpdate();
                if (exitButton.ButtonUpdate() == true)
                {
                    this.Exit();
                }

                // Play again button
                playAgainButton.ButtonLocation(GraphicsDevice.Viewport.Width / 2 - 50, GraphicsDevice.Viewport.Height / 2);
                playAgainButton.ButtonUpdate();
                if (playAgainButton.ButtonUpdate() == true)
                {
                    currentGameState = TheGameStates.Start;
                    IsMouseVisible = true;
                }
            }

            // Changing the game state if certain keys are pressed
            // Pause if in the Game 
            if (currentGameState == TheGameStates.Game && SingleKeyPress(Keys.P))
            {
                currentGameState = TheGameStates.Pause;
            }
            // Start the game if in the Pause menu
            if (currentGameState == TheGameStates.Pause && SingleKeyPress(Keys.Enter))
            {
                currentGameState = TheGameStates.Game;
            }
            // Go back to start if in the Pause menu
            if (currentGameState == TheGameStates.Pause && SingleKeyPress(Keys.S))
            {
                currentGameState = TheGameStates.Start;
            }

            base.Update(gameTime);
        }

        // Method to detect if the character has collided with an object
        public void CollisionDetection()
        {
            // Character touches the enemy
            if (character.ObjectSquare.Intersects(enemy.ObjectSquare))
            {
                // Game ends because you lost
                currentGameState = TheGameStates.End;
            }
            // Character touches the Platforms
            if (character.ObjectSquare.Intersects(block.TileRectangle))
            {
                if (character.ObjectPosX < block.TileLocation.X)
                {
                    character.ObjectPosX = block.TileLocation.X - 1;
                    //jumpspeed = 0;
                }
            }
            // Characer reaches the right edge of the screen
            if (character.ObjectPosX + character.ObjectSquare.Width >= GraphicsDevice.Viewport.Width)
            {
                character.ObjectPosX = GraphicsDevice.Viewport.Width - character.ObjectSquare.Width;
            }
            // Character reaches the left edge of the screen
            if (character.ObjectPosX <= 0)
            {
                character.ObjectPosX = 1;
            }
            // Character reaches the Goal
            if (character.ObjectSquare.Intersects(goal.TileRectangle))
            {
                currentGameState = TheGameStates.End;
            }
        }

        // Take in a single key to check if its been hit
        public Boolean SingleKeyPress(Keys keyPress)
        {
            if (previouskbState != null && previouskbState.IsKeyDown(keyPress))
            {
                return false;
            }

            return keyState.IsKeyDown(keyPress);
        }

        // Update Amo
        public void UpdateAmmo()
        {
            bolt.ObjectPosX += bolt.ObjectSpeed * 2;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.AliceBlue);
            spriteBatch.Begin();

            // Calling the GameStates DrawCheck method to set up some of the basic things
            myGameState.DrawCheck(currentGameState, theFont, spriteBatch);

            // Button drawing
            // Start
            if (currentGameState == TheGameStates.Start)
            {
                playButton.Draw();
            }
            // End
            if (currentGameState == TheGameStates.End)
            {
                exitButton.Draw();
                playAgainButton.Draw();
            }

            // Alpaca drawing
            if (currentGameState == TheGameStates.Game)
            {
                // Draw the map
                map.Draw(spriteBatch);

                // Draw the bolt if it is active
                if (isActive == true)
                {
                    bolt.Draw(spriteBatch, boltImage);
                }

                // Drawing the character, enemy, and blocks
                character.Draw(spriteBatch, character.Texture);
                enemy.Draw(spriteBatch, enemyImage);
                block.Draw(spriteBatch);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        } 
    }
}
