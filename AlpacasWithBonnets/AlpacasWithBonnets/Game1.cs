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
        End  // Show final score 
    }


    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch; 

        //Zoe McHenry
        Character character;
        CharacterIO characterIO;
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

        // Character walk textures 
        Texture2D walk1;
        Texture2D walk2;
        Texture2D walk3;

        // Jump attributes
        bool jumping;
        float startY;
        float startX;
        float jumpspeed = 0;

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
            // TODO: Add your initialization logic here
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
            startY = character.ObjectPosY;
            startX = character.ObjectPosX;

            // Load character walk cycle images
            walk1 = this.Content.Load<Texture2D>("walk1");
            walk2 = this.Content.Load<Texture2D>("walk2");
            walk3 = this.Content.Load<Texture2D>("walk3");
     
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

            if (currentGameState == TheGameStates.Start && SingleKeyPress(Keys.Enter))
            {
                currentGameState = TheGameStates.Game;
            }

            if (currentGameState == TheGameStates.Game)
            {
                myGameState.HandleInput(gameTime, keyState, character);
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
                else if (jumping)
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
                else
                {
                    if (keyState.IsKeyDown(Keys.W))
                    {
                        jumping = true;
                        jumpspeed = -14;
                    }
                }
            }
          

            base.Update(gameTime);
        }

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
            map.Draw(spriteBatch);

            // Calling the GameStates DrawCheck method
            myGameState.DrawCheck(currentGameState, theFont, spriteBatch);

            // Alpaca drawing
            if (currentGameState == TheGameStates.Game)
            {
                character.Draw(spriteBatch, walk1);
            }

            spriteBatch.End();
           

            base.Draw(gameTime);
        }
    }
}
