using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GamePrototype.Classes;
using GamePrototype.Classes.Objects;
using GamePrototype.Classes.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*Workers: Caleb, Tom
 * DisasterPiece Games
 * MenuObject Class
 */
namespace GamePrototype.Classes.Menu
{
    class MenuObject
    {
        // May need to be separated into multiple other classes

        // attributes
        Rectangle rect;
        // NOTE: This class may not need to be used in light of how recent menu implementation has been handled, but just in case it's here to cover bases.
        // If we do use it, the primary function will be to associate the clues a player has found with a class that encompasses more. So if the player looks
        // in a menu to see what clues they've found, those'll be associated with MenuObjects which will, more than just containing the Clue object itself, can also have other elements like a thumbnail or such.
        // If we go with this method we'll also use this class to standardize display of menu items in a way that makes it easy to add more and control which appear on the screen, since not obviously not every clue can fit on one
        // "page" of the menu. There's too many and more will be available as the player finds them, so be it a scroll or pages we'll work that out here.
        public MenuObject(Rectangle r)
        {
            rect = r;
        }
    }
}