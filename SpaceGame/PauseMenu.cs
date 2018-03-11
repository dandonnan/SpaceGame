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
    public class PauseMenu
    {
        bool debugMode;

        int selected;
        int maxSelection;

#if DEBUG
        DebugMenu debugMenu;
#endif

        SpriteFont font;
        InputManager inputManager;

        Level level;

        public PauseMenu(ContentManager cm, InputManager im, SaveData sd, Level lvl)
        {
            font = cm.Load<SpriteFont>("scorefont");
            inputManager = im;

            level = lvl;

            maxSelection = 2;
            debugMode = false;

#if DEBUG
            debugMenu = new DebugMenu(font, im, sd, level);
            maxSelection = maxSelection + 1;
#endif
        }

        public int Update()
        {
            if (inputManager.InputUpPressed())
            {
                if (selected == 0)
                    selected = maxSelection - 1;
                else
                    selected--;
            }

            if (inputManager.InputDownPressed())
            {
                if (selected >= maxSelection - 1)
                    selected = 0;
                else
                    selected++;
            }

            if (!debugMode)
            {
                if (inputManager.InputAccept())
                {
                    if (selected == 0)
                        return 1;
                    else if (selected==1)
                    {
                        selected = 0;
                        return 2;
                    }

#if DEBUG
                    if (selected == 2)
                    {
                        selected = 0;
                        debugMode = true;
                    }
#endif
                }
            }
#if DEBUG
            else
            {
                int val = debugMenu.Update();

                if (val == 1)
                {
                    selected = 0;
                    debugMode = false;
                }
            }
#endif

            if (inputManager.InputPause())
                return 1;

            return 0;
        }

        public void Draw(SpriteBatch sb)
        {
            if (!debugMode)
            {
                int yVal = (selected * 30) + 280;
                sb.DrawString(font, "*", new Vector2(590, yVal), Color.White);

                sb.DrawString(font, "Resume", new Vector2(600, 280), Color.White);
                sb.DrawString(font, "Quit", new Vector2(600, 310), Color.White);

#if DEBUG
                sb.DrawString(font, "Debug", new Vector2(600, 340), Color.Yellow);
#endif
            }
#if DEBUG
            else
            {
                debugMenu.Draw(sb);
            }
#endif
        }
    }
}
