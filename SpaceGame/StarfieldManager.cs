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
    public class StarfieldManager
    {
        List<StarfieldObject> objects;
        Texture2D starfield;

        int minTime;
        int maxTime;
        int timeUntilNext;

        bool planetActive = false;

        public StarfieldManager(ContentManager cm)
        {
            objects = new List<StarfieldObject>();
            starfield = cm.Load<Texture2D>("starfield");

            minTime = 1;
            maxTime = 5;

            objects.Add(new StarfieldObject(starfield, new Vector2(1350, 682), 0.1f, 1, StarfieldObject.Predefined.Planet));
        }

        public void Update()
        {
            timeUntilNext--;

            if (timeUntilNext <= 0)
            {
                Random rand = new Random();
                timeUntilNext = rand.Next(minTime, maxTime + 1);
                int y = rand.Next(0, 682);
                objects.Add(new StarfieldObject(starfield, new Vector2(1280, y), 3, 1, StarfieldObject.Predefined.SmallStar));
            }

            if (objects.Count > 0)
            {
                for (int i = objects.Count - 1; i >= 0; i--)
                {
                    objects[i].Update();

                    if (objects[i].GetPositionX() < 0 - objects[i].GetWidth())
                        objects.RemoveAt(i);
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (StarfieldObject so in objects)
                so.Draw(sb);
        }
    }
}
