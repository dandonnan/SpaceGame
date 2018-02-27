using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    public class Card
    {
        public enum CardTypes { Crew, Ship, Part }
        CardTypes cardType;

        string name;
        UltimateCrew.Roles role;
        UltimateCrew.Fleets fleet;

        float intellect;
        float reflex;

        int buyValue;
        int resaleValue;

        bool shiny;
        bool inHand;

        public Card()
        {
        }

        public Card(CardTypes type, string n, UltimateCrew.Roles r, UltimateCrew.Fleets f, int bv)
        {
            cardType = type;
            name = n;
            role = r;
            fleet = f;
            buyValue = bv;
            resaleValue = buyValue / 2;
        }

        public Card(CardTypes type, string n, UltimateCrew.Roles r, UltimateCrew.Fleets f, int bv, float inct, float rflx)
        {
            cardType = type;
            name = n;
            role = r;
            fleet = f;
            buyValue = bv;
            resaleValue = buyValue / 2;

            intellect = inct;
            reflex = rflx;
        }

        public void SetResaleValue(int rv) { resaleValue = rv; }

        public bool IsInHand() { return inHand; }
        public void AddToHand() { inHand = true; }
        public void RemoveFromHand() { inHand = false; }

        public void SetShiny() { shiny = true; }
        public void SetShiny(bool s) { shiny = s; }

        public string GetName() { return name; }
        public CardTypes GetCardType() { return cardType; }
        public UltimateCrew.Roles GetRole() { return role; }
        public UltimateCrew.Fleets GetFleet() { return fleet; }

        public float GetIntellect() { return intellect; }
        public float GetReflex() { return reflex; }

        public int GetBuyValue() { return buyValue; }
        public int GetResaleValue() { return resaleValue; }
        public bool IsShiny() { return shiny; }
    }
}
