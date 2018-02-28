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
    public class MousePointer
    {
        Texture2D texture;
        Vector2 mousePos;
        Vector2 lastMousePos;

        bool idle;
        float timer;

        float alpha;

        InputManager inputManager;

        public MousePointer(ContentManager cm, InputManager im)
        {
            texture = cm.Load<Texture2D>("menu");
            inputManager = im;
            idle = true;
        }

        public bool IsIdle() { return idle; }

        public float GetX() { return mousePos.X; }
        public float GetY() { return mousePos.Y; }

        public bool Clicked()
        {
            return inputManager.IsLeftMouseClicked();
        }

        public void Update()
        {
            mousePos = inputManager.GetMousePos();

            if (!idle)
            {
                if (alpha < 1)
                    alpha += 0.05f;
            }
            else
            {
                if (alpha > 0)
                    alpha -= 0.05f;
            }

            if (mousePos != lastMousePos)
            {
                idle = false;
                timer = 0;
            }
            else if (!idle)
            {
                timer++;
                if (timer >= 30)
                {
                    timer = 0;
                    idle = true;
                }
            }

            lastMousePos = mousePos;
        }

        public void Draw(SpriteBatch sb)
        {
            if (alpha>0)
                sb.Draw(texture, new Vector2((int)mousePos.X, (int)mousePos.Y), new Rectangle(515, 6, 16, 16), new Color(123, 123, 123, alpha));
        }
    }
}
