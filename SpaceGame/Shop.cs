using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    class Shop
    {
        int unopenedBoxes;

        SaveData saveData;

        public Shop(SaveData sd)
        {
            saveData = sd;
        }

        public void Refresh()
        {
            unopenedBoxes = saveData.GetUnopenedBoxes();
        }

        void openBox()
        {
            unopenedBoxes--;
            saveData.RemoveUnopenedBox();
            saveData.Save();
        }

        public void Update()
        {
        }

        public void Draw(SpriteBatch sb)
        {
        }
    }
}
