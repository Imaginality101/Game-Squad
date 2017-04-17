using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
/*Workers: Tom
 * DisasterPiece Games
 * Door Class
 */

namespace GamePrototype.Classes.Objects
{

    class Door : Interactable
    {
        Texture2D doorText;
        Rectangle doorRect;
        Clue requiredClue;
        CurrentRoom wannago;

        public Door(Texture2D txtr, Rectangle psRct,CurrentRoom wenothere, Clue rqClue) : base(txtr, psRct)
        {
            doorText = txtr;
            doorRect = psRct;
            requiredClue = rqClue;
            wannago = wenothere;

        }

        // TODO: Class needs fleshing out, but its primary difference is that interacting
        // with it should somehow prompt the game to switch to another room. Tom's recommendation - custom event handlers. If someone wants to tackle that verbally raise your hand to me,
        // otherwise I'll do it when we get to Milestone 3 and have multiple rooms.
        public override void Interact(Player user)
        {
            // if there is a required clue
            if (requiredClue != null)
            {
                if (Enabled && Clue.Inventory.Contains(requiredClue))
                {
                    Game1.activeRoom = wannago;
                    user.X = 1100;
                    user.Y = 450;
                }
                else
                {
                    Console.WriteLine("You don't have the right key.");
                }
            }
        }

    }
}
