using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public void DrawCheck(TheGameStates aGameState)
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
