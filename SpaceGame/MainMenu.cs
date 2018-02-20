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
        // tabs: Home, Play, Online, Customise

        // HOME:
        // The Journey, Kick Off | The Journey, Product of the Week

        // PLAY:
        // 6 of the bottom right from HOME
        // Kick Off, The Journey, Training | UT, Career, Street

        enum Tabs { Home, Play, Online, Customise }
        Tabs currentTab;

        Texture2D spriteSheet;
        SpriteFont tabFont;
        SpriteFont itemFont;

        InputManager inputManager;

        int playerLevel;
        int playerExp;
        int expToNextLevel;
        float money;
        bool shopNotification;

        public MainMenu(ContentManager cm, InputManager im)
        {
            inputManager = im;

            spriteSheet = cm.Load<Texture2D>("menu");
            tabFont = cm.Load<SpriteFont>("scorefont");
            itemFont = cm.Load<SpriteFont>("menuitem");

            playerLevel = 1;
            playerExp = 0;
            money = 0;
            expToNextLevel = 10;
            shopNotification = true;

            currentTab = Tabs.Home;
        }

        public int Update()
        {
            switch (currentTab)
            {
                case Tabs.Home:
                    if (inputManager.InputRightPressed())
                        currentTab = Tabs.Play;

                    if (inputManager.InputAccept())
                        return 1;
                    break;

                case Tabs.Play:
                    if (inputManager.InputLeftPressed())
                        currentTab = Tabs.Home;

                    if (inputManager.InputAccept())
                        return 1;
                    break;

                case Tabs.Online:
                    break;

                case Tabs.Customise:
                    break;
            }

            return 0;
        }
        
        public void Draw(SpriteBatch sb)
        {
            if (currentTab == Tabs.Play)
                sb.GraphicsDevice.Clear(Color.DarkBlue);
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

                case Tabs.Online:
                    break;

                case Tabs.Customise:
                    break;
            }
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
            sb.DrawString(tabFont, "ONLINE", new Vector2(400, 225), Color.Black);
            sb.DrawString(tabFont, "CUSTOMISE", new Vector2(525, 225), Color.Black);
        }

        void drawHome(SpriteBatch sb)
        {
            sb.Draw(spriteSheet, new Vector2(95, 221), new Rectangle(9, 7, 149, 35), Color.White);
            sb.DrawString(tabFont, "HOME", new Vector2(150, 225), Color.Purple);

            sb.Draw(spriteSheet, new Vector2(95, 275), new Rectangle(10, 98, 253, 352), Color.White);
            sb.DrawString(itemFont, "THE JOURNEY", new Vector2(150, 290), Color.White);
            sb.Draw(spriteSheet, new Vector2(360, 275), new Rectangle(268, 98, 253, 352), Color.White);

            sb.Draw(spriteSheet, new Vector2(630, 275), new Rectangle(536, 233, 454, 128), Color.White);
            sb.Draw(spriteSheet, new Vector2(630, 420), new Rectangle(14, 462, 456, 215), Color.White);

            sb.Draw(spriteSheet, new Vector2(908, 645), new Rectangle(908, 632, 322, 63), Color.White);
        }

        void drawPlay(SpriteBatch sb)
        {
            sb.Draw(spriteSheet, new Vector2(225, 221), new Rectangle(9, 7, 149, 35), Color.White);
            sb.DrawString(tabFont, "PLAY", new Vector2(275, 225), Color.Purple);

            sb.Draw(spriteSheet, new Vector2(100, 275), new Rectangle(535, 101, 454, 128), Color.White);
            sb.DrawString(itemFont, "THE JOURNEY", new Vector2(400, 375), Color.White);
            sb.Draw(spriteSheet, new Vector2(100, 410), new Rectangle(536, 233, 454, 128), Color.White);
            sb.Draw(spriteSheet, new Vector2(100, 545), new Rectangle(536, 233, 454, 128), Color.White);

            sb.Draw(spriteSheet, new Vector2(630, 275), new Rectangle(536, 233, 454, 128), Color.White);
            sb.Draw(spriteSheet, new Vector2(630, 410), new Rectangle(536, 233, 454, 128), Color.White);
            sb.Draw(spriteSheet, new Vector2(630, 545), new Rectangle(536, 233, 454, 128), Color.White);
        }
    }
}
