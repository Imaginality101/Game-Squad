using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePrototype.Classes.Menu
{
    class Menu : Tools.IControlled
    {

        public void CheckInput()
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
