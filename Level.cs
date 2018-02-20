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
    public class Level
    {
        Ship ship;
        List<Monster> monsters;
        List<Bolt> bolts;

        float score;
        float currentScore;
        bool reduced;
        SpriteFont font;

        float monsterTimer = 0;

        ContentManager contentManager;

        public Level(ContentManager cm)
        {
            contentManager = cm;

            font = cm.Load<SpriteFont>("scorefont");

            reset();
        }

        void reset()
        {
            ship = new Ship(contentManager);
            monsters = new List<Monster>();
            bolts = new List<Bolt>();
            reduced = false;
        }

        public void Update()
        {
            if (GamePad.GetState(0).Buttons.Back == ButtonState.Pressed)
                reset();

            if (score <= 0)
            {
                score = 0;

                if (reduced)
                    reset();
            }

            if (currentScore < score)
                currentScore += 10;
            else if (currentScore > score)
                currentScore -= 10;

            monsterTimer--;
            if (monsterTimer <= 0)
            {
                monsterTimer = 150;
                Random rand = new Random();
                int y = rand.Next(0, 650);

                float speed = 1;

                if (score >= 10000)
                {
                    speed = 10;
                    monsterTimer = 50;
                }
                else if (score >= 7500)
                {
                    speed = 7;
                    monsterTimer = 60;
                }
                else if (score >= 5000)
                {
                    speed = 6;
                    monsterTimer = 70;
                }
                else if (score >= 2000)
                {
                    speed = 5;
                    monsterTimer = 80;
                }
                else if (score >= 1000)
                {
                    speed = 4;
                    monsterTimer = 90;
                }
                else if (score >= 500)
                {
                    speed = 3;
                    monsterTimer = 100;
                }
                else if (score >= 100)
                    speed = 2;
                
                monsters.Add(new Monster(contentManager, new Vector2(1280, y), speed));
            }

            ship.Update();

            if (ship.HasFired())
            {
                bolts.Add(new Bolt(contentManager, new Vector2(ship.GetPosition().X+32, ship.GetPosition().Y+48), Color.Green, 5, 400));
            }

            if (bolts.Count > 0)
            {
                for (int i = bolts.Count - 1; i >= 0; i--)
                {
                    bolts[i].Update();

                    if (bolts[i].IsDead())
                        bolts.RemoveAt(i);
                }
            }

            if (monsters.Count > 0)
            {
                for (int i = monsters.Count - 1; i >= 0; i--)
                {
                    monsters[i].Update();

                    if (ship.GetCollision().Intersects(monsters[i].GetCollision()))
                    {
                        if (!ship.ControlsLocked())
                        {
                            ship.LockControls(true);
                            monsters[i].SetEating();
                            monsters[i].SetPosition(new Vector2(ship.GetPosition().X + 20, ship.GetPosition().Y));
                        }
                    }

                    if (bolts.Count > 0)
                    {
                        for (int j = bolts.Count - 1; j >= 0; j--)
                        {
                            if (bolts[j].GetCollision().Intersects(monsters[i].GetCollision()))
                            {
                                monsters[i].DoDamage(1);
                                score += 100;
                                bolts.RemoveAt(j);
                            }
                        }
                    }

                    if (monsters[i].IsDead())
                        monsters.RemoveAt(i);
                    else
                    {
                        if (monsters[i].GetPosition().X < -64)
                        {
                            reduced = true;
                            score -= 200;
                            monsters.RemoveAt(i);
                        }
                    }
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.DrawString(font, "Score: " + currentScore, new Vector2(10, 10), Color.White);

            ship.Draw(sb);

            foreach (Monster m in monsters)
            {
                m.Draw(sb);
            }

            foreach (Bolt b in bolts)
            {
                b.Draw(sb);
            }
        }
    }
}