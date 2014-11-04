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
        // Sara Nuffer
        // This class will have all of the information that the Game1 class will need to be able to work with the game states
    
        // This method changes the menus depending on the game state
        public void MenuCheck(TheGameStates currentGameState)
        {
            switch (currentGameState)
            {
                case TheGameStates.Start:

                    break;
                case TheGameStates.Game:

                    break;
                case TheGameStates.End:

                    break;
            }
        }

        // This method changes what the game draws depending on the game state
        public void DrawCheck(TheGameStates aGameState, SpriteFont aSpriteFont) // Also needs the name of the spritefont
        {
            switch (aGameState)
            {
                case TheGameStates.Start:

                    break;
                case TheGameStates.Game:

                    break;
                case TheGameStates.End:

                    break;
            }
        }
    }
}
