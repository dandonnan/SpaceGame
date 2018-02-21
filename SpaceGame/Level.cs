using Microsoft.Xna.Framework;
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

        public Level(ContentManager cm, InputManager im, SaveData sd)
        {
            contentManager = cm;
            inputManager = im;

            saveData = sd;

            font = cm.Load<SpriteFont>("scorefont");

            reset();
        }

        void reset()
        {
            ship = new Ship(contentManager, inputManager);
            monsters = new List<Monster>();
            bolts = new List<Bolt>();
            reduced = false;
            eaten = false;
            score = 0;
            currentScore = 0;
            currentState = States.Playing;
            sessionScore = 0;
        }

        public int Update()
        {
            int val = 0;

            switch (currentState)
            {
                case States.Playing:
                    updatePlaying();
                    break;

                case States.Paused:
                    updatePaused();
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
                    bolts.Add(new Bolt(contentManager, new Vector2(ship.GetPosition().X + 32, ship.GetPosition().Y + 48), Color.Green, 5, 400));
                else
                    bolts.Add(new Bolt(contentManager, new Vector2(ship.GetPosition().X + 32, ship.GetPosition().Y + 20), Color.Green, 5, 400));
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
                            score = 0;
                            eaten = true;
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

        void updatePaused()
        {
            if (inputManager.InputPause())
                currentState = States.Playing;
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

        void drawPaused(SpriteBatch sb)
        {
            drawPlaying(sb);
            sb.DrawString(font, "Paused", new Vector2(600, 300), Color.White);
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
