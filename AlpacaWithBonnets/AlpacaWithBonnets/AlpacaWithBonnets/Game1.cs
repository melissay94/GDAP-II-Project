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
        Map secondLevel;
        Map thirdLevel;
        int levelCount;

        Character mainCharacter;
        Enemy mainEnemy;

        // Coins per level
        Texture2D coin;
        List<Collectible> generatedCoins;

        KeyboardState keyState;
        KeyboardState prevKeyState;

        Texture2D gameBackground;

        Goal firstGoal;
        Goal secondGoal;
        Goal thirdGoal;

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
            firstLevel = new Map(26, 10);
            secondLevel = new Map(26, 10);
            thirdLevel = new Map(26, 10);

            mainCharacter = new Character(100, 100);
       //     mainCharacter.Load("jimmy.alpaca");

            mainEnemy = new Enemy(500, 275, 75, 75);

            generatedCoins = new List<Collectible>();
            generatedCoins.Add(new Collectible(100, 100));
            generatedCoins.Add(new Collectible(400, 150));

            firstGoal = new Goal();
            secondGoal = new Goal();
            thirdGoal = new Goal();

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
            secondLevel.LoadMap("secondLevel.txt", Content);
            thirdLevel.LoadMap("thirdLevel.txt", Content);

            mainCharacter.LoadCharacter(this, "WalkG1");
            mainEnemy.LoadEnemy(this);
            coin = this.Content.Load<Texture2D>("coin");

            firstGoal.LoadGoal(this, "goal");
            secondGoal.LoadGoal(this, "fedoraGoal");
            thirdGoal.LoadGoal(this, "bonnetGoal");
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

            gameState.ChangeState(gameState.CurrentState, keyState, prevKeyState, mainCharacter, gameTime);

            if (gameState.CurrentState == TheGameStates.Game)
            {
                mainCharacter.HandleCharacterMovement(gameTime);
                mainEnemy.EnemyMovement(gameTime);
                GameCollision(gameTime);

                if (mainCharacter.TotalHealth == 0)
                {
                    gameState.CurrentState = TheGameStates.End;
                    firstGoal.ActiveGoal = true;
                }
            }

            if (gameState.CurrentState == TheGameStates.Start || gameState.CurrentState == TheGameStates.End)
            {
                firstGoal.ActiveGoal = true;
            }
           

            base.Update(gameTime);
        }

        public void GameCollision(GameTime gameTime)
        {
            if (mainCharacter.ObjectPosX + mainCharacter.ObjectSquare.Width >= GraphicsDevice.Viewport.Width)
            {
                mainCharacter.ObjectPosX = GraphicsDevice.Viewport.Width - mainCharacter.ObjectSquare.Width;
            }

            if (mainCharacter.ObjectPosX + mainCharacter.ObjectSquare.Width <= 0)
            {
                mainCharacter.ObjectPosX = 1;
            }

            if (mainCharacter.ObjectSquare.Intersects(mainEnemy.ObjectSquare))
            {
                mainCharacter.TotalHealth -= 1;
            }

            foreach (Collectible levelCoin in generatedCoins)
            {
                if (levelCoin.ActiveCoin && mainCharacter.ObjectSquare.Intersects(levelCoin.ObjectSquare))
                {
                    levelCoin.ActiveCoin = false;
                    mainCharacter.TotalScore += 50;
                }
            }

            if (mainCharacter.ObjectSquare.Intersects(firstGoal.ObjectSquare))
            {
                firstGoal.ActiveGoal = false;
                firstGoal.ObjectPosX = 27 * 50;
                mainCharacter.ObjectPosY = 250;
                mainCharacter.ObjectPosX = 0;
            }
            else if (mainCharacter.ObjectSquare.Intersects(secondGoal.ObjectSquare))
            {
                secondGoal.ActiveGoal = false;
                secondGoal.ObjectPosX = 27 * 50;
                mainCharacter.ObjectPosX = 0;
                mainCharacter.ObjectPosY = 250;
            }
            else if (mainCharacter.ObjectSquare.Intersects(thirdGoal.ObjectSquare))
            {
                gameState.CurrentState = TheGameStates.End;
            }
            
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
                if (firstGoal.ActiveGoal)
                {
                    firstLevel.Draw(spriteBatch, pos);
                    firstGoal.DrawGoal(spriteBatch);
                    levelCount = 1;
                }
                else if (secondGoal.ActiveGoal)
                {
                    secondLevel.Draw(spriteBatch, pos);
                    secondGoal.DrawGoal(spriteBatch);
                    levelCount = 2;
                }
                else if (thirdGoal.ActiveGoal)
                {
                    thirdLevel.Draw(spriteBatch, pos);
                    thirdGoal.DrawGoal(spriteBatch);
                    levelCount = 3;
                }
          

                mainCharacter.DrawCharacter(spriteBatch);
                mainEnemy.DrawEnemy(spriteBatch);

                foreach (Collectible levelCoin in generatedCoins)
                {
                    if (levelCoin.ActiveCoin)
                    {
                        levelCoin.Draw(spriteBatch, coin);
                    }
                }

                string stats = "Level: " + levelCount + " High Score: " + mainCharacter.TotalScore + " Total Health: " + mainCharacter.TotalHealth;
                spriteBatch.DrawString(gameFont, stats, Vector2.Zero, Color.Black, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0.8f);

                graphics.PreferredBackBufferWidth = 26 * 50;
                graphics.ApplyChanges();
            }

            // Make the background for the menus
            else if (gameState.CurrentState == TheGameStates.Start || gameState.CurrentState == TheGameStates.Pause || gameState.CurrentState == TheGameStates.End)
            {
                spriteBatch.Draw(gameBackground, backgroundRect, Color.White);
                graphics.PreferredBackBufferWidth = 16 * 50;
                graphics.ApplyChanges();
            }

            gameState.DrawState(gameState.CurrentState, gameFont, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
