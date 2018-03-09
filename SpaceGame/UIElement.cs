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
        enum States { NotSelected, Selected };
        States currentState;

        Rectangle textureOffsetNotSel;
        Rectangle textureOffsetSel;
        MousePointer mouse;

        bool differingTextures;

        List<TextElement> textElements;

        // Need to position this relative to the TextElement rather than world space (although an option
        // for world space might be useful too)
        public UIElement(Texture2D tex, Vector2 pos, Rectangle texOffset, MousePointer mp)
        {
            currentState = States.NotSelected;

            texture = tex;
            position = pos;
            mouse = mp;
            textureOffsetNotSel = texOffset;

            width = texOffset.Width;
            height = texOffset.Height;

            differingTextures = false;

            textElements = new List<TextElement>();

            updateCollision();
        }

        public UIElement(Texture2D tex, Vector2 pos, Rectangle texOffsetNS, Rectangle texOffsetS, MousePointer mp)
        {
            currentState = States.NotSelected;

            texture = tex;
            position = pos;
            mouse = mp;
            textureOffsetNotSel = texOffsetNS;
            textureOffsetSel = texOffsetS;

            width = texOffsetNS.Width;
            height = texOffsetNS.Height;

            differingTextures = true;

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

        public void UpdateTextElement(int id, string text)
        {
            textElements[id].UpdateText(text);
        }

        public void UpdateTextElement(int id, string text, Vector2 pos)
        {
            textElements[id].UpdateText(text, pos);
        }

        public void SetSelected() { currentState = States.Selected; }
        public void SetDeslected() { currentState = States.NotSelected; }

        public void SetSelected(bool sel)
        {
            if (sel)
                currentState = States.Selected;
            else
                currentState = States.NotSelected;
        }

        public bool IsSelected()
        {
            if (currentState == States.Selected)
                return true;

            return false;
        }

        public bool IsIntersected()
        {
            if (collision.Intersects(new Rectangle((int)mouse.GetX(), (int)mouse.GetY(), 1, 1)))
                return true;

            return false;
        }

        public bool IsClicked()
        {
            if (mouse.Clicked())
            {
                if (IsIntersected())
                    return true;
            }

            return false;
        }

        public override void Update()
        {
            switch (currentState)
            {
                case States.NotSelected:
                    if (IsIntersected())
                        currentState = States.Selected;
                    break;

                case States.Selected:
                    if (!IsIntersected())
                        currentState = States.NotSelected;
                    break;
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            switch (currentState)
            {
                case States.NotSelected:
                    if (texture != null)
                        sb.Draw(texture, position, textureOffsetNotSel, Color.White);

                    if (textElements.Count > 0)
                    {
                        foreach (TextElement t in textElements)
                            t.Draw(sb);
                    }
                    break;

                case States.Selected:
                    if (texture != null)
                    {
                        if (differingTextures)
                            sb.Draw(texture, position, textureOffsetSel, Color.White);
                        else
                            sb.Draw(texture, position, textureOffsetNotSel, Color.White);
                    }

                    if (textElements.Count > 0)
                    {
                        foreach (TextElement t in textElements)
                            t.Draw(sb);
                    }
                    break;
            }
        }
    }
}
