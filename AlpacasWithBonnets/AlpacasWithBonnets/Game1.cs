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
    /// This is the main type for your game
    /// 
    /// Aplacas!!!!!!!
    /// </summary>
    /// 

    //Zoe McHenry - implementing level & testing
    public enum TheGameStates
    {
        Start, // Show title and instructions
        Game, //  levels of gameplay
        Pause, // Pause the game 
        End  // Show final score 
    }


    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch; 

        //Zoe McHenry
        Character character;
        CharacterIO characterIO;
        GameObject goal;
        GameObject block;
        Map map = new Map(16, 10); //For testing

        // Current game state variable based on TheGameStates
        TheGameStates currentGameState;

        // GameState object
        GameStates myGameState = new GameStates();

        // Sprite Font
        SpriteFont theFont;

        // Keyboard States
        KeyboardState keyState;
        KeyboardState previouskbState;

        // Alpaca walk
        Texture2D walk1;
        Texture2D walkCycle;
        Point frameSize = new Point(50, 50);
        Point currentFrame = new Point(0, 250);
        Point sheetSize = new Point(1, 3);

        // Jump attributes
        bool jumping;
        float startY;
        float startX;
        float jumpspeed = 0;

        Texture2D test;

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

            // Identifying the Font
            theFont = this.Content.Load<SpriteFont>("AvoiderFont");

            //Zoe McHenry
            //
            characterIO = new CharacterIO();
            //character = characterIO.LoadCharacter("testFile.alpaca");
            character = new Character(0, 250, 100, 100, 100, 50);

            // Temporary gameObject to collide with in order to get to the end of the game
            goal = new GameObject(GraphicsDevice.Viewport.Width - 50, 250, 50, 50);
            block = new GameObject(150, 250, 50, 50);

            startY = character.ObjectPosY;
            startX = character.ObjectPosX;

            // Load character walk cycle images
            walk1 = this.Content.Load<Texture2D>("walk1");
            test = this.Content.Load<Texture2D>("sky");
            walkCycle = this.Content.Load<Texture2D>("walkcycle");
          
     
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            previouskbState = keyState;
            keyState = Keyboard.GetState();

            // Use enter key to change from the start menu to the game screen
            if (currentGameState == TheGameStates.Start && SingleKeyPress(Keys.Enter))
            {
                currentGameState = TheGameStates.Game;
            }

            if (currentGameState == TheGameStates.Game)
            {
                myGameState.HandleInput(gameTime, keyState, character);
                CollisionDetection(character);
               // character.ObjectCollide(block);


                // Create jump action based on character direction
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
                else
                {
                    if (keyState.IsKeyDown(Keys.W))
                    {
                        jumping = true;
                        jumpspeed = -14;
                    }
                }
            }

            if (currentGameState == TheGameStates.Game && SingleKeyPress(Keys.P))
            {
                currentGameState = TheGameStates.Pause;
            }

            if (currentGameState == TheGameStates.Pause && SingleKeyPress(Keys.Enter))
            {
                currentGameState = TheGameStates.Game;
            }

            if (currentGameState == TheGameStates.Pause && SingleKeyPress(Keys.S))
            {
                currentGameState = TheGameStates.Start;
            }

            if (currentGameState == TheGameStates.End && SingleKeyPress(Keys.Enter))
            {
                currentGameState = TheGameStates.Start;
            }

            base.Update(gameTime);
        }

        // Detection method for if the character has reached the edge of the screen
        public void CollisionDetection(Character newCharacter)
        {
            if (character.ObjectPosX + character.ObjectSquare.Width >= GraphicsDevice.Viewport.Width)
            {
                character.ObjectPosX = GraphicsDevice.Viewport.Width - character.ObjectSquare.Width;
            }
            if (character.ObjectPosX <= 0)
            {
                character.ObjectPosX = 1;
            }
            if (character.ObjectSquare.Intersects(goal.ObjectSquare))
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

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.AliceBlue);
            spriteBatch.Begin();

            // Calling the GameStates DrawCheck method
            myGameState.DrawCheck(currentGameState, theFont, spriteBatch);

            // Alpaca drawing
            if (currentGameState == TheGameStates.Game)
            {
                map.Draw(spriteBatch);
                character.Draw(spriteBatch, walk1);
                // block.Draw(spriteBatch, test);
            }
                spriteBatch.End();
                base.Draw(gameTime);
            }
        
    }
}
