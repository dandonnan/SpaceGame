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

        public Card()
        {
        }

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
