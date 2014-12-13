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

namespace AlpacaWithBonnets
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// 
    
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont gameFont;

        GameStates gameState;
        Map firstLevel;

        KeyboardState keyState;
        KeyboardState prevKeyState;

        Texture2D gameBackground;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            gameState = new GameStates(this);

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
            gameState.CurrentState = TheGameStates.Start;
            firstLevel = new Map(16, 10);

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

            gameBackground = this.Content.Load<Texture2D>("background");
            gameFont = this.Content.Load<SpriteFont>("gameFont");
            gameState.LoadButtons(gameFont, spriteBatch);
            firstLevel.LoadMap("firstLevel.txt", Content);
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

            prevKeyState = keyState;
            keyState = Keyboard.GetState();

            gameState.ChangeState(gameState.CurrentState, keyState, prevKeyState, gameTime);
           

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.AliceBlue);
            Rectangle backgroundRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            spriteBatch.Begin();

            // Make the maps for when in the game state
            if (gameState.CurrentState == TheGameStates.Game)
            {
                Vector2 pos = new Vector2(0f, 0f);
                firstLevel.Draw(spriteBatch, pos);
            }

            // Make the background for the menus
            else if (gameState.CurrentState == TheGameStates.Start || gameState.CurrentState == TheGameStates.Pause || gameState.CurrentState == TheGameStates.End)
            {
                spriteBatch.Draw(gameBackground, backgroundRect, Color.White);
            }

            gameState.DrawState(gameState.CurrentState, gameFont, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
