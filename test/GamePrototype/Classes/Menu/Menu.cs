using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace GamePrototype.Classes.Menu
{
    enum Category { Main, Journal, Clues, Settings }
    enum SelectedEntry { TopLeft, TopRight, BotLeft, BotRight, Upper, Lower }

    class Menu : Tools.IControlled
    {

        private Category activeMenu;
        private SelectedEntry selectedEntry;
        Clue openedClue; // so the menu knows if the player's chosen a clue to look at
        Dictionary<Clue, Boolean> clueList;

        public Menu()
        {
            activeMenu = Category.Main;
            selectedEntry = SelectedEntry.TopRight;
        }

        public void Update()
        {
            CheckInput();
        }

        public void CheckInput()
        {
            KeyboardState kbState = Keyboard.GetState();
            switch(selectedEntry)
            {
                case SelectedEntry.TopLeft:
                    {
                        break;
                    }
                case SelectedEntry.TopRight:
                    {
                        break;
                    }
                case SelectedEntry.BotLeft:
                    {
                        break;
                    }
                case SelectedEntry.BotRight:
                    {
                        break;
                    }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }

    // TODO: Menu class will need an update and draw method, should check for input
    // Needs to have a way of tracking which menu item it's on, we may also need a more categorical version of this class to hold other menus
    // If the player hits whatever the method of selection is, it should call an Open() method of the menu item highlighted
    // The menu view itself also needs a property and attribute to check if it's enabled, as well as an Open method for itself which toggles that on/Close method for off
    // Other than the main view of the phone itself and maybe settings, menus should use lists for content since as the player finds clues they go there
    // Implements IControlled, use this to check user input while in the menu. Whoever does player updating should make sure they check if the menu is open before checking for input.

}
