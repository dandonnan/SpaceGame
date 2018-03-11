using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    [Serializable()]
    public class SaveData
    {
        int highScore;
        int playerLevel;
        int playerExp;
        float money;

        int shipID;
        string boltColour;

        int boxesToOpen;
        bool[] ships;
        bool[] bolts;
        bool[] colours;

        List<Card> deck;

        public SaveData()
        {
            highScore = 0;
            playerLevel = 1;
            playerExp = 0;
            money = 0;

            boltColour = "Green";

            boxesToOpen = 0;

            colours = new bool[10];  // Green, Red, Blue, Yellow, White, Orange, Purple, Pink (Peach), Silver, Gold

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
        public void SetShipID(int id) { shipID = id; }
        public void SetBoltColour(string clr) { boltColour = clr; }
        public void SetUnopenedBoxes(int box) { boxesToOpen = box; }

        public int GetHighScore() { return highScore; }
        public int GetPlayerLevel() { return playerLevel; }
        public int GetPlayerExp() { return playerExp; }
        public float GetMoney() { return money; }
        public int GetShipID() { return shipID; }
        public string GetBoltColourAsString() { return boltColour; }
        public int GetUnopenedBoxes() { return boxesToOpen; }
        public List<Card> GetCardList() { return deck; }

        /// <summary>
        /// Gets the bolt colour that is current active
        /// </summary>
        /// <returns>The colour of the bolt</returns>
        public Color GetBoltColour()
        {
            Color clr = Color.White;

            if (boltColour == "Green")
                clr = Color.Green;
            else if (boltColour == "Red")
                clr = Color.Red;
            else if (boltColour == "Blue")
                clr = Color.Blue;
            else if (boltColour == "Yellow")
                clr = Color.Yellow;
            else if (boltColour == "Orange")
                clr = Color.Orange;
            else if (boltColour == "Purple")
                clr = Color.Purple;
            else if (boltColour == "Pink")
                clr = Color.Pink;
            else if (boltColour == "Silver")
                clr = Color.Silver;
            else if (boltColour == "Gold")
                clr = Color.Gold;

            return clr;
        }

        /// <summary>
        /// Picks a random bolt from all the colours that have been unlocked
        ///  * YET TO BE WRITTEN *
        ///  Will just return White at the moment
        /// </summary>
        /// <returns>*YET TO BE WRITTEN*</returns>
        public Color GetRandomBoltColour()
        {
            Color clr = Color.White;

            // pick a random colour from those that are unlocked

            return clr;
        }

        /// <summary>
        /// Gets the amount of EXP required to reach the next level
        /// </summary>
        /// <returns>The EXP required</returns>
        public int GetExpToNextLevel()
        {
            if (playerLevel < 100)
            {
                int formula = (playerLevel + 1) * 5 * playerLevel;
                return formula;
            }

            return 0;
        }

        /// <summary>
        /// Unlocks a colour matching the string
        /// </summary>
        /// <param name="clr">The colour to unlock</param>
        public void UnlockColour(string clr)
        {
            if (clr == "Green")
                colours[0] = true;
            else if (clr == "Red")
                colours[1] = true;
            else if (clr == "Blue")
                colours[2] = true;
            else if (clr == "Yellow")
                colours[3] = true;
            else if (clr == "White")
                colours[4] = true;
            else if (clr == "Orange")
                colours[5] = true;
            else if (clr == "Purple")
                colours[6] = true;
            else if (clr == "Pink")
                colours[7] = true;
            else if (clr == "Silver")
                colours[8] = true;
            else if (clr == "Gold")
                colours[9] = true;
        }

        /// <summary>
        /// Unlocks the colour at the given ID
        /// </summary>
        /// <param name="clrID">From 0-9</param>
        public void UnlockColour(int clrID)
        {
            if (clrID >= 0 && clrID < 10)
                colours[clrID] = true;
        }

        /// <summary>
        /// Writes data out to a binary file (extension: .sav)
        /// </summary>
        public void Save()
        {
            using (FileStream fs = File.Create("data.sav", 2048, FileOptions.None))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, this);
            }

            /*StreamWriter sw = new StreamWriter("data.sav");
            sw.WriteLine(highScore);
            sw.WriteLine(playerLevel);
            sw.WriteLine(playerExp);
            sw.WriteLine(money);
            sw.WriteLine(boltColour);
            sw.WriteLine(boxesToOpen);
            sw.Close(); */
        }

        /// <summary>
        /// Reads data in from a binary file (extension: .sav)
        /// </summary>
        /// <returns>Returns a SaveData object that reads from the file</returns>
        public static SaveData Load()
        {
            if (File.Exists("data.sav"))
            {
                SaveData sd = new SaveData();

                using (FileStream fs = File.Open("data.sav", FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    sd = (SaveData)formatter.Deserialize(fs);
                }

                /*StreamReader sr = new StreamReader("data.sav");
                sd.SetHighScore(int.Parse(sr.ReadLine()));
                sd.SetPlayerLevel(int.Parse(sr.ReadLine()));
                sd.SetPlayerExp(int.Parse(sr.ReadLine()));
                sd.SetMoney(int.Parse(sr.ReadLine()));
                sd.SetBoltColour(sr.ReadLine());
                sd.SetUnopenedBoxes(int.Parse(sr.ReadLine()));
                sr.Close(); */

                return sd;
            }

            return new SaveData();
        }

        /// <summary>
        /// Deletes the save data
        /// </summary>
        public static void Delete()
        {
            if (File.Exists("data.sav"))
            {
                File.Delete("data.sav");
            }
        }
    }
}
