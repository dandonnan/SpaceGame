﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    public class Ship
    {
        Texture2D texture;
        Vector2 position;
        Rectangle collision;

        float boltCooldown = 10;
        float cooldown;
        bool firing = false;

        bool canMove = true;

        int frame = 0;

        InputManager inputManager;

        public Ship(ContentManager cm, InputManager im)
        {
            texture = cm.Load<Texture2D>("ship");

            inputManager = im;

            position = new Vector2(10, 310);

            updateCollision();
        }

        void updateCollision()
        {
            collision = new Rectangle((int)position.X, (int)position.Y, 64, 64);
        }

        public Vector2 GetPosition() { return position; }
        public Rectangle GetCollision() { return collision; }

        public int GetFrame() { return frame; }

        public void LockControls(bool l) { canMove = !l; }
        public bool ControlsLocked() { return !canMove; }

        public bool HasFired() {
            if (firing)
            {
                firing = false;
                return true;
            }

            return false;
        }

        public void Update()
        {
            frame = 0;

            if (cooldown > 0)
            {
                cooldown--;
            }

            if (canMove)
            {
                if (inputManager.InputFire())
                {
                    if (cooldown <= 0 && !firing)
                    {
                        cooldown = boltCooldown;
                        firing = true;
                    }
                }

                if (inputManager.InputUp())
                {
                    frame = 2;

                    if (position.Y > 0)
                        position.Y -= 3;
                }

                if (inputManager.InputDown())
                {
                    frame = 1;

                    if (position.Y < 650)
                        position.Y += 3;
                }
            }

            updateCollision();
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, new Rectangle(64*frame, 0, 64, 64), Color.White);
        }
    }
}
