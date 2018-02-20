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
    public class Bolt
    {
        Texture2D texture;
        Vector2 position;
        Rectangle collision;

        Color colour;

        int speed = 1;

        float lifetime = 1200;

        public Bolt(ContentManager cm, Vector2 pos, Color clr, int spd, float life)
        {
            texture = cm.Load<Texture2D>("bolt");
            position = pos;
            colour = clr;
            speed = spd;
            lifetime = life;

            updateCollision();
        }

        void updateCollision()
        {
            collision = new Rectangle((int)position.X, (int)position.Y, 8, 8);
        }

        public Rectangle GetCollision() { return collision; }
        public Vector2 GetPosition() { return position; }

        public bool IsDead()
        {
            if (lifetime <= 0)
                return true;

            return false;
        }

        public void Update()
        {
            position.X += speed;

            lifetime--;

            updateCollision();
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, colour);
        }
    }
}
