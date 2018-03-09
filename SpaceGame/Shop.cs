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
    public class Shop
    {
        int unopenedBoxes;
        bool openingBox;

        int currentSelection;

        SaveData saveData;
        ContentManager contentManager;
        InputManager inputManager;

        Texture2D spriteSheet;
        SpriteFont tabFont;
        SpriteFont itemFont;

        UIElement shopLoot;
        UIElement shopCards;
        UIElement shopRewards;

        LootboxReveal revealBox;

        public Shop(ContentManager cm, InputManager im, SaveData sd, MousePointer mp)
        {
            saveData = sd;
            contentManager = cm;
            inputManager = im;

            currentSelection = 0;
            openingBox = false;

            unopenedBoxes = saveData.GetUnopenedBoxes();

            spriteSheet = cm.Load<Texture2D>("menu");
            tabFont = cm.Load<SpriteFont>("scorefont");
            itemFont = cm.Load<SpriteFont>("menuitem");

            shopLoot = new UIElement(spriteSheet, new Vector2(100, 275), new Rectangle(480, 660, 192, 192), mp);
            shopCards = new UIElement(spriteSheet, new Vector2(320, 275), new Rectangle(480, 660, 192, 192), mp);
            shopRewards = new UIElement(spriteSheet, new Vector2(540, 275), new Rectangle(480, 660, 192, 192), mp);

            shopLoot.AddTextElement(new TextElement("BUY LOOTBOXES", new Vector2(110, 350), itemFont));
            shopCards.AddTextElement(new TextElement("BUY CARD PACKS", new Vector2(325, 350), itemFont));
            shopRewards.AddTextElement(new TextElement("REWARDS", new Vector2(560, 350), itemFont));

            string rewards;
            if (unopenedBoxes == 1)
                rewards = unopenedBoxes + " Reward Available";
            else
                rewards = unopenedBoxes + " Rewards Available";

            shopRewards.AddTextElement(new TextElement(rewards, new Vector2(560, 390), tabFont));
        }

        public void Refresh()
        {
            unopenedBoxes = saveData.GetUnopenedBoxes();

            string rewards;
            if (unopenedBoxes == 1)
                rewards = unopenedBoxes + " Reward Available";
            else
                rewards = unopenedBoxes + " Rewards Available";

            shopRewards.UpdateTextElement(1, rewards);
        }

        void openBox()
        {
            unopenedBoxes--;
            saveData.RemoveUnopenedBox();
            saveData.Save();
        }

        public int Update()
        {
            if (!openingBox)
            {
                if (inputManager.InputLeftPressed())
                {
                    if (currentSelection == 0)
                        return 1;
                    else
                        currentSelection--;
                }

                if (inputManager.InputRightPressed())
                {
                    if (currentSelection == 2)
                    {
                        currentSelection = 0;
                        return 2;
                    }
                    else
                        currentSelection++;
                }

                if (inputManager.InputAccept())
                {
                    if (currentSelection == 2)
                    {
                        if (unopenedBoxes > 0)
                        {
                            revealBox = new LootboxReveal(contentManager, inputManager);
                            openingBox = true;
                        }
                        else
                        {
                            // display message about not having boxes to open
                        }
                    }
                }
            }
            else
            {
                // wait for box to open
                int boxDone = revealBox.Update();

                if (boxDone == 1)
                {
                    saveData.RemoveUnopenedBox();
                    saveData.Save();
                    openingBox = false;
                    Refresh();
                }
            }

            return 0;
        }

        public void Draw(SpriteBatch sb)
        {
            shopLoot.Draw(sb);
            shopCards.Draw(sb);
            shopRewards.Draw(sb);

            string drawSelected = "";
            if (currentSelection == 0) { drawSelected = "Buy Box"; }
            else if (currentSelection == 1) { drawSelected = "Buy Cards"; }
            else { drawSelected = "Get Rewards"; }
            sb.DrawString(tabFont, "Select " + drawSelected, new Vector2(140, 647), Color.White);

            if (openingBox)
                revealBox.Draw(sb);
        }
    }
}
