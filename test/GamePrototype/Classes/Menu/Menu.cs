using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using GamePrototype;
/*Workers: Kat, Tom
 * DisasterPiece Games
 * Menu Class
 */
namespace GamePrototype.Classes.Menu
{
    enum Category { Main, Journal, Clues, Settings }
    enum SelectedEntry { TopLeft, TopRight, BotLeft, BotRight, Upper, Lower }

    class Menu : Tools.IControlled
    {

        private Category activeMenu;
        private SelectedEntry selectedEntry;
        Clue openedClue; // so the menu knows if the player's chosen a clue to look at
        // deprecated, use Clue.Inventory
        //Dictionary<Clue, Boolean> clueList; // to keep track of
        // first index is the page on the menu, second index is which space it is on: 0 is top left, 1 is top right, 2 is bottom left, 3 is bottom right 
        private Clue[,] pageClue = new Clue[7, 4];
        // icons
        Icon newsPaper;
        Icon bathroomKey;
        Icon closetKey;
        Icon oldPhoto;
        Icon newPhoto;
        Icon tenantDiary;
        Icon crazyPersonDiary;
        Icon receipt;
        Icon ring;
        Icon pendant;
        Icon bones;
        Icon jaggedKnife;
        Icon spaCoupon;
        Icon medicineBottle;
        Icon stickyNote;
        public Menu()
        {
            activeMenu = Category.Main;
            selectedEntry = SelectedEntry.TopRight;
            // commented out because it is deprecated
            /*clueList = new Dictionary<Clue, Boolean>();
            foreach(Clue cl in Clue.Clues.Values)
            {
                clueList.Add(cl, false);
            }*/
        }
        // TODO: Load icons in Game1, pass them here in an array
        public void LoadContent(Texture2D nws, Texture2D stcky)
        {
            newsPaper = new Icon(nws, new Rectangle(755, 210, 70, 100));
            //stickyNote = new Icon(stcky, new Rectangle(300, 0, 50, 50));
        }
        public void Update()
        {
            CheckInput();
        }

        public void CheckInput()
        {
            KeyboardState kbState = Keyboard.GetState();
            KeyboardState prevKbState = kbState;
            switch (selectedEntry)
            {
                case SelectedEntry.TopLeft:
                    {
                        if (kbState.IsKeyDown(Keys.S) && prevKbState.IsKeyUp(Keys.S))
                        {
                            selectedEntry = SelectedEntry.BotLeft;
                        }
                        else if (kbState.IsKeyDown(Keys.D) && prevKbState.IsKeyUp(Keys.D))
                        {
                            selectedEntry = SelectedEntry.TopRight;
                        }
                        break;
                    }
                case SelectedEntry.TopRight:
                    {
                        if (kbState.IsKeyDown(Keys.S) && prevKbState.IsKeyUp(Keys.S))
                        {
                            selectedEntry = SelectedEntry.BotRight;
                        }
                        else if (kbState.IsKeyDown(Keys.A) && prevKbState.IsKeyUp(Keys.A))
                        {
                            selectedEntry = SelectedEntry.TopLeft;
                        }
                        break;
                    }
                case SelectedEntry.BotLeft:
                    {
                        if (kbState.IsKeyDown(Keys.W) && prevKbState.IsKeyUp(Keys.W))
                        {
                            selectedEntry = SelectedEntry.TopLeft;
                        }
                        else if (kbState.IsKeyDown(Keys.D) && prevKbState.IsKeyUp(Keys.D))
                        {
                            selectedEntry = SelectedEntry.BotRight;
                        }
                        break;
                    }
                case SelectedEntry.BotRight:
                    {
                        if (kbState.IsKeyDown(Keys.W) && prevKbState.IsKeyUp(Keys.W))
                        {
                            selectedEntry = SelectedEntry.TopRight;
                        }
                        else if (kbState.IsKeyDown(Keys.A) && prevKbState.IsKeyUp(Keys.A))
                        {
                            selectedEntry = SelectedEntry.BotLeft;
                        }
                        break;
                    }
            }  
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (gameState == GameState.GMenu && menuState == MenuState.Main)
            {
                bedRoom.Draw(uSpriteBatch);
                uSpriteBatch.Draw(startingPhoneState, new Rectangle(300, 0, 1200, 1000), Color.White);
            }
            if (gameState == GameState.GMenu && menuState == MenuState.Journal)
            {
                bedRoom.Draw(uSpriteBatch);
                uSpriteBatch.Draw(textPhoneState, new Rectangle(300, 0, 1200, 1000), Color.White);
            }
            if (gameState == GameState.GMenu && menuState == MenuState.Photos)
            {
                bedRoom.Draw(uSpriteBatch);
                uSpriteBatch.Draw(imagePhoneState, new Rectangle(300, 0, 1200, 1000), Color.White);
            }
            if (gameState == GameState.GMenu && menuState == MenuState.Settings)
            {
                bedRoom.Draw(uSpriteBatch);
                uSpriteBatch.Draw(textPhoneState, new Rectangle(300, 0, 1200, 1000), Color.White);
                // Draw textbox
                settingsTextBox.Draw(uSpriteBatch);
            }
            if (Clue.Inventory.Contains(Clue.Clues["News1"]) || Clue.Inventory.Contains(Clue.Clues["News2"]) || Clue.Inventory.Contains(Clue.Clues["News3"]) || Clue.Inventory.Contains(Clue.Clues["News4"]) || Clue.Inventory.Contains(Clue.Clues["News5"]))
            {
                newsPaper.Draw(spriteBatch);
            }
            if (Clue.Inventory.Contains(Clue.Clues["StickyNote"]))
            {
                stickyNote.Draw(spriteBatch);
            }
        }
    }

    // TODO: Menu class will need an update and draw method, should check for input
    // Needs to have a way of tracking which menu item it's on, we may also need a more categorical version of this class to hold other menus
    // If the player hits whatever the method of selection is, it should call an Open() method of the menu item highlighted
    // The menu view itself also needs a property and attribute to check if it's enabled, as well as an Open method for itself which toggles that on/Close method for off
    // Other than the main view of the phone itself and maybe settings, menus should use lists for content since as the player finds clues they go there
    // Implements IControlled, use this to check user input while in the menu. Whoever does player updating should make sure they check if the menu is open before checking for input.
    // - Tom
}
