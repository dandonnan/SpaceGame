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

        SplashScreen splashScreen;
        // Menu
        Level level;

        public GameManager(ContentManager cm)
        {
            contentManager = cm;

            currentMode = Modes.Splash;

            splashScreen = new SplashScreen(contentManager);
            level = new Level(contentManager);
        }

        public void Update()
        {
            switch (currentMode)
            {
                case Modes.Splash:
                    splashScreen.Update();

                    if (splashScreen.IsFinished())
                        currentMode = Modes.Game;
                    break;

                case Modes.Menu:
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
                    break;

                case Modes.Game:
                    level.Draw(sb);
                    break;
            }
        }
    }
}
