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
    public class LootboxReveal
    {
        Texture2D spriteSheet;

        SpriteFont tempFont;

        InputManager inputManager;

        public LootboxReveal(ContentManager cm, InputManager im)
        {
            spriteSheet = cm.Load<Texture2D>("menu");
            tempFont = cm.Load<SpriteFont>("menuitem");

            inputManager = im;

            // figure out what is already unlocked

            // cards can be random
            // colours & ships can only be unlocked once
        }

        public LootboxReveal(ContentManager cm, InputManager im, Card card)
        {
            spriteSheet = cm.Load<Texture2D>("menu");
            tempFont = cm.Load<SpriteFont>("");

            inputManager = im;

            // reveal a fixed card
        }

        public LootboxReveal(ContentManager cm, InputManager im, Color clr)
        {
            spriteSheet = cm.Load<Texture2D>("menu");
            tempFont = cm.Load<SpriteFont>("");

            inputManager = im;

            // reveal a fixed colour
        }

        public int Update()
        {
            if (inputManager.InputAccept())
                return 1;

            return 0;
        }

        public void Draw(SpriteBatch sb)
        {
            //sb.Draw(spriteSheet, new Vector2(0, 0), new Rectangle(0, 0, 0, 0), Color.White);
            sb.DrawString(tempFont, "Unlocked ITEM", new Vector2(0, 0), Color.White);
        }
    }
}
