using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    public class StarfieldObject : GameObject
    {
        public enum Predefined { SmallStar, PointedStar, Planet, Nebula };

        float velocity = 0;
        float scale = 0;
        float rotation = 0;

        int spriteOffsetX;
        int spriteOffsetY;
        int spriteWidth;
        int spriteHeight;

        public StarfieldObject(Texture2D tex, Vector2 start, float vel, float scl, Predefined def)
        {
            texture = tex;
            position = start;
            velocity = vel;
            scale = scl;

            if (def == Predefined.SmallStar)
            {
                spriteOffsetX = 1;
                spriteOffsetY = 1;
                spriteWidth = 1;
                spriteHeight = 1;
            }
            else if (def == Predefined.PointedStar)
            {
                spriteOffsetX = 6;
                spriteOffsetY = 1;
                spriteWidth = 3;
                spriteHeight = 3;
            }
            else if (def == Predefined.Planet)
            {
                spriteOffsetX = 51;
                spriteOffsetY = 682;
                spriteWidth = 151;
                spriteHeight = 38;
            }
            else if (def == Predefined.Nebula)
            {
                Random rand = new Random();
                int type = rand.Next(0, 3);

                spriteOffsetX = 10;
                spriteOffsetY = 2;
                spriteWidth = 32;
                spriteHeight = 25;

                if (type == 1)
                {
                    spriteOffsetX = 44;
                    spriteOffsetY = 4;
                    spriteWidth = 23;
                    spriteHeight = 18;
                }
                else if (type==2)
                {
                    spriteOffsetX = 70;
                    spriteOffsetY = 2;
                    spriteWidth = 27;
                    spriteHeight = 31;
                }

                rotation = rand.Next(0, (int)Math.PI*2);
            }

        }

        public void PauseMovement() { }
        public void ResumeMovement() { }

        public int GetWidth()
        {
            return spriteWidth;
        }

        public override void Update()
        {
            position.X -= velocity;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, new Rectangle(spriteOffsetX, spriteOffsetY, spriteWidth, spriteHeight), Color.White, rotation, new Vector2(spriteWidth/2, spriteHeight/2), scale, SpriteEffects.None, 0);
        }
    }
}
