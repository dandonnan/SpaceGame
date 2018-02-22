using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    public class GameObject
    {
        protected Texture2D texture;
        protected Vector2 position;
        protected Rectangle collision;

        protected float width;
        protected float height;

        protected bool canMove;
        protected float health;

        // anims
        // fps
        // frames

        public GameObject()
        {
            health = 1;
            canMove = true;
        }

        public Texture2D GetTexture() { return texture; }

        public Vector2 GetPosition() { return position; }
        public float GetPositionX() { return position.X; }
        public float GetPositionY() { return position.Y; }

        public Rectangle GetCollisionBox() { return collision; }

        public void SetPosition(Vector2 pos) { position = pos; updateCollision(); }

        protected void updateCollision()
        {
            collision = new Rectangle((int)position.X, (int)position.Y, (int)width, (int)height);
        }

        public virtual bool IsDead()
        {
            if (health <= 0)
                return true;

            return false;
        }

        public virtual void Update()
        {
        }

        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);
        }
    }
}
