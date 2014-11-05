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

    // Making the different Game States that are needed
    public enum TheGameStates
    {
        Start,
        Game,
        End
    }

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //Zoe McHenry
        //
        Character character;
        CharacterIO characterIO;
        //

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Map map = new Map(100, 10); //For testing

        // TheGameStates variable
        TheGameStates currentGameState;
        // GameState object
        GameStates myGameState = new GameStates();
        // Sprite Font
        SpriteFont theFont;

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
            //theFont = this.Content.Load<SpriteFont>("AvoiderFont");
            // TODO: use this.Content to load your game content here

            //Zoe McHenry
            //
            characterIO = new CharacterIO();
            //character = characterIO.LoadCharacter("testFile.alpaca");
            //
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

            // Calls the GameStates MenuCheck method
            currentGameState = myGameState.MenuCheck(currentGameState);

            // TODO: Add your update logic here

            base.Update(gameTime);
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

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
