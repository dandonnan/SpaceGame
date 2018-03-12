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
    public class CustomiseMenu
    {
        enum Screens { Menu, Colours, Bolts, Ships };
        Screens currentScreen;

        int currentSelection;

        SaveData saveData;
        ContentManager contentManager;
        InputManager inputManager;

        Texture2D spriteSheet;
        SpriteFont tabFont;
        SpriteFont itemFont;

        UIElement customColour;
        UIElement customBolt;
        UIElement customShip;

        public CustomiseMenu(ContentManager cm, InputManager im, SaveData sd, MousePointer mp)
        {
            saveData = sd;
            contentManager = cm;
            inputManager = im;

            currentSelection = 0;
            currentScreen = Screens.Menu;

            spriteSheet = cm.Load<Texture2D>("menu");
            tabFont = cm.Load<SpriteFont>("scorefont");
            itemFont = cm.Load<SpriteFont>("menuitem");

            customColour = new UIElement(spriteSheet, new Vector2(100, 275), new Rectangle(480, 660, 192, 192), mp);
            customBolt = new UIElement(spriteSheet, new Vector2(320, 275), new Rectangle(480, 660, 192, 192), mp);
            customShip = new UIElement(spriteSheet, new Vector2(540, 275), new Rectangle(480, 660, 192, 192), mp);

            customColour.AddTextElement(new TextElement("COLOURS", new Vector2(110, 350), itemFont));
            customBolt.AddTextElement(new TextElement("BOLTS", new Vector2(325, 350), itemFont));
            customShip.AddTextElement(new TextElement("SHIPS", new Vector2(560, 350), itemFont));
        }

        public int Update()
        {
            if (inputManager.InputLeftPressed())
            {
                if (currentSelection == 0)
                    return 1;
                else
                    currentSelection--;
            }

            if (inputManager.InputRightPressed())
            {
                if (currentSelection < 2)
                    currentSelection++;
            }

            if (inputManager.InputAccept())
            {
            }

            return 0;
        }

        public void Draw(SpriteBatch sb)
        {
            customColour.Draw(sb);
            customBolt.Draw(sb);
            customShip.Draw(sb);

            string drawSelected = "";
            if (currentSelection == 0) { drawSelected = "Customise Colours"; }
            else if (currentSelection == 1) { drawSelected = "Customise Bolts"; }
            else { drawSelected = "Customise Ships"; }
            sb.DrawString(tabFont, "Select " + drawSelected, new Vector2(140, 647), Color.White);
        }
    }
}