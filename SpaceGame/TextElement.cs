﻿using Microsoft.Xna.Framework;
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

        public TextElement(string str, Vector2 pos, SpriteFont fnt)
        {
            position = pos;
            text = str;
            font = fnt;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.DrawString(font, text, position, Color.White);
        }
    }
}