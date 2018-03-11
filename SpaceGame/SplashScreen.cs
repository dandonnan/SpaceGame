using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    class SplashScreen
    {
        enum Modes { EA, }
        Modes currentMode;

        enum EA_Modes { E, A, Sports, }
        EA_Modes eaMode;

        Texture2D ea;

        Vector2 eaEPos;
        Vector2 eaAPos;
        Vector2 eaSportsPos;

        float eaRotation = 2 * (float)Math.PI;
        float eaAlpha = 0;

        Song eaSong;

        float eaScale;

        int waitTime = 50;

        bool finished = false;

        public SplashScreen(ContentManager cm)
        {
            currentMode = Modes.EA;

            ea = cm.Load<Texture2D>("ea");
            eaEPos = new Vector2(506, 240);
            eaAPos = new Vector2(606, 240);
            eaSportsPos = new Vector2(640, 440);

            // Need to create an AudioManager, and probably make this a sound effect
            // rather than a song (songs are usually background music)
            eaSong = cm.Load<Song>("eaJingle");

            eaScale = 20f;
        }

        /// <summary>
        /// Checks if the animations / sounds are finished
        /// </summary>
        /// <returns>Returns true when everything is done</returns>
        public bool IsFinished() { return finished; }

        /// <summary>
        /// The main update loop
        /// </summary>
        public void Update()
        {
            switch (currentMode)
            {
                case Modes.EA:
                    switch (eaMode)
                    {
                        case EA_Modes.E:
                            if (MediaPlayer.State != MediaState.Playing)
                                MediaPlayer.Play(eaSong);

                            eaScale -= 0.5f;

                            if (eaScale <= 1)
                            {
                                eaScale = 20f;
                                eaMode = EA_Modes.A;
                            }
                            break;

                        case EA_Modes.A:
                            eaScale -= 0.5f;

                            if (eaScale <= 1)
                            {
                                eaScale = 20f;
                                eaMode = EA_Modes.Sports;
                            }
                            break;

                        case EA_Modes.Sports:

                            if (eaScale > 1)
                                eaScale -= 0.5f;

                            if (eaRotation > 0)
                                eaRotation -= (float)Math.PI * 0.05f;

                            if (eaAlpha < 1)
                                eaAlpha += 0.05f;

                            if (MediaPlayer.State == MediaState.Stopped)
                            {
                                waitTime--;

                                if (waitTime <= 0)
                                {
                                    waitTime = 200;
                                    finished = true;
                                }
                            }
                            break;
                    }

                    break;
            }
        }

        /// <summary>
        /// The main render loop
        /// </summary>
        /// <param name="sb"></param>
        public void Draw(SpriteBatch sb)
        {
            switch (currentMode)
            {
                case Modes.EA:
                    sb.GraphicsDevice.Clear(Color.White);

                    switch (eaMode)
                    {
                        case EA_Modes.E:
                            sb.Draw(ea, eaEPos, new Rectangle(21, 457, 181, 144), Color.White, 0f, Vector2.Zero, eaScale, SpriteEffects.None, 0);
                            break;

                        case EA_Modes.A:
                            sb.Draw(ea, eaEPos, new Rectangle(21, 457, 181, 144), Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0);
                            sb.Draw(ea, eaAPos, new Rectangle(207, 457, 186, 145), Color.White, 0f, Vector2.Zero, eaScale, SpriteEffects.None, 0);
                            break;

                        case EA_Modes.Sports:
                            sb.Draw(ea, new Vector2(450, 180), new Rectangle(466, 26, 388, 396), new Color(Color.White, eaAlpha), 0f, Vector2.Zero, 1, SpriteEffects.None, 0);
                            sb.Draw(ea, new Vector2(642, 371), new Rectangle(0, 0, 444, 442), new Color(Color.White, eaAlpha), eaRotation, new Vector2(222, 221), 1, SpriteEffects.None, 0);

                            sb.Draw(ea, eaEPos, new Rectangle(21, 457, 181, 144), Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0);
                            sb.Draw(ea, eaAPos, new Rectangle(207, 457, 186, 145), Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0);
                            sb.Draw(ea, eaSportsPos, new Rectangle(509, 508, 315, 64), Color.White, 0f, new Vector2(154, 32), eaScale, SpriteEffects.None, 0);
                            break;
                    }
                    break;
            }
        }
    }
}
