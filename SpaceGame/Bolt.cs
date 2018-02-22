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
    public class Bolt : GameObject
    {
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

            width = 8;
            height = 8;

            updateCollision();
        }

        public override bool IsDead()
        {
            if (lifetime <= 0)
                return true;

            return false;
        }

        public override void Update()
        {
            position.X += speed;

            lifetime--;

            updateCollision();
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, colour);
        }
    }
}
