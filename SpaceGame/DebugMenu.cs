using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
#if DEBUG
    public class DebugMenu
    {
        // Debug ->
        // Colour Modifiers
        //  * List of colours
        // Bolt Modifiers
        //  * List of bolt types
        // Ship Modifiers
        //  * Ship
        // Card Modifiers
        //  * somehow modify what cards are unlocked

        enum DebugOptions { All, Display, Score, Player, PlayerCol, PlayerBolt, PlayerShip, PlayerCard, Save };
        DebugOptions currentOption;

        int currentSelection;
        int maxSelection;

        SpriteFont font;

        InputManager inputManager;
        SaveData saveData;

        Level level;

        string verification;
        int verificationTimer = 0;

        public DebugMenu(SpriteFont fnt, InputManager im, SaveData sd, Level lvl)
        {
            font = fnt;
            inputManager = im;
            saveData = sd;
            level = lvl;

            currentOption = DebugOptions.All;
            currentSelection = 0;
            maxSelection = 4;
        }

        void verify(string msg, int time)
        {
            verification = msg;
            verificationTimer = time;
        }

        public int Update()
        {
            if (verificationTimer > 0)
                verificationTimer--;

            if (inputManager.InputUpPressed())
            {
                if (currentSelection == 0)
                    currentSelection = maxSelection - 1;
                else
                    currentSelection--;
            }

            if (inputManager.InputDownPressed())
            {
                if (currentSelection >= maxSelection - 1)
                    currentSelection = 0;
                else
                    currentSelection++;
            }

            if (inputManager.InputDecline())
            {
                if (currentOption == DebugOptions.All)
                    return 1;
                else if (currentOption == DebugOptions.PlayerBolt || currentOption == DebugOptions.PlayerCard ||
                    currentOption == DebugOptions.PlayerCol || currentOption == DebugOptions.PlayerShip)
                {
                    maxSelection = 7;
                    currentSelection = 0;
                    currentOption = DebugOptions.Player;
                }
                else
                {
                    maxSelection = 4;
                    currentSelection = 0;
                    currentOption = DebugOptions.All;
                }
            }

            if (inputManager.InputPause())
            {
                currentOption = DebugOptions.All;
                currentSelection = 0;
                return 1;
            }

            switch (currentOption)
            {
                case DebugOptions.All:
                    updateAll();
                    break;

                case DebugOptions.Display:
                    updateDisplay();
                    break;

                case DebugOptions.Score:
                    updateScore();
                    break;

                case DebugOptions.Player:
                    updatePlayer();
                    break;

                case DebugOptions.PlayerCol:
                    updatePlayerColours();
                    break;

                case DebugOptions.PlayerBolt:
                    updatePlayerBolts();
                    break;

                case DebugOptions.PlayerShip:
                    updatePlayerShips();
                    break;

                case DebugOptions.PlayerCard:
                    updatePlayerCards();
                    break;

                case DebugOptions.Save:
                    updateSave();
                    break;
            }

            return 0;
        }

        public void Draw(SpriteBatch sb)
        {
            if (verificationTimer > 0)
                sb.DrawString(font, verification, new Vector2(600, 10), Color.White);

            int yVal;

            switch (currentOption)
            {
                case DebugOptions.All:
                    yVal = (currentSelection * 30) + 270;
                    sb.DrawString(font, "*", new Vector2(590, yVal), Color.White);

                    sb.DrawString(font, "Display", new Vector2(600, 270), Color.White);
                    sb.DrawString(font, "Score", new Vector2(600, 300), Color.White);
                    sb.DrawString(font, "Player", new Vector2(600, 330), Color.White);
                    sb.DrawString(font, "Save", new Vector2(600, 360), Color.White);
                    break;

                case DebugOptions.Display:
                    yVal = (currentSelection * 30) + 310;
                    sb.DrawString(font, "*", new Vector2(590, yVal), Color.White);

                    sb.DrawString(font, "Object Count", new Vector2(600, 310), Color.White);
                    sb.DrawString(font, "Star Count", new Vector2(600, 330), Color.White);
                    break;

                case DebugOptions.Score:
                    yVal = (currentSelection * 30) + 270;
                    sb.DrawString(font, "*", new Vector2(590, yVal), Color.White);

                    sb.DrawString(font, "Increase Score (100)", new Vector2(600, 270), Color.White);
                    sb.DrawString(font, "Increase Score (1000)", new Vector2(600, 300), Color.White);
                    sb.DrawString(font, "Decrease Score (100)", new Vector2(600, 330), Color.White);
                    sb.DrawString(font, "Decrease Score (1000)", new Vector2(600, 360), Color.White);
                    break;

                case DebugOptions.Player:
                    yVal = (currentSelection * 30) + 210;
                    sb.DrawString(font, "*", new Vector2(590, yVal), Color.White);

                    sb.DrawString(font, "Level: <" + saveData.GetPlayerLevel() + ">", new Vector2(600, 210), Color.White);
                    sb.DrawString(font, "Exp: <" + saveData.GetPlayerExp() + ">", new Vector2(600, 240), Color.White);
                    sb.DrawString(font, "Money: <" + saveData.GetMoney() + ">", new Vector2(600, 270), Color.White);
                    sb.DrawString(font, "Colours", new Vector2(600, 300), Color.White);
                    sb.DrawString(font, "Bolts", new Vector2(600, 330), Color.White);
                    sb.DrawString(font, "Ships", new Vector2(600, 360), Color.White);
                    sb.DrawString(font, "Cards", new Vector2(600, 390), Color.White);
                    break;

                case DebugOptions.PlayerCol:
                    yVal = (currentSelection * 30) + 310;
                    sb.DrawString(font, "*", new Vector2(590, yVal), Color.White);

                    sb.DrawString(font, "Colours", new Vector2(600, 310), Color.White);
                    break;

                case DebugOptions.PlayerBolt:
                    yVal = (currentSelection * 30) + 310;
                    sb.DrawString(font, "*", new Vector2(590, yVal), Color.White);

                    sb.DrawString(font, "Bolts", new Vector2(600, 310), Color.White);
                    break;

                case DebugOptions.PlayerShip:
                    yVal = (currentSelection * 30) + 310;
                    sb.DrawString(font, "*", new Vector2(590, yVal), Color.White);

                    sb.DrawString(font, "Ships", new Vector2(600, 310), Color.White);
                    break;

                case DebugOptions.PlayerCard:
                    yVal = (currentSelection * 30) + 310;
                    sb.DrawString(font, "*", new Vector2(590, yVal), Color.White);

                    sb.DrawString(font, "Cards", new Vector2(600, 310), Color.White);
                    break;

                case DebugOptions.Save:
                    yVal = (currentSelection * 30) + 300;
                    sb.DrawString(font, "*", new Vector2(590, yVal), Color.White);

                    sb.DrawString(font, "Force Save", new Vector2(600, 300), Color.White);
                    sb.DrawString(font, "Delete Save", new Vector2(600, 330), Color.White);
                    break;
            }
        }

        #region Updates

        void updateAll()
        {
            if (inputManager.InputAccept())
            {
                if (currentSelection == 0)
                {
                    currentSelection = 0;
                    maxSelection = 2;
                    currentOption = DebugOptions.Display;
                }
                else if (currentSelection == 1)
                {
                    currentSelection = 0;
                    maxSelection = 4;
                    currentOption = DebugOptions.Score;
                }
                else if (currentSelection == 2)
                {
                    currentSelection = 0;
                    maxSelection = 7;
                    currentOption = DebugOptions.Player;
                }
                else if (currentSelection == 3)
                {
                    currentSelection = 0;
                    maxSelection = 2;
                    currentOption = DebugOptions.Save;
                }
            }
        }

        void updateDisplay()
        {
            if (inputManager.InputAccept())
            {
                if (currentSelection == 0)
                    level.ToggleDebugObjectCount();
                else if (currentSelection == 1)
                    level.ToggleDebugStarCount();
            }
        }

        void updateScore()
        {
            if (inputManager.InputAccept())
            {
                if (currentSelection == 0)
                    level.IncrementScore(100);
                else if (currentSelection == 1)
                    level.IncrementScore(1000);
                else if (currentSelection == 2)
                    level.DecrementScore(100);
                else if (currentSelection == 3)
                    level.DecrementScore(1000);
            }
        }

        void updatePlayer()
        {
            if (inputManager.InputAccept())
            {
                if (currentSelection == 3)
                {
                    currentSelection = 0;
                    maxSelection = 1;
                    currentOption = DebugOptions.PlayerCol;
                }
                else if (currentSelection == 4)
                {
                    currentSelection = 0;
                    maxSelection = 1;
                    currentOption = DebugOptions.PlayerBolt;
                }
                else if (currentSelection == 5)
                {
                    currentSelection = 0;
                    maxSelection = 1;
                    currentOption = DebugOptions.PlayerShip;
                }
                else if (currentSelection == 6)
                {
                    currentSelection = 0;
                    maxSelection = 1;
                    currentOption = DebugOptions.PlayerCard;
                }
            }

            if (inputManager.InputLeftPressed())
            {
                if (currentSelection == 0)
                {
                    if (saveData.GetPlayerLevel() > 1)
                        saveData.SetPlayerLevel(saveData.GetPlayerLevel() - 1);
                }
                else if (currentSelection == 1)
                {
                    if (saveData.GetPlayerExp() > 0)
                        saveData.SetPlayerExp(saveData.GetPlayerExp() - 1);
                }
                else if (currentSelection == 2)
                {
                    if (saveData.GetMoney() > 0)
                        saveData.SetMoney(saveData.GetMoney() - 1);
                }
            }

            if (inputManager.InputRightPressed())
            {
                if (currentSelection == 0)
                {
                    if (saveData.GetPlayerLevel() < 100)
                    {
                        saveData.SetPlayerLevel(saveData.GetPlayerLevel() + 1);
                    }
                }
                else if (currentSelection == 1)
                    saveData.SetPlayerExp(saveData.GetPlayerExp() + 1);
                else if (currentSelection == 2)
                    saveData.SetMoney(saveData.GetMoney() + 1);
            }
        }

        void updatePlayerColours()
        {

        }

        void updatePlayerBolts()
        {

        }

        void updatePlayerShips()
        {

        }

        void updatePlayerCards()
        {

        }

        void updateSave()
        {
            if (inputManager.InputAccept())
            {
                if (currentSelection == 0)
                {
                    saveData.Save();
                    verify("Game Saved", 60);
                }
                else if (currentSelection == 1)
                {
                    SaveData.Delete();
                    saveData = new SaveData();
                    saveData.Save();
                    verify("Save deleted", 60);
                }
            }
        }
        #endregion
    }
#endif
}