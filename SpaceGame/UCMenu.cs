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
    public class UCMenu
    {
        Texture2D textureSheet;
        SpriteFont debugFont;

        int optionSelected;

        InputManager inputManager;
        SaveData saveData;

        public UCMenu(ContentManager cm, InputManager im, SaveData sd)
        {
            textureSheet = cm.Load<Texture2D>("menu");
            debugFont = cm.Load<SpriteFont>("scoreFont");

            optionSelected = 0;

            inputManager = im;
            saveData = sd;
        }

        public int Update()
        {
            if (inputManager.InputDecline())
            {
                optionSelected = 0;
                return 1;
            }

            if (inputManager.InputRightPressed())
            {
                if (optionSelected == 0)
                    optionSelected = 6;
                else if (optionSelected == 1)
                    optionSelected = 2;
                else if (optionSelected == 2)
                    optionSelected = 6;
                else if (optionSelected == 3 || optionSelected == 4 || optionSelected==5)
                    optionSelected++;
                else if (optionSelected == 6)
                    optionSelected = 1;
            }

            if (inputManager.InputLeftPressed())
            {
                if (optionSelected == 0)
                    optionSelected = 6;
                else if (optionSelected == 1)
                    optionSelected = 6;
                else if (optionSelected == 2)
                    optionSelected = 1;
                else if (optionSelected == 4 || optionSelected == 5)
                    optionSelected--;
                else if (optionSelected == 3)
                    optionSelected = 6;
                else if (optionSelected == 6)
                    optionSelected = 2;
            }

            if (inputManager.InputUpPressed())
            {
                if (optionSelected == 0)
                    optionSelected = 3;
                else if (optionSelected == 1 || optionSelected == 2)
                    optionSelected = 0;
                else if (optionSelected == 3 || optionSelected == 4 || optionSelected == 5)
                    optionSelected = 2;
            }

            if (inputManager.InputDownPressed())
            {
                if (optionSelected == 0)
                    optionSelected = 1;
                else if (optionSelected == 1)
                    optionSelected = 3;
                else if (optionSelected == 2)
                    optionSelected = 5;
                else if (optionSelected == 3 || optionSelected == 4 || optionSelected == 5)
                    optionSelected = 0;
            }

            return 0;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(textureSheet, new Vector2(250, 10), new Rectangle(1152, 28, 128, 160), Color.White);    // Ship
            sb.Draw(textureSheet, new Vector2(50, 150), new Rectangle(1152, 28, 128, 160), Color.White);    // Weapon1
            sb.Draw(textureSheet, new Vector2(450, 150), new Rectangle(1152, 28, 128, 160), Color.White);   // Weapon2
            sb.Draw(textureSheet, new Vector2(50, 350), new Rectangle(1152, 28, 128, 160), Color.White);    // Captain
            sb.Draw(textureSheet, new Vector2(250, 350), new Rectangle(1152, 28, 128, 160), Color.White);   // Pilot
            sb.Draw(textureSheet, new Vector2(450, 350), new Rectangle(1152, 28, 128, 160), Color.White);   // Engineer

            sb.Draw(textureSheet, new Vector2(1000, 75), new Rectangle(1152, 28, 128, 160), Color.White);   // Deck

            string sel = "";

            if (optionSelected == 0) { sel = "Ship"; sb.Draw(textureSheet, new Vector2(300, 100), new Rectangle(486, 1, 10, 10), Color.White); }
            if (optionSelected == 1) { sel = "Weapon 1"; sb.Draw(textureSheet, new Vector2(100, 240), new Rectangle(486, 1, 10, 10), Color.White); }
            if (optionSelected == 2) { sel = "Weapon 2"; sb.Draw(textureSheet, new Vector2(500, 240), new Rectangle(486, 1, 10, 10), Color.White); }
            if (optionSelected == 3) { sel = "Captain"; sb.Draw(textureSheet, new Vector2(100, 440), new Rectangle(486, 1, 10, 10), Color.White); }
            if (optionSelected == 4) { sel = "Pilot"; sb.Draw(textureSheet, new Vector2(300, 440), new Rectangle(486, 1, 10, 10), Color.White); }
            if (optionSelected == 5) { sel = "Engineer"; sb.Draw(textureSheet, new Vector2(500, 440), new Rectangle(486, 1, 10, 10), Color.White); }
            if (optionSelected == 6) { sel = "Deck"; sb.Draw(textureSheet, new Vector2(1050, 165), new Rectangle(486, 1, 10, 10), Color.White); }
            
            sb.DrawString(debugFont, sel, new Vector2(50, 650), Color.White);
        }
    }
}
