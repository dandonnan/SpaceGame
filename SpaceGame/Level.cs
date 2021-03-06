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
        enum States { Playing, Paused, GameOver, Leaderboard }
        States currentState;

        Ship ship;
        List<Monster> monsters;
        List<Bolt> bolts;
        StarfieldManager starfield;
        Texture2D extraAssets;

        int score;
        int currentScore;
        int sessionScore;
        bool reduced;
        SpriteFont font;

        bool eaten;

        float monsterTimer = 0;

        ContentManager contentManager;
        InputManager inputManager;
        SaveData saveData;

        PauseMenu pauseMenu;

#if DEBUG
        bool drawObjectCount;
        bool drawStarCount;
#endif

        public Level(ContentManager cm, InputManager im, SaveData sd)
        {
            contentManager = cm;
            inputManager = im;

            saveData = sd;

            font = cm.Load<SpriteFont>("scorefont");
            extraAssets = cm.Load<Texture2D>("menu");

            pauseMenu = new PauseMenu(cm, im, sd, this);

            reset();

#if DEBUG
            drawObjectCount = false;
            drawStarCount = false;
#endif
        }

        void reset()
        {
            ship = new Ship(contentManager, inputManager);
            monsters = new List<Monster>();
            bolts = new List<Bolt>();
            starfield = new StarfieldManager(contentManager);
            reduced = false;
            eaten = false;
            score = 0;
            currentScore = 0;
            currentState = States.Playing;
            sessionScore = 0;
        }

        public void IncrementScore(int val)
        {
            score += val;
            currentScore += val;
        }

        public void DecrementScore(int val)
        {
            score -= val;
            currentScore -= val;
        }

        public void SetScore(int val)
        {
            score = val;
            currentScore = val;
        }

#if DEBUG
        public void ToggleDebugObjectCount()
        {
            if (drawObjectCount)
                drawObjectCount = false;
            else
                drawObjectCount = true;
        }

        public void ToggleDebugStarCount()
        {
            if (drawStarCount)
                drawStarCount = false;
            else
                drawStarCount = true;
        }
#endif

        public int Update()
        {
            int val = 0;

            switch (currentState)
            {
                case States.Playing:
                    updatePlaying();
                    break;

                case States.Paused:
                    val = pauseMenu.Update();

                    if (val == 1)
                        currentState = States.Playing;

                    if (val == 2)
                    {
                        reset();
                        return 1;
                    }
                    break;

                case States.GameOver:
                    val = updateGameOver();

                    if (val == 1)
                        return 1;
                    break;

                case States.Leaderboard:
                    updateLeaderboard();
                    break;
            }

            return 0;
        }

        public void Draw(SpriteBatch sb)
        {
            switch (currentState)
            {
                case States.Playing:
                    drawPlaying(sb);
                    break;

                case States.Paused:
                    drawPaused(sb);
                    break;

                case States.GameOver:
                    drawGameOver(sb);
                    break;

                case States.Leaderboard:
                    drawLeaderboard(sb);
                    break;
            }
        }

        void updatePlaying()
        {
            if (GamePad.GetState(0).Buttons.Back == ButtonState.Pressed)
                reset();

            if (inputManager.InputPause())
                currentState = States.Paused;

            if (score <= 0)
            {
                score = 0;

                if (reduced && !eaten)
                    currentState = States.GameOver;
                else if (eaten)
                {
                    if (currentScore == 0)
                        currentState = States.GameOver;
                }
            }

            if (score > saveData.GetHighScore())
                saveData.SetHighScore(score);

            if (score > sessionScore)
                sessionScore = score;

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
                if (ship.GetFrame() != 2)
                    bolts.Add(new Bolt(contentManager, new Vector2(ship.GetPosition().X + 32, ship.GetPosition().Y + 48), saveData.GetBoltColour(), 5, 400));
                else
                    bolts.Add(new Bolt(contentManager, new Vector2(ship.GetPosition().X + 32, ship.GetPosition().Y + 20), saveData.GetBoltColour(), 5, 400));
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

                    if (ship.GetCollisionBox().Intersects(monsters[i].GetCollisionBox()))
                    {
                        if (!ship.ControlsLocked())
                        {
                            ship.LockControls(true);
                            monsters[i].SetEating();
                            monsters[i].SetPosition(new Vector2(ship.GetPosition().X + 20, ship.GetPosition().Y));
                            score = 0;
                            eaten = true;
                        }
                    }

                    if (bolts.Count > 0)
                    {
                        for (int j = bolts.Count - 1; j >= 0; j--)
                        {
                            if (bolts[j].GetCollisionBox().Intersects(monsters[i].GetCollisionBox()))
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

            starfield.Update();
        }

        int updateGameOver()
        {
            if (inputManager.InputAccept())
            {
                saveData.AddPlayerExp(sessionScore/100);
                reset();
                saveData.Save();
                return 1;
            }

            return 0;
        }

        void updateLeaderboard()
        {

        }

        void drawPlaying(SpriteBatch sb)
        {
            starfield.Draw(sb);

            sb.Draw(extraAssets, new Vector2(0, 10), new Rectangle(729, 13, 146, 27), Color.White);
            sb.DrawString(font, "Score: " + currentScore, new Vector2(10, 12), Color.Black);

            ship.Draw(sb);

            foreach (Monster m in monsters)
            {
                m.Draw(sb);
            }

            foreach (Bolt b in bolts)
            {
                b.Draw(sb);
            }

#if DEBUG
            if (drawObjectCount)
                sb.DrawString(font, "Monsters: " + monsters.Count() + ", Bolts: " + bolts.Count(), new Vector2(10, 630), Color.White);

            if (drawStarCount)
                sb.DrawString(font, "Stars: " + starfield.GetStarfieldObjectCount(), new Vector2(10, 650), Color.White);
#endif
        }

        void drawPaused(SpriteBatch sb)
        {
            starfield.Draw(sb);
            drawPlaying(sb);

            pauseMenu.Draw(sb);
        }

        void drawGameOver(SpriteBatch sb)
        {
            sb.DrawString(font, "Game Over", new Vector2(600, 300), Color.White);
        }

        void drawLeaderboard(SpriteBatch sb)
        {

        }
    }
}
