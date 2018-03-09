using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    public class PauseMenu
    {
        bool debugMode;

        // Resume
        // Quit

        DebugMenu debugMenu;

        InputManager inputManager;

        public PauseMenu(ContentManager cm, InputManager im, SaveData sd)
        {
            inputManager = im;
        }

        public int Update()
        {
            if (!debugMode)
            {
                if (inputManager.InputPause())
                    return 1;
            }
            else
            {
                debugMenu.Update();
            }

            return 0;
        }

        public void Draw(SpriteBatch sb)
        {
        }
    }

    public class DebugMenu
    {
        // Debug ->
        // Display
        //  * Object Count
        // Score Modifiers
        //  * Increase Score (100)
        //  * Increase Score (1000)
        //  * Decrease Score (100)
        //  * Decrease Score (1000)
        // Player Modifiers
        //  * Level
        //  * EXP
        //  * Money
        // Colour Modifiers
        //  * List of colours
        // Bolt Modifiers
        //  * List of bolt types
        // Ship Modifiers
        //  * Ship
        // Card Modifiers
        //  * somehow modify what cards are unlocked
        // Save Modifiers
        //  * Force Save
        //  * Delete Save

        enum DebugOptions { Display, Score, Player, PlayerCol, PlayerBolt, PlayerShip, PlayerCard, Save };
        DebugOptions currentOption;

        int currentSelection;
        int maxSelection;

        public DebugMenu()
        {
            currentOption = DebugOptions.Display;
            currentSelection = 0;
            maxSelection = 3;
        }

        public int Update()
        {
            return 0;
        }

        public void Draw(SpriteBatch sb)
        {
        }
    }
}
