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
    public class Monster : GameObject
    {
        int frameW = 0;
        int frameH = 0;

        bool eating = false;

        float fps;
        float speed = 1;

        public Monster(ContentManager cm, Vector2 pos, float spd)
        {
            texture = cm.Load<Texture2D>("monster");
            position = pos;

            speed = spd;

            width = 64;
            height = 64;

            updateCollision();
        }

        public void SetEating() { eating = true; }

        public void DoDamage(float damage)
        {
            health -= damage;
        }

        public override void Update()
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

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, new Rectangle(64*frameW, 64*frameH, 64, 64), Color.White);
        }
    }
}
