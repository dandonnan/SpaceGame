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
    public class TitleScreen
    {
        Texture2D background;
        Texture2D logo;

        SpriteFont font;

        InputManager inputManager;

        public TitleScreen(ContentManager cm, InputManager im)
        {
            //background = cm.Load<Texture2D>("");
            //logo = cm.Load<Texture2D>("");

            font = cm.Load<SpriteFont>("scorefont");

            inputManager = im;
        }

        public int Update()
        {
            if (inputManager.InputAccept())
                return 1;

            return 0;
        }

        public void Draw(SpriteBatch sb)
        {
            //sb.Draw(background, Vector2.Zero, Color.White);
            //sb.Draw(logo, new Vector2(0, 0), Color.White);

            sb.DrawString(font, "Press Start (or something)", new Vector2(550, 600), Color.White);
        }
    }
}
