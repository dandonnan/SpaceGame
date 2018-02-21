using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    public class SaveData
    {
        int highScore;
        int playerLevel;
        int playerExp;
        float money;

        public SaveData()
        {
            highScore = 0;
            playerLevel = 1;
            playerExp = 0;
            money = 0;
        }

        public void AddPlayerExp(int exp)
        {
            playerExp += exp;
        }

        public void SetHighScore(int hs) { highScore = hs; }
        public void SetPlayerLevel(int lvl) { playerLevel = lvl; }
        public void SetPlayerExp(int exp) { playerExp = exp; }
        public void SetMoney(float mny) { money = mny; }

        public int GetHighScore() { return highScore; }
        public int GetPlayerLevel() { return playerLevel; }
        public int GetPlayerExp() { return playerExp; }
        public float GetMoney() { return money; }

        public int GetExpToNextLevel()
        {
            if (playerLevel < 100)
            {
                int formula = (playerLevel + 1) * 5 * playerLevel;
                return formula;
            }

            return 0;
        }

        public void Save()
        {
            StreamWriter sw = new StreamWriter("data.sav");
            sw.WriteLine(highScore);
            sw.WriteLine(playerLevel);
            sw.WriteLine(playerExp);
            sw.WriteLine(money);
            sw.Close();
        }

        public static SaveData Load()
        {
            if (File.Exists("data.sav"))
            {
                SaveData sd = new SaveData();

                StreamReader sr = new StreamReader("data.sav");
                sd.SetHighScore(int.Parse(sr.ReadLine()));
                sd.SetPlayerLevel(int.Parse(sr.ReadLine()));
                sd.SetPlayerExp(int.Parse(sr.ReadLine()));
                sd.SetMoney(int.Parse(sr.ReadLine()));
                sr.Close();

                return sd;
            }

            return new SaveData();
        }
    }
}
