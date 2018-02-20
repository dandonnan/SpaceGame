﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    class GameManager
    {
        enum Modes { Splash, Menu, Game }
        Modes currentMode;

        ContentManager contentManager;
        InputManager inputManager;

        SplashScreen splashScreen;
        MainMenu mainMenu;
        Level level;

        public GameManager(ContentManager cm)
        {
            contentManager = cm;
            inputManager = new InputManager();

            currentMode = Modes.Splash;

            splashScreen = new SplashScreen(contentManager);
            mainMenu = new MainMenu(contentManager, inputManager);
            level = new Level(contentManager, inputManager);
        }

        public void Update()
        {
            inputManager.Update();

            switch (currentMode)
            {
                case Modes.Splash:
                    splashScreen.Update();

                    if (splashScreen.IsFinished())
                        currentMode = Modes.Menu;
                    break;

                case Modes.Menu:
                    int val = mainMenu.Update();

                    if (val == 1)
                        currentMode = Modes.Game;
                    break;

                case Modes.Game:
                    level.Update();
                    break;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            switch (currentMode)
            {
                case Modes.Splash:
                    splashScreen.Draw(sb);
                    break;

                case Modes.Menu:
                    mainMenu.Draw(sb);
                    break;

                case Modes.Game:
                    level.Draw(sb);
                    break;
            }
        }
    }
}