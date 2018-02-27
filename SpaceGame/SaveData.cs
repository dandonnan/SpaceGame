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

        int boxesToOpen;
        bool[] bolts;
        bool[] colours;

        List<Card> deck;

        public SaveData()
        {
            highScore = 0;
            playerLevel = 1;
            playerExp = 0;
            money = 0;

            boxesToOpen = 0;

            colours = new bool[4];  // Green, Red, Blue, Yellow, 

            deck = new List<Card>();

            deck.Add(new Card(Card.CardTypes.Ship, "USS Untitled", UltimateCrew.Roles.Ship, UltimateCrew.Fleets.Undefined, 1000));
            deck.Add(new Card(Card.CardTypes.Part, "Blaster Mk1", UltimateCrew.Roles.Weapon, UltimateCrew.Fleets.Undefined, 500));
            deck.Add(new Card(Card.CardTypes.Part, "Scattershot Mk1", UltimateCrew.Roles.Weapon, UltimateCrew.Fleets.Undefined, 500));
            deck.Add(new Card(Card.CardTypes.Crew, "El Capitan", UltimateCrew.Roles.Captain, UltimateCrew.Fleets.Undefined, 400));
            deck.Add(new Card(Card.CardTypes.Crew, "Engine Man", UltimateCrew.Roles.Engineer, UltimateCrew.Fleets.Undefined, 300));
            deck.Add(new Card(Card.CardTypes.Crew, "Mr. Driver", UltimateCrew.Roles.Pilot, UltimateCrew.Fleets.Undefined, 200));

            foreach (Card c in deck)
                c.AddToHand();
        }

        public void AddPlayerExp(int exp)
        {
            playerExp += exp;
        }

        public void AddUnopenedBox() { boxesToOpen++; }
        public void RemoveUnopenedBox() { boxesToOpen--; }
        public void AddCardToList(Card c) { deck.Add(c); }

        public void SetHighScore(int hs) { highScore = hs; }
        public void SetPlayerLevel(int lvl) { playerLevel = lvl; }
        public void SetPlayerExp(int exp) { playerExp = exp; }
        public void SetMoney(float mny) { money = mny; }
        public void SetUnopenedBoxes(int box) { boxesToOpen = box; }

        public int GetHighScore() { return highScore; }
        public int GetPlayerLevel() { return playerLevel; }
        public int GetPlayerExp() { return playerExp; }
        public float GetMoney() { return money; }
        public int GetUnopenedBoxes() { return boxesToOpen; }
        public List<Card> GetCardList() { return deck; }

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
            sw.WriteLine(boxesToOpen);
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
                if (!sr.EndOfStream) { sd.SetUnopenedBoxes(int.Parse(sr.ReadLine())); }
                sr.Close();

                return sd;
            }

            return new SaveData();
        }
    }
}
