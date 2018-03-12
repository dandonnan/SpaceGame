using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    public class GameManager
    {
        // All the modes the game can be in (Splash screen, Title screen, Menu screen, Ultimate Crew, Main game)
        enum Modes { Splash, Title, Menu, Crew, Game }
        Modes currentMode;

        ContentManager contentManager;
        InputManager inputManager;
        AudioManager audioManager;

        SplashScreen splashScreen;
        TitleScreen titleScreen;
        MainMenu mainMenu;
        UCMenu ucMenu;
        Level level;

        MousePointer pointer;

        SaveData saveData;

        /// <summary>
        /// Create a GameManager object
        /// </summary>
        /// <param name="cm">The ContentManager that will be used to load content</param>
        public GameManager(ContentManager cm)
        {
            contentManager = cm;
            inputManager = new InputManager();
            audioManager = new AudioManager();
            saveData = SaveData.Load();

            currentMode = Modes.Menu;

            pointer = new MousePointer(contentManager, inputManager);

            splashScreen = new SplashScreen(contentManager, audioManager);
            titleScreen = new TitleScreen(contentManager, inputManager);
            mainMenu = new MainMenu(contentManager, inputManager, saveData, pointer);
            ucMenu = new UCMenu(contentManager, inputManager, saveData);
            level = new Level(contentManager, inputManager, saveData);
        }

        /// <summary>
        /// The main update loop for all game logic
        /// </summary>
        /// <returns>Will close the game if 1</returns>
        public int Update()
        {
            inputManager.Update();
            //pointer.Update();

            int val = 0;

            switch (currentMode)
            {
                case Modes.Splash:
                    splashScreen.Update();

                    if (splashScreen.IsFinished())
                        currentMode = Modes.Title;
                    break;

                case Modes.Title:
                    val = titleScreen.Update();

                    if (val == 1)
                        currentMode = Modes.Menu;
                    break;

                case Modes.Menu:
                    val = mainMenu.Update();

                    if (val == 1)
                        currentMode = Modes.Game;
                    else if (val == 3)
                        currentMode = Modes.Crew;
                    else if (val == 6)
                        return 1;
                    break;

                case Modes.Crew:
                    val = ucMenu.Update();

                    if (val == 1)
                    {
                        mainMenu.Refresh();
                        currentMode = Modes.Menu;
                    }
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

            return 0;
        }

        /// <summary>
        /// The main render code for the game
        /// </summary>
        /// <param name="sb">Name of the SpriteBatch to render with</param>
        public void Draw(SpriteBatch sb)
        {
            switch (currentMode)
            {
                case Modes.Splash:
                    splashScreen.Draw(sb);
                    break;

                case Modes.Title:
                    titleScreen.Draw(sb);
                    break;

                case Modes.Menu:
                    mainMenu.Draw(sb);
                    break;

                case Modes.Crew:
                    ucMenu.Draw(sb);
                    break;

                case Modes.Game:
                    level.Draw(sb);
                    break;
            }

            //pointer.Draw(sb);
        }
    }
}
