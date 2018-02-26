using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    public class MainMenu
    {
        // top bar: Level, Exp/Exp, Money, Shop (notification)

        // back button / esc: Do you want to quit? YES | NO

        enum Tabs { Home, Play, Shop, Customise }
        Tabs currentTab;

        Texture2D spriteSheet;
        SpriteFont tabFont;
        SpriteFont itemFont;

        InputManager inputManager;
        SaveData saveData;

        Shop shop;

        int currentSelection;

        int playerLevel;
        int playerExp;
        int expToNextLevel;
        float money;
        bool shopNotification;

        bool leveledUp;
        bool quitCheck;

        public MainMenu(ContentManager cm, InputManager im, SaveData sd)
        {
            inputManager = im;
            saveData = sd;

            shop = new Shop(sd);

            spriteSheet = cm.Load<Texture2D>("menu");
            tabFont = cm.Load<SpriteFont>("scorefont");
            itemFont = cm.Load<SpriteFont>("menuitem");

            currentSelection = 0;
            quitCheck = false;

            Refresh();
            shopNotification = true;

            currentTab = Tabs.Home;
        }

        public void Refresh()
        {
            leveledUp = false;

            playerLevel = saveData.GetPlayerLevel();
            playerExp = saveData.GetPlayerExp();
            expToNextLevel = saveData.GetExpToNextLevel();
            money = saveData.GetMoney();

            if (playerExp >= expToNextLevel)
            {
                while (playerExp >= expToNextLevel && playerLevel<100)
                {
                    playerExp -= expToNextLevel;
                    playerLevel++;

                    saveData.SetPlayerLevel(playerLevel);
                    saveData.SetPlayerExp(playerExp);

                    leveledUp = true;

                    expToNextLevel = saveData.GetExpToNextLevel();
                }

                if (playerLevel == 100 && playerExp > 0)
                {
                    playerExp = 0;
                    saveData.SetPlayerExp(0);
                }

                saveData.Save();
            }
        }

        public int Update()
        {
            int returnValue = 0;

            if (!quitCheck && !leveledUp)
            {
                switch (currentTab)
                {
                    case Tabs.Home:
                        returnValue = updateHome();
                        break;

                    case Tabs.Play:
                        returnValue = updatePlay();
                        break;

                    case Tabs.Shop:
                        shop.Update();

                        if (inputManager.InputLeftPressed())
                            currentTab = Tabs.Play;

                        if (inputManager.InputRightPressed())
                            currentTab = Tabs.Customise;
                        break;

                    case Tabs.Customise:
                        if (inputManager.InputLeftPressed())
                            currentTab = Tabs.Shop;
                        break;
                }

                if (inputManager.InputQuit())
                    quitCheck = true;
            }
            else if (quitCheck)
            {
                if (inputManager.InputAccept())
                    returnValue = 6;

                if (inputManager.InputDecline())
                    quitCheck = false;
            }
            else if (leveledUp)
            {
                if (inputManager.InputAccept() || inputManager.InputDecline())
                    leveledUp = false;
            }

            return returnValue;
        }
        
        public void Draw(SpriteBatch sb)
        {
            if (currentTab == Tabs.Play)
                sb.GraphicsDevice.Clear(Color.DarkBlue);
            else if (currentTab == Tabs.Shop)
                sb.GraphicsDevice.Clear(Color.Black);
            else
                sb.GraphicsDevice.Clear(Color.Coral);

            drawTabBar(sb);
            drawStatBar(sb);

            switch (currentTab)
            {
                case Tabs.Home:
                    drawHome(sb);
                    break;

                case Tabs.Play:
                    drawPlay(sb);
                    break;

                case Tabs.Shop:
                    drawShop(sb);
                    break;

                case Tabs.Customise:
                    drawCustomise(sb);
                    break;
            }

            if (quitCheck)
            {
                sb.Draw(spriteSheet, new Vector2(490, 285), new Rectangle(479, 503, 300, 150), Color.White);
                sb.DrawString(tabFont, "Are you sure you want to quit?", new Vector2(530, 295), Color.White);
                sb.Draw(spriteSheet, new Vector2(500, 375), new Rectangle(585, 45, 32, 32), Color.White);
                sb.DrawString(tabFont, "Yes", new Vector2(540, 380), Color.White);
                sb.Draw(spriteSheet, new Vector2(675, 375), new Rectangle(618, 45, 32, 32), Color.White);
                sb.DrawString(tabFont, "No", new Vector2(712, 380), Color.White);
            }

            if (leveledUp)
            {
                sb.Draw(spriteSheet, new Vector2(490, 285), new Rectangle(479, 503, 300, 150), Color.White);
                sb.DrawString(tabFont, "You've Levelled Up!", new Vector2(530, 295), Color.White);
                sb.Draw(spriteSheet, new Vector2(580, 318), new Rectangle(1003, 120, 122, 109), Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
                sb.Draw(spriteSheet, new Vector2(580, 316), new Rectangle(1003, 49, 121, 37), Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
                sb.DrawString(tabFont, "A new Reward awaits in the Shop.", new Vector2(530, 400), Color.White);
            }
        }

        int updateHome()
        {
            if (inputManager.InputRightPressed())
            {
                if (currentSelection < 2)
                    currentSelection++;
                else
                {
                    currentSelection = 0;
                    currentTab = Tabs.Play;
                }
            }

            if (inputManager.InputLeftPressed())
            {
                if (currentSelection > 1)
                    currentSelection = 1;
                else if (currentSelection == 1)
                    currentSelection = 0;
            }

            if (inputManager.InputDownPressed() || inputManager.InputUpPressed())
            {
                if (currentSelection == 2)
                    currentSelection = 3;
                else if (currentSelection == 3)
                    currentSelection = 2;
            }

            if (inputManager.InputAccept())
            {
                if (currentSelection == 0)
                    return 2;
                else if (currentSelection == 1)
                    return 1;
                else if (currentSelection == 2)
                    return 3;
                else if (currentSelection == 3)
                    return 4;
            }

            return 0;
        }

        int updatePlay()
        {
            if (inputManager.InputRightPressed())
            {
                if (currentSelection < 2)
                    currentSelection+=2;
                else
                {
                    currentSelection = 0;
                    currentTab = Tabs.Shop;
                }
            }

            if (inputManager.InputLeftPressed())
            {
                if (currentSelection > 1)
                    currentSelection-=2;
                else
                {
                    currentSelection = 0;
                    currentTab = Tabs.Home;
                }
            }

            if (inputManager.InputDownPressed() || inputManager.InputUpPressed())
            {
                if (currentSelection == 0 || currentSelection == 2)
                    currentSelection++;
                else
                    currentSelection--;
            }

            if (inputManager.InputAccept())
            {
                if (currentSelection == 0)
                    return 1;
                else if (currentSelection == 1)
                    return 2;
                else if (currentSelection == 2)
                    return 3;
                else if (currentSelection == 3)
                    return 5;
            }

            return 0;
        }

        void drawStatBar(SpriteBatch sb)
        {
            sb.Draw(spriteSheet, new Vector2(859, 58), new Rectangle(206, 13, 327, 26), Color.White);
            sb.DrawString(tabFont, "LVL: " + playerLevel, new Vector2(890, 62), Color.Black);
            sb.DrawString(tabFont, playerExp + "/" + expToNextLevel, new Vector2(990, 62), Color.Black);
            sb.DrawString(tabFont, "" + money, new Vector2(1075, 62), Color.Black);
        }

        void drawTabBar(SpriteBatch sb)
        {
            sb.Draw(spriteSheet, new Vector2(94, 221), new Rectangle(8, 53, 561, 35), Color.White);
            sb.DrawString(tabFont, "HOME", new Vector2(150, 225), Color.Black);
            sb.DrawString(tabFont, "PLAY", new Vector2(275, 225), Color.Black);
            sb.DrawString(tabFont, "SHOP", new Vector2(400, 225), Color.Black);
            sb.DrawString(tabFont, "CUSTOMISE", new Vector2(525, 225), Color.Black);
        }

        void drawHome(SpriteBatch sb)
        {
            sb.Draw(spriteSheet, new Vector2(95, 221), new Rectangle(9, 7, 149, 35), Color.White);
            sb.DrawString(tabFont, "HOME", new Vector2(150, 225), Color.Purple);

            sb.Draw(spriteSheet, new Vector2(95, 275), new Rectangle(10, 98, 253, 352), Color.White);
            sb.DrawString(itemFont, "THE ODYSSEY", new Vector2(150, 290), Color.White);
            sb.Draw(spriteSheet, new Vector2(360, 275), new Rectangle(268, 98, 253, 352), Color.White);
            sb.DrawString(itemFont, "INFINITY", new Vector2(415, 290), Color.White);

            sb.Draw(spriteSheet, new Vector2(630, 275), new Rectangle(536, 233, 454, 128), Color.White);
            sb.DrawString(itemFont, "ULTIMATE CREW", new Vector2(640, 290), Color.White);
            sb.Draw(spriteSheet, new Vector2(630, 420), new Rectangle(14, 462, 456, 215), Color.White);
            sb.DrawString(itemFont, "PRODUCT OF THE WEEK", new Vector2(640, 440), Color.White);

            sb.Draw(spriteSheet, new Vector2(850, 470), new Rectangle(266, 701, 91, 126), Color.White);

            sb.Draw(spriteSheet, new Vector2(908, 645), new Rectangle(908, 632, 322, 63), Color.White);

            sb.Draw(spriteSheet, new Vector2(100, 640), new Rectangle(585, 45, 32, 32), Color.White);
            string drawSelected = "";
            if (currentSelection == 0) { drawSelected = "The Odyssey"; }
            else if (currentSelection == 1) { drawSelected = "Infinity"; }
            else if (currentSelection == 2) { drawSelected = "Ultimate Crew"; }
            else { drawSelected = "Product of the Week"; }
            sb.DrawString(tabFont, "Select " + drawSelected, new Vector2(140, 647), Color.White);
        }

        void drawPlay(SpriteBatch sb)
        {
            sb.Draw(spriteSheet, new Vector2(220, 221), new Rectangle(9, 7, 149, 35), Color.White);
            sb.DrawString(tabFont, "PLAY", new Vector2(275, 225), Color.Purple);

            sb.Draw(spriteSheet, new Vector2(100, 275), new Rectangle(536, 233, 454, 128), Color.White);
            sb.DrawString(itemFont, "INFINITY", new Vector2(400, 375), Color.White);
            sb.Draw(spriteSheet, new Vector2(100, 410), new Rectangle(535, 101, 454, 128), Color.White);
            sb.DrawString(itemFont, "THE ODYSSEY", new Vector2(400, 510), Color.White);

            sb.Draw(spriteSheet, new Vector2(630, 275), new Rectangle(536, 233, 454, 128), Color.White);
            sb.DrawString(itemFont, "ULTIMATE CREW", new Vector2(900, 375), Color.White);
            sb.Draw(spriteSheet, new Vector2(630, 410), new Rectangle(536, 233, 454, 128), Color.White);
            sb.DrawString(itemFont, "TRAINING", new Vector2(930, 510), Color.White);

            sb.Draw(spriteSheet, new Vector2(100, 640), new Rectangle(585, 45, 32, 32), Color.White);
            string drawSelected = "";
            if (currentSelection == 0) { drawSelected = "Infinity"; }
            else if (currentSelection == 1) { drawSelected = "The Odyssey"; }
            else if (currentSelection == 2) { drawSelected = "Ultimate Crew"; }
            else { drawSelected = "Training"; }
            sb.DrawString(tabFont, "Select " + drawSelected, new Vector2(140, 647), Color.White);
        }

        void drawShop(SpriteBatch sb)
        {
            sb.Draw(spriteSheet, new Vector2(357, 221), new Rectangle(9, 7, 149, 35), Color.White);
            sb.DrawString(tabFont, "SHOP", new Vector2(400, 225), Color.Purple);

            sb.Draw(spriteSheet, new Vector2(100, 640), new Rectangle(585, 11, 32, 32), Color.White);
            //string drawSelected = "";
            //sb.DrawString(tabFont, "Select " + drawSelected, new Vector2(140, 647), Color.White);

            shop.Draw(sb);
        }

        void drawCustomise(SpriteBatch sb)
        {
            sb.Draw(spriteSheet, new Vector2(506, 221), new Rectangle(9, 7, 149, 35), Color.White);
            sb.DrawString(tabFont, "CUSTOMISE", new Vector2(525, 225), Color.Purple);

            sb.Draw(spriteSheet, new Vector2(100, 640), new Rectangle(585, 45, 32, 32), Color.White);
            //string drawSelected = "";
            //sb.DrawString(tabFont, "Select " + drawSelected, new Vector2(140, 647), Color.White);
        }
    }
}
