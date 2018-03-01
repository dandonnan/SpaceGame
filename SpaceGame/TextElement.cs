using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    public class TextElement
    {
        Vector2 position;
        string text;
        SpriteFont font;
        Color colour;

        public TextElement(string str, Vector2 pos, SpriteFont fnt)
        {
            position = pos;
            text = str;
            font = fnt;
            colour = Color.White;
        }

        public TextElement(string str, Vector2 pos, SpriteFont fnt, Color clr)
        {
            position = pos;
            text = str;
            font = fnt;
            colour = clr;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.DrawString(font, text, position, colour);
        }
    }
}
