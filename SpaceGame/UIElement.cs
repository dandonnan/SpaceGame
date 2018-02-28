using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    public class UIElement : GameObject
    {
        Rectangle textureOffset;
        MousePointer mouse;

        List<TextElement> textElements;

        public UIElement(Texture2D tex, Vector2 pos, Rectangle texOffset, MousePointer mp)
        {
            texture = tex;
            position = pos;
            mouse = mp;
            textureOffset = texOffset;

            width = texOffset.Width;
            height = texOffset.Height;

            textElements = new List<TextElement>();

            updateCollision();
        }

        public void AddTextElement(TextElement te)
        {
            textElements.Add(te);
        }

        public void AddTextElement(string text, Vector2 pos, SpriteFont font)
        {
            textElements.Add(new TextElement(text, pos, font));
        }

        public bool IsClicked()
        {
            if (mouse.Clicked())
            {
                if (collision.Intersects(new Rectangle((int)mouse.GetX(), (int)mouse.GetY(), 1, 1)))
                    return true;
            }

            return false;
        }

        public override void Update()
        {
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, textureOffset, Color.White);

            if (textElements.Count > 0)
            {
                foreach (TextElement t in textElements)
                    t.Draw(sb);
            }
        }
    }
}
