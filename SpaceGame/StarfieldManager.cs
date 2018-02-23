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

        public StarfieldManager(ContentManager cm)
        {
            objects = new List<StarfieldObject>();

            objects.Add(new StarfieldObject(cm.Load<Texture2D>("starfield"), new Vector2(1280, 5), 3, 1, StarfieldObject.Predefined.SmallStar));
            objects.Add(new StarfieldObject(cm.Load<Texture2D>("starfield"), new Vector2(1300, 360), 3, 3, StarfieldObject.Predefined.SmallStar));
            objects.Add(new StarfieldObject(cm.Load<Texture2D>("starfield"), new Vector2(1450, 590), 3, 1, StarfieldObject.Predefined.SmallStar));
            objects.Add(new StarfieldObject(cm.Load<Texture2D>("starfield"), new Vector2(1350, 682), 0.1f, 1, StarfieldObject.Predefined.Planet));
        
        }

        public void Update()
        {
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
