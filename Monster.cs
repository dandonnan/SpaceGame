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
    public class Monster
    {
        Texture2D texture;
        Vector2 position;
        Rectangle collision;

        int frameW = 0;
        int frameH = 0;

        bool eating = false;

        float fps;

        float health = 1;
        float speed = 1;

        public Monster(ContentManager cm, Vector2 pos, float spd)
        {
            texture = cm.Load<Texture2D>("monster");
            position = pos;

            speed = spd;

            updateCollision();
        }

        void updateCollision()
        {
            collision = new Rectangle((int)position.X, (int)position.Y, 64, 64);
        }

        public bool IsDead()
        {
            if (health <= 0)
                return true;

            return false;
        }

        public Vector2 GetPosition() { return position; }
        public Rectangle GetCollision() { return collision; }

        public void SetEating() { eating = true; }

        public void SetPosition(Vector2 pos)
        {
            position = pos;
            updateCollision();
        }

        public void DoDamage(float damage)
        {
            health -= damage;
        }

        public void Update()
        {
            fps++;

            if (fps >= 10)
            {
                fps = 0;

                frameW++;
                if (frameW > 1)
                    frameW = 0;
            }

            if (!eating)
                position.X-=speed;
            else
                frameH = 1;

            updateCollision();
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, new Rectangle(64*frameW, 64*frameH, 64, 64), Color.White);
        }
    }
}
