using Microsoft.Xna.Framework.Content;
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
        SaveData saveData;

        public GameManager(ContentManager cm)
        {
            contentManager = cm;
            inputManager = new InputManager();
            saveData = SaveData.Load();

            currentMode = Modes.Splash;

            splashScreen = new SplashScreen(contentManager);
            mainMenu = new MainMenu(contentManager, inputManager, saveData);
            level = new Level(contentManager, inputManager, saveData);
        }

        public void Update()
        {
            inputManager.Update();

            int val = 0;

            switch (currentMode)
            {
                case Modes.Splash:
                    splashScreen.Update();

                    if (splashScreen.IsFinished())
                        currentMode = Modes.Menu;
                    break;

                case Modes.Menu:
                    val = mainMenu.Update();

                    if (val == 1)
                        currentMode = Modes.Game;
                    break;

                case Modes.Game:
                    val = level.Update();

                    if (val == 1)
                    {
                        mainMenu.Refresh();
                        currentMode = Modes.Menu;
                    }
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
